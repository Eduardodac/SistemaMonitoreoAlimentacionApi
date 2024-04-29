using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Dtos.Gato;
using SistemaMonitoreoAlimentacionApi.Dtos.Usuario;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UsuariosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            return await context.Usuarios.ToListAsync();
        }
        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<Usuario>> GetUsuario([FromRoute] Guid usuarioId)
        { 
            var usuarioExistente = await context.Usuarios.AnyAsync(u => u.UsuarioId == usuarioId);

            if(!usuarioExistente)
            {
                return BadRequest($"El usuario con id {usuarioId} no existe");
            }

            return Ok();
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult> PostUsuario(Usuario usuario)
        {
            context.Add(usuario);
            await context.SaveChangesAsync();
            return Ok();
        }
        #endregion

        #region Put
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> PutUsuarios(Usuario usuario, Guid id)
        {
            if (usuario.UsuarioId != id)
            {
                return BadRequest("El id del usuario no coincide con la id de la URL");
            }
            var user = context.Usuarios.Any(usuario => usuario.UsuarioId == id);
            if (!user)
            {
                return NotFound("El id del usuario no existe");
            }

            context.Update(usuario);
            await context.SaveChangesAsync();
            return Ok();

        }

        [HttpPut("activarDosificador/{usuarioId}")]
        public async Task<ActionResult> ActivarDosificador([FromRoute] Guid usuarioId, [FromBody] ModificarDosificadorDto modificarDosificadorDto)
        {
            var dosificadorExistente = await context.Dosificadores.FirstOrDefaultAsync(c => c.NumeroRegistro.Equals(modificarDosificadorDto.NumeroRegistro));
            if (dosificadorExistente == null)
            {
                return BadRequest($"El dosificador con número de registro {modificarDosificadorDto.NumeroRegistro} no existe");
            }

            var usuarioExistente = await context.Usuarios.FirstOrDefaultAsync(g => g.UsuarioId.Equals(usuarioId));

            if (usuarioExistente == null)
            {
                return BadRequest($"El id {usuarioId} no existe");
            }


            dosificadorExistente.EstatusActivacion = true;
            dosificadorExistente.FechaActivacion = DateTime.Now;
            usuarioExistente.DosificadorId = dosificadorExistente.DosificadorId;

            context.Update(dosificadorExistente);
            await context.SaveChangesAsync();

            context.Update(usuarioExistente);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("desactivarDosificador/{usuarioId}")]
        public async Task<ActionResult> DesctivarDosificador([FromRoute] Guid usuarioId, [FromBody] ModificarDosificadorDto modificarDosificadorDto)
        {
            var dosificadorExistente = await context.Dosificadores.FirstOrDefaultAsync(c => c.NumeroRegistro.Equals(modificarDosificadorDto.NumeroRegistro));
            if (dosificadorExistente == null)
            {
                return BadRequest($"El dosificador con número de registro {modificarDosificadorDto.NumeroRegistro} no existe");
            }

            var usuarioExistente = await context.Usuarios.FirstOrDefaultAsync(g => g.UsuarioId.Equals(usuarioId));

            if (usuarioExistente == null)
            {
                return BadRequest($"El id {usuarioId} no existe");
            }


            dosificadorExistente.EstatusActivacion = false;
            dosificadorExistente.FechaActivacion = null;
            usuarioExistente.DosificadorId = null;

            context.Update(dosificadorExistente);
            await context.SaveChangesAsync();

            context.Update(usuarioExistente);
            await context.SaveChangesAsync();

            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteUsuarios(Guid id)
        {

            var user = await context.Usuarios.AnyAsync(usuario => usuario.UsuarioId == id);
            if (!user)
            {
                return NotFound("El id del usuario no existe");
            }

            context.Remove(new Usuario() { UsuarioId = id });
            await context.SaveChangesAsync();
            return Ok();

        }
        #endregion
    }
}
