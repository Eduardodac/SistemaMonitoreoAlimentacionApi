using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using SistemaMonitoreoAlimentacionApi.Dtos.Collar;
using SistemaMonitoreoAlimentacionApi.Dtos.Gato;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollaresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CollaresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<List<Collar>>> GetCollares()
        {
            return await context.Collares.ToListAsync();
        }

        [HttpGet("{collarId}")]
        public async Task<ActionResult<Collar>> GetCollar([FromRoute] Guid collarId)
        {
            var collar = await context.Collares.FirstOrDefaultAsync(c => c.CollarId == collarId);

            if(collar == null)
            {
                return new NotFoundResult();
            }

            return collar;

        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult> CrearCollar(CollarEntidadDto collarEntidad)
        { 
            var collarIdExistente = await context.Collares.AnyAsync(c => c.CollarId.Equals(collarEntidad.CollarId));
            if(collarIdExistente == true)
            {
                return BadRequest($"Ya existe un collar con el mismo Id {collarEntidad.CollarId}");
            }

            var collarRegistroExistente = await context.Collares.AnyAsync(c => c.NumeroRegistro.Equals(collarEntidad.NumeroRegistro));
            if (collarIdExistente == true)
            {
                return BadRequest($"Ya existe un collar con el mismo Número de registro {collarEntidad.NumeroRegistro}");
            }

            var collar = mapper.Map<Collar>(collarEntidad);

            context.Add(collar);
            await context.SaveChangesAsync();
            return Ok();

        }
        #endregion
        
    }
}
