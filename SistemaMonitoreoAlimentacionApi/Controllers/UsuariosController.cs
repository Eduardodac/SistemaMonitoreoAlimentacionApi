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
        public ActionResult<List<Usuarios>> GetUsuarios()
        {
            return new List<Usuarios>();
        }

        [HttpPost]
        public async Task<ActionResult> PostUsuario(Usuarios usuario)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
