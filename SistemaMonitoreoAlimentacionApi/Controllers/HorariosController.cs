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

        [HttpGet("{dosificadorId}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<HorarioEntidadDto>>> ListaHorarioPorDosificador([FromRoute] Guid dosificadorId)
        {
            //obtener usuario
            var UserId = await context.Users.Where(u=>u.DosificadorId == dosificadorId).FirstOrDefaultAsync();

            if (UserId == null)
            {
                return NotFound($"Este dosificador no está asignado");
            }

            var horarios = await context.Horarios.Where(h => h.UsuarioId == UserId.Id).ToListAsync();
            var HorariosDto = mapper.Map<List<Horario>, List<HorarioEntidadDto>>(horarios);

            return HorariosDto;
        }

        [HttpGet("confirmarDosificacion/{dosificadorId}")]
        [AllowAnonymous]
        public async Task<ActionResult<ConfirmarDosificacionDto>> confirmarDosificacion([FromRoute] Guid dosificadorId)
        {
            //obtener usuario
            var UserId = await context.Users.Where(u => u.DosificadorId == dosificadorId).FirstOrDefaultAsync();
            
            TimeSpan unMinuto = TimeSpan.FromMinutes(1);//tiempo mínimo de intervalo

            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");
            DateTime horaUtc = DateTime.UtcNow;
            DateTime horaLocal = TimeZoneInfo.ConvertTimeFromUtc(horaUtc, zonaHoraria);


            var ahora = horaLocal.AddMinutes(-1);
            
            var confirmar = new ConfirmarDosificacionDto();
            confirmar.dosificar = false;
            confirmar.habilitado = false;
            confirmar.Hora = ahora.AddMinutes(1);

            if (UserId == null)
            {
                return confirmar;
            }

            confirmar.habilitado = true;
            var diaDeLaSemana = (int)DateTime.Now.DayOfWeek + 1; // se suma para que el id coincida
            var horarios = await context.Horarios.Include(h => h.DiadelaSemana).Where(horario => horario.DiadelaSemana.DiadelaSemanaId == diaDeLaSemana).ToListAsync();

            var siguienteHorario = horarios.Where(hora => hora.Hora.TimeOfDay.CompareTo(ahora.TimeOfDay) >= 0).FirstOrDefault();

            if (siguienteHorario == null)
            {
                confirmar.dosificar = false;
                return confirmar;
            }

            var minutosRestantes = Math.Abs((siguienteHorario.Hora.TimeOfDay - ahora.AddMinutes(1).TimeOfDay).TotalMinutes);

            if (minutosRestantes < .5)
            {
                confirmar.dosificar = true;
            }

            return confirmar;
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
                return BadRequest($"La hora ingresada {horarioCrear.Hora.Hour}:{horarioCrear.Hora.Minute} no tiene más de media hora de diferencia con otros del mismo día");
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

            return Ok(new { horarioId= horarioExistente.HorarioId, hora= horarioExistente.Hora });
        }
        #endregion

        #region Delete
        [HttpDelete("{horarioId}")]
        public async Task<ActionResult> EliminarHorario([FromRoute] Guid horarioId)
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

            context.Remove(horarioExistente);
            await context.SaveChangesAsync();

            return Ok();
        }
        #endregion
    }
}
