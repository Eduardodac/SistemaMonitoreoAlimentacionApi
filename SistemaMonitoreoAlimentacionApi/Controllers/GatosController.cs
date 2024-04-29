using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Dtos.Collar;
using SistemaMonitoreoAlimentacionApi.Dtos.Gato;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GatosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GatosController(ApplicationDbContext context, IMapper mapper) { 
            this.context = context;
            this.mapper = mapper;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<List<Gato>>> GetGatos() 
        { 
            return await context.Gatos.ToListAsync();
        }

        [HttpGet("{gatoId}")]
        public async Task<ActionResult<Gato>> GetGato([FromRoute]Guid gatoId)
        {

            var gato =  await context.Gatos.FirstOrDefaultAsync(x => x.GatoId == gatoId);

            if(gato == null)
            {
                return new NotFoundResult();
            }

            return gato;
        }

        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult> PostGato([FromBody] GatoCreacionDto gatoCreacionDto)
        {
            var existeMismoGato = await context.Gatos.FirstOrDefaultAsync(x => x.Nombre == gatoCreacionDto.Nombre);

            if(existeMismoGato != null)
            {
                if(existeMismoGato.Raza == gatoCreacionDto.Raza) 
                {
                    return BadRequest($"Ya existe un gato con el mismo nombre {gatoCreacionDto.Nombre} y con la misma raza {gatoCreacionDto.Raza}");
                }
            }

            var gato = mapper.Map<Gato>(gatoCreacionDto);

            context.Gatos.Add(gato);
            await context.SaveChangesAsync();
            return Ok();

        }
        #endregion

        #region Put
        [HttpPut("activarCollar/{gatoId}")]
        public async Task<ActionResult> ActivarCollar([FromRoute] Guid gatoId, [FromBody] ModificarCollarDto modificarCollarDto)
        {
            var collarExistente = await context.Collares.FirstOrDefaultAsync(c => c.NumeroRegistro.Equals(modificarCollarDto.NumeroRegistro));
            if (collarExistente == null)
            {
                return BadRequest($"El collar con número de registro {modificarCollarDto.NumeroRegistro} no existe");
            }

            var gatoExistente = await context.Gatos.FirstOrDefaultAsync(g => g.GatoId.Equals(gatoId));

            if (gatoExistente == null)
            {
                return BadRequest($"El id {gatoId} no existe");
            }


            collarExistente.EstatusActivacion = true;
            collarExistente.FechaActivacion = DateTime.Now;
            gatoExistente.CollarId = collarExistente.CollarId;

            context.Update(collarExistente);
            await context.SaveChangesAsync();

            context.Update(gatoExistente);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("desactivarCollar/{gatoId}")]
        public async Task<ActionResult> DesactivarCollar([FromRoute] Guid gatoId, [FromBody] ModificarCollarDto modificarCollarDto)
        {

            var gatoExistente = await context.Gatos.FirstOrDefaultAsync(g => g.GatoId.Equals(gatoId));

            if (gatoExistente == null)
            {
                return BadRequest($"El id {gatoId} de gato no existe");
            }

            var collarExistente = await context.Collares.FirstOrDefaultAsync(c => c.NumeroRegistro.Equals(modificarCollarDto.NumeroRegistro));

            if (collarExistente == null)
            {
                return BadRequest($"El collar con número de registro {modificarCollarDto.NumeroRegistro} no existe");
            }

            collarExistente.EstatusActivacion = false;
            collarExistente.FechaActivacion = null;
            gatoExistente.CollarId = null;

            context.Update(collarExistente);
            await context.SaveChangesAsync();

            context.Update(gatoExistente);
            await context.SaveChangesAsync();

            return Ok();
        }



            #endregion
        }
}
