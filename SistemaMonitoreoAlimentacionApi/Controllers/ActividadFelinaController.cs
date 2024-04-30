using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Dtos.ActividadFelina;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActividadFelinaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActividadFelinaController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        #region Get
        [HttpGet("{gatoId}")]
        public async Task<ActionResult<List<ActividadFelina>>> ListaActividadesFelinas([FromRoute] Guid gatoId)
        {
            var gatoAsignado = await context.Gatos.FirstOrDefaultAsync(g => g.GatoId.Equals(gatoId));

            if (gatoAsignado == null)
            {
                return NotFound($"Este gato con id {gatoId} no existe");
            }

            return await context.ActividadesFelinas.Where(ac => ac.GatoId.Equals(gatoId)).ToListAsync();
        }
        #endregion
        #region Post
        [HttpPost]
        public async Task<ActionResult> CrearActividadFelina([FromBody] NuevaActividadFelinaDto nuevaActividadFelinaDto) 
        {
            var gatoAsignado = await context.Gatos.FirstOrDefaultAsync(g => g.GatoId.Equals(nuevaActividadFelinaDto.GatoId));

            if (gatoAsignado == null)
            {
                return NotFound($"Este gato con id {nuevaActividadFelinaDto.GatoId} no existe");
            }

            var gato = mapper.Map<ActividadFelina>(nuevaActividadFelinaDto);

            context.Add(gato);
            await context.SaveChangesAsync();
            return Ok();
        }
        #endregion
    }
}
