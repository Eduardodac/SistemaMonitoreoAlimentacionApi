using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Dtos.Aviso;
using SistemaMonitoreoAlimentacionApi.Dtos.Gato;
using SistemaMonitoreoAlimentacionApi.Entidades;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AvisosController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<Usuario> userManager;

        public AvisosController(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<NuevoAvisoDto>> GetAviso() 
        {
            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var usuario = await userManager.FindByNameAsync(Username);

            var aviso = await context.Avisos.FirstOrDefaultAsync(a => a.UsuarioId == usuario.Id);

            if(aviso == null)
            {
                NuevoAvisoDto nuevoAviso = new NuevoAvisoDto();
                nuevoAviso.AvisoId = new Guid();

                nuevoAviso.AlimentoDisponible = 0;
                nuevoAviso.LimpiarPlato = new DateTime(2000, 1, 1);
                nuevoAviso.LimpiarContenedor = new DateTime(2000, 1, 1);
                nuevoAviso.Caducidad = new DateTime(2000, 1, 1);

                var avisoDto = mapper.Map<Aviso>(nuevoAviso);
                avisoDto.UsuarioId = usuario.Id;

                context.Add(avisoDto);
                await context.SaveChangesAsync();
                return mapper.Map<Aviso, NuevoAvisoDto>(avisoDto);
            }

            var avisoD = mapper.Map<Aviso, NuevoAvisoDto>(aviso);
            return avisoD;
                
        }
        #endregion

        #region Put
        [HttpPut]
        public async Task<ActionResult> NuevoAviso([FromBody] ModificarAvisoDto modificarAvisoDto)
        {

            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var usuario = await userManager.FindByNameAsync(Username);

            var avisoExistente = await context.Avisos.FirstOrDefaultAsync(a => a.UsuarioId.Equals(usuario.Id));

            if (avisoExistente == null)
            {
                NuevoAvisoDto nuevoAviso = new NuevoAvisoDto();
                nuevoAviso.AvisoId = new Guid();
                nuevoAviso.AlimentoDisponible = 0;
                nuevoAviso.LimpiarPlato = modificarAvisoDto.LimpiarPlato;
                nuevoAviso.LimpiarContenedor = modificarAvisoDto.LimpiarContenedor;
                nuevoAviso.Caducidad = modificarAvisoDto.Caducidad;

                var avisoDto = mapper.Map<Aviso>(nuevoAviso);
                avisoDto.UsuarioId = usuario.Id;

                context.Add(avisoDto);
                await context.SaveChangesAsync();
                return Ok();
            }

            if (avisoExistente.UsuarioId != usuario.Id)
            {
                return Forbid($"No tienes permisos de modificacion");
            }

            if(modificarAvisoDto.LimpiarPlato.Date != new DateTime(2000, 1, 1).Date)
            {
                avisoExistente.LimpiarPlato = modificarAvisoDto.LimpiarPlato;
            }

            if (modificarAvisoDto.Caducidad.Date != new DateTime(2000, 1, 1).Date)
            {
                avisoExistente.Caducidad = modificarAvisoDto.Caducidad;
            }

            if (modificarAvisoDto.LimpiarContenedor.Date != new DateTime(2000, 1, 1).Date)
            {
                avisoExistente.LimpiarContenedor = modificarAvisoDto.LimpiarContenedor;
            }

            context.Update(avisoExistente);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("modificarAlimentoDisponible/{dosificadorId}")]
        [AllowAnonymous]
        public async Task<ActionResult> ModificarAlimentoDisponible([FromRoute] Guid dosificadorId, [FromBody] ModificarAlimentoDisponibleDto modificarAlimentoDisponible)
        {
            //obtener usuario
            var user = await context.Users.Where(u => u.DosificadorId == dosificadorId).FirstOrDefaultAsync();

            if (user == null)
            {
                return BadRequest();
            }
            var avisoExistente = await context.Avisos.FirstOrDefaultAsync(a => a.UsuarioId.Equals(user.Id));

            if (avisoExistente == null)
            {
                NuevoAvisoDto nuevoAviso = new NuevoAvisoDto();
                nuevoAviso.AvisoId = new Guid();
                nuevoAviso.AlimentoDisponible = 0;
                nuevoAviso.LimpiarPlato = new DateTime(2000, 1, 1);
                nuevoAviso.LimpiarContenedor = new DateTime(2000, 1, 1);
                nuevoAviso.Caducidad = new DateTime(2000, 1, 1);

                var avisoDto = mapper.Map<Aviso>(nuevoAviso);
                avisoDto.UsuarioId = user.Id;

                context.Add(avisoDto);
                await context.SaveChangesAsync();
                return Ok();
            }

            int porcentaje=10;
            if (modificarAlimentoDisponible.distancia <= 9.5)
            {
                porcentaje = 100;
            }else if(modificarAlimentoDisponible.distancia > 9.5 && modificarAlimentoDisponible.distancia <= 18.5)
            {
                var per = (modificarAlimentoDisponible.distancia - 9.5) * 10;
                porcentaje = (int)per;
            }
            else
            {
                porcentaje = 10;
            }

            avisoExistente.AlimentoDisponible = porcentaje;
            context.Update(avisoExistente);
            await context.SaveChangesAsync();

            return Ok();

        }
        #endregion
    }
}
