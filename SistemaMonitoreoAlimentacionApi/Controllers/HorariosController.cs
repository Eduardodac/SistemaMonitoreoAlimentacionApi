using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Dtos.Horario;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HorariosController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<Usuario> userManager;

        public HorariosController(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<List<HorarioEntidadDto>>> ListaHorarios()
        {
            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var usuario = await userManager.FindByNameAsync(Username);

            var horarios = await context.Horarios.Where(h => h.UsuarioId == usuario.Id).ToListAsync();

            var HorariosDto = mapper.Map<List<Horario>, List<HorarioEntidadDto>>(horarios);

            return HorariosDto;
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<List<Horario>>> CrearHorarios([FromBody] HorarioEntidadDto horarioCrear)
        {
            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var usuario = await userManager.FindByNameAsync(Username);

            var horarioExistente = await context.Horarios.AnyAsync(h => h.HorarioId.Equals(horarioCrear.HorarioId));

            if (horarioExistente)
            {
                return NotFound($"El horario con id {horarioCrear.HorarioId} ya existe");
            }

            var diadelaSemanaExistente = await context.DiadelaSemana.AnyAsync(d => d.DiadelaSemanaId == horarioCrear.DiaDeLaSemanaId);

            if (!diadelaSemanaExistente)
            {
                return NotFound($"El Dia de la semana con id {horarioCrear.DiaDeLaSemanaId} no existe");
            }

            var listaHorarios = await context.Horarios.Where(h => h.UsuarioId == usuario.Id && h.DiaDeLaSemanaId == horarioCrear.DiaDeLaSemanaId).Select(h => h.Hora).ToListAsync();

            TimeSpan mediaHora = TimeSpan.FromMinutes(30);//tiempo mínimo de intervalo

            bool comparacionMediaHoraDiferencia = listaHorarios.Any(hora => Math.Abs((horarioCrear.Hora - hora).Ticks) < mediaHora.Ticks);

            if (comparacionMediaHoraDiferencia)
            {
                return BadRequest($"La hora ingresada {horarioCrear.Hora} no tiene más de media hora de diferencia con otros del mismo día");
            }

            var horario = mapper.Map<Horario>(horarioCrear);
            horario.UsuarioId = usuario.Id;

            context.Add(horario);
            await context.SaveChangesAsync();
            return Ok();
        }
        #endregion

        #region Put
        [HttpPut("{horarioId}")]
        public async Task<ActionResult> ModificarHorario([FromRoute] Guid horarioId, [FromBody] HorarioModificarDto horarioModificarDto)
        {
            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var usuario = await userManager.FindByNameAsync(Username);


            var horarioExistente = await context.Horarios.FirstOrDefaultAsync(h => h.HorarioId == horarioId);

            if (horarioExistente == null)
            {
                return BadRequest($"El horario con id {horarioId} no existe");
            }

            if (horarioExistente.UsuarioId != usuario.Id)
            {
                return Forbid($"No tienes permisos de modificacion");
            }


            var diadelaSemanaExistente = await context.DiadelaSemana.AnyAsync(d => d.DiadelaSemanaId == horarioModificarDto.DiaDeLaSemanaId);

            if (horarioModificarDto.DiaDeLaSemanaId != null)
            {
                if (!diadelaSemanaExistente)
                {
                    return NotFound($"El Dia de la semana con id {horarioModificarDto.DiaDeLaSemanaId} no existe");
                }
                else
                {
                    horarioExistente.DiaDeLaSemanaId = (int)horarioModificarDto.DiaDeLaSemanaId;
                }
            }

            if (horarioModificarDto.Hora != null)
            {
                horarioExistente.Hora = (DateTime)horarioModificarDto.Hora;
            }

            var listaHorarios = await context.Horarios
                .Where(h => h.UsuarioId == horarioExistente.UsuarioId
                        && h.DiaDeLaSemanaId == horarioExistente.DiaDeLaSemanaId
                        && h.HorarioId != horarioExistente.HorarioId)
                .Select(h => h.Hora).ToListAsync();

            TimeSpan mediaHora = TimeSpan.FromMinutes(30);//tiempo mínimo de intervalo

            bool comparacionMediaHoraDiferencia = listaHorarios.Any(hora => Math.Abs((horarioExistente.Hora - hora).Ticks) < mediaHora.Ticks);

            if (comparacionMediaHoraDiferencia)
            {
                return BadRequest($"La hora ingresada {horarioExistente.Hora} no tiene más de media hora de diferencia con otros del mismo día");
            }

            context.Update(horarioExistente);
            await context.SaveChangesAsync();

            return Ok();
        }
        #endregion
    }
}
