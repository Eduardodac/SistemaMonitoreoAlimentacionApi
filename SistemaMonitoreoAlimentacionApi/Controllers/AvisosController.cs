using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Dtos.Aviso;
using SistemaMonitoreoAlimentacionApi.Entidades;

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
        public async Task<ActionResult<Aviso>> GetAviso() 
        {
            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var usuario = await userManager.FindByNameAsync(Username);

            var aviso = await context.Avisos.FirstOrDefaultAsync(a => a.UsuarioId == usuario.Id);

            if(aviso == null)
            {
                return NotFound($"Los avisos del usuario con id {usuario.Id} no existen");
            }
            return Ok();
        }
        #endregion
        #region Post
        [HttpPost]
        public async Task<ActionResult> NuevoAviso([FromBody] NuevoAvisoDto nuevoAvisoDto) 
        {
            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var usuario = await userManager.FindByNameAsync(Username);

            var avisoExistente = await context.Avisos.AnyAsync(a => a.AvisoId.Equals(nuevoAvisoDto.AvisoId));
            if (avisoExistente == true) {
                return BadRequest($"El aviso con id {nuevoAvisoDto.AvisoId} ya existe");
            }

            var aviso = mapper.Map<Aviso>(avisoExistente);
            aviso.UsuarioId = usuario.Id;

            context.Add(aviso);
            await context.SaveChangesAsync();

            return Ok();
        }
        #endregion
        #region Put
        [HttpPut("{avisoId}")]
        public async Task<ActionResult> NuevoAviso([FromRoute] Guid avisoId,[FromBody] ModificarAvisoDto modificarAvisoDto)
        {

            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var usuario = await userManager.FindByNameAsync(Username);

            var avisoExistente = await context.Avisos.FirstOrDefaultAsync(a => a.AvisoId.Equals(avisoId));
            if (avisoExistente == null)
            {
                return BadRequest($"El aviso con id {avisoId} no existe");
            }

            if(avisoExistente.UsuarioId != usuario.Id)
            {
                return Forbid($"No tienes permisos de modificacion");
            }

            if(modificarAvisoDto.LimpiarPlato != new DateTime(2000, 1, 1))
            {
                avisoExistente.LimpiarPlato = modificarAvisoDto.LimpiarPlato;
            }

            if (modificarAvisoDto.Caducidad != new DateTime(2000, 1, 1))
            {
                avisoExistente.Caducidad = modificarAvisoDto.Caducidad;
            }

            if (modificarAvisoDto.LimpiarContenedor != new DateTime(2000, 1, 1))
            {
                avisoExistente.LimpiarContenedor = modificarAvisoDto.LimpiarContenedor;
            }

            context.Update(avisoExistente);
            await context.SaveChangesAsync();

            return Ok();
        }
        #endregion
    }
}
