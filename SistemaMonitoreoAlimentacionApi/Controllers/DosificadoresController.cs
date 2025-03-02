using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Dtos.Aviso;
using SistemaMonitoreoAlimentacionApi.Dtos.Dosificador;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DosificadoresController : Controller
    {
        private readonly ApplicationDbContext context;

        public IMapper mapper { get; }

        public DosificadoresController(ApplicationDbContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }
        #region Get
        [HttpGet]
        public async Task<ActionResult<List<Dosificador>>> GetDosificadores()
        {
            return await context.Dosificadores.ToListAsync();
        }

        [HttpGet("{dosificadorId}")]
        public async Task<ActionResult<Dosificador>> GetDosificador([FromRoute] Guid dosificadorId)
        {
            var dosificadorExistente = await context.Dosificadores.FirstOrDefaultAsync(d => d.DosificadorId.Equals(dosificadorId));

            if (dosificadorExistente == null)
            {
                return BadRequest($"El dosificador con id {dosificadorId} no existe");
            }

            return dosificadorExistente;
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult> CrearDosificador([FromBody] DosificadorEntidadDto dosificadorCreacionDto)
        { 
            var dosificadorExistente = await context.Dosificadores
                .AnyAsync(d => d.NumeroRegistro.Equals(dosificadorCreacionDto.NumeroRegistro) || d.DosificadorId.Equals(dosificadorCreacionDto.DosificadorId));

            if(dosificadorExistente)
            {
                return BadRequest($"Ya existe un dosificador con el mismo id {dosificadorCreacionDto.DosificadorId} o con el mismo número de registro {dosificadorCreacionDto.NumeroRegistro}");
            }

            var dosificador = mapper.Map<Dosificador>(dosificadorCreacionDto);

            context.Add(dosificador);
            await context.SaveChangesAsync();
            return Ok();
        }
        #endregion


    }
}
