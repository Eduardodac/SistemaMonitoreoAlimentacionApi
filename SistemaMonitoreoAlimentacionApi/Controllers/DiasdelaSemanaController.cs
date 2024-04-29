using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiasdelaSemanaController : Controller
    {
        private readonly ApplicationDbContext context;

        public DiasdelaSemanaController(ApplicationDbContext context) 
        {
            this.context = context;
        }
        #region Get
        [HttpGet]
        public async Task<ActionResult<List<DiadelaSemana>>> GetDiasdelaSemana ()
        {
            return await context.DiadelaSemana.ToListAsync();
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<ActionResult> NuevoDiadelaSemana([FromBody] DiadelaSemana diadelaSemana)
        {
            var diadelaSemanaItem = new DiadelaSemana
            {
                Dia = diadelaSemana.Dia
            };

            context.Add(diadelaSemanaItem);
            await context.SaveChangesAsync();
            return Ok();
        }
        #endregion

    }
}
