using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GatosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GatosController(ApplicationDbContext context) { 
            this._context = context;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<List<Gato>>> GetGatos() 
        { 
            return await _context.Gatos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Gato>> GetGato(Guid Id)
        {
            var gato =  await _context.Gatos.FirstOrDefaultAsync(x => x.GatoId == Id);

            if(gato == null)
            {
                return new NotFoundResult();
            }

            return gato;
        }

        #endregion

    }
}
