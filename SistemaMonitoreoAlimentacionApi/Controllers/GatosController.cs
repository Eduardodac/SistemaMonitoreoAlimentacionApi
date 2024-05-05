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
                return NotFound($"El gato con id {gatoId} no existe");
            }

            return gato;
        }

        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult> PostGato([FromBody] GatoCreacionDto gatoCreacionDto)
        {
            var gatoIdExistente = await context.Gatos.AnyAsync(x => x.GatoId == gatoCreacionDto.GatoId);
            var existeMismoGato = await context.Gatos.FirstOrDefaultAsync(x => x.Nombre == gatoCreacionDto.Nombre);

            if(gatoIdExistente)
            {
                return BadRequest($"Ya existe un gato con la misma Id {gatoCreacionDto.GatoId}");
            }

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
        [HttpPut("{gatoId}")]
        public async Task<ActionResult> ModificarGato([FromRoute] Guid gatoId, [FromBody] ModificarGatoDto modificarGatoDto)
        {
            var gatoExistente = await context.Gatos.FirstOrDefaultAsync(g => g.GatoId.Equals(gatoId));

            if (gatoExistente == null)
            {
                return BadRequest($"El id {gatoId} no existe");
            }

            if(modificarGatoDto.Nombre != null && modificarGatoDto.Nombre != "")
            {
                gatoExistente.Nombre = modificarGatoDto.Nombre;
            }

            if (modificarGatoDto.Raza != null && modificarGatoDto.Raza != "")
            {
                gatoExistente.Raza = modificarGatoDto.Raza;
            }

            if (modificarGatoDto.Sexo != null && modificarGatoDto.Sexo != "")
            {
                gatoExistente.Sexo = modificarGatoDto.Sexo;
            }

            if (modificarGatoDto.Edad != null && modificarGatoDto.Edad < 0)
            {
                gatoExistente.Edad = modificarGatoDto.Edad;
            }

            context.Update(gatoExistente);
            await context.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("activarCollar/{gatoId}")]
        public async Task<ActionResult> ActivarCollar([FromRoute] Guid gatoId, [FromBody] ModificarCollarDto modificarCollarDto)
        {
            var collarExistente = await context.Collares.FirstOrDefaultAsync(c => c.NumeroRegistro.Equals(modificarCollarDto.NumeroRegistro));
            if (collarExistente == null)
            {
                return BadRequest($"El collar con número de registro {modificarCollarDto.NumeroRegistro} no existe");
            }

            if (collarExistente.EstatusActivacion)
            {
                return BadRequest($"El collar con número de registro {modificarCollarDto.NumeroRegistro} ya está activado");
            }

            var gatoExistente = await context.Gatos.FirstOrDefaultAsync(g => g.GatoId.Equals(gatoId));

            if (gatoExistente == null)
            {
                return BadRequest($"El gato con id {gatoId} no existe");
            }

            if (gatoExistente.CollarId != null)
            {
                return BadRequest($"El gato con id {gatoId} ya tiene un collar acivado");
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

            if (!collarExistente.EstatusActivacion)
            {
                return BadRequest($"El collar con número de registro {modificarCollarDto.NumeroRegistro} ya está desactivado");
            }

            if(gatoExistente.CollarId != collarExistente.CollarId)
            {
                return BadRequest($"El collar con número de registro {modificarCollarDto.NumeroRegistro} no está ligado al gato con id {gatoId}");
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
