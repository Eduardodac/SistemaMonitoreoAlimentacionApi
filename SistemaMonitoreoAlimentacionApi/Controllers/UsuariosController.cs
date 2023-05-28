using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<List<Usuario>> GetUsuarios()
        {
            return new List<Usuario>();
        }

        [HttpPost]
        public async Task<ActionResult> PostUsuario(Usuario usuario)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
