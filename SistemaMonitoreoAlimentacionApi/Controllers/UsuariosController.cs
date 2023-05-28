using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController (ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> PostUsuario(Usuario usuario)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> PutUsuarios(Usuario usuario, Guid id)
        {
            if(usuario.UsuarioId != id)
            {
                return BadRequest("El id del usuario no coincide con la id de la URL");
            }
            var user = _context.Usuarios.Any(usuario => usuario.UsuarioId == id);
            if (!user)
            {
                return NotFound("El id del usuario no existe");
            }

            _context.Update(usuario);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteUsuarios(Guid id)
        {

            var user = await _context.Usuarios.AnyAsync(usuario => usuario.UsuarioId == id);
            if (!user)
            {
                return NotFound("El id del usuario no existe");
            }

            _context.Remove(new Usuario() { UsuarioId = id});
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
