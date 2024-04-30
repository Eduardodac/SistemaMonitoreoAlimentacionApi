using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Dtos.Aviso;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvisosController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AvisosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        #region Get
        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<Aviso>> GetAviso([FromRoute] Guid usuarioId) 
        { 
            var aviso = await context.Avisos.FirstOrDefaultAsync(a => a.UsuarioId == usuarioId);

            if(aviso == null)
            {
                return NotFound($"Los avisos del usuario con id {usuarioId} no existen");
            }
            return Ok();
        }
        #endregion
        #region Post
        [HttpPost]
        public async Task<ActionResult> NuevoAviso([FromBody] NuevoAvisoDto nuevoAvisoDto) 
        {
            var avisoExistente = await context.Avisos.AnyAsync(a => a.AvisoId.Equals(nuevoAvisoDto.AvisoId));
            if (avisoExistente == true) {
                return BadRequest($"El aviso con id {nuevoAvisoDto.AvisoId} ya existe");
            }

            var aviso = mapper.Map<Aviso>(avisoExistente);

            context.Add(aviso);
            await context.SaveChangesAsync();

            return Ok();
        }
        #endregion
        #region Put
        [HttpPut("{avisoId}")]
        public async Task<ActionResult> NuevoAviso([FromRoute] Guid avisoId,[FromBody] ModificarAvisoDto modificarAvisoDto)
        {
            var avisoExistente = await context.Avisos.FirstOrDefaultAsync(a => a.AvisoId.Equals(avisoId));
            if (avisoExistente == null)
            {
                return BadRequest($"El aviso con id {avisoId} no existe");
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

        [HttpPut("disponibilidad/{dosificadorId}")]
        public async Task<ActionResult> DisponibilidadAlimento([FromRoute] Guid dosificadorId, [FromBody] DisponibilidadAvisoDto disponibilidadAvisoDto)
        {
            var usuarioExistente = await context.Usuarios.FirstOrDefaultAsync(u => u.DosificadorId == dosificadorId);

            if (usuarioExistente == null)
            {
                return BadRequest($"El dosificador con id {dosificadorId} no está asignado");
            }

            var avisoExistente = await context.Avisos.FirstOrDefaultAsync(a => a.UsuarioId == usuarioExistente.UsuarioId);
            if (avisoExistente == null)
            {
                return BadRequest($"El usuario con id {usuarioExistente.UsuarioId} no tiene avisos");
            }

            avisoExistente.AlimentoDisponible = disponibilidadAvisoDto.AlimentoDisponible;

            context.Update(avisoExistente);
            await context.SaveChangesAsync();

            return Ok();
        }
        #endregion
    }
}
