using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Dtos.Gato;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GatosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GatosController(ApplicationDbContext context, IMapper mapper) { 
            this._context = context;
            this._mapper = mapper;
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

        #region Post
        [HttpPost]
        public async Task<ActionResult> PostGato([FromBody] GatoCreacionDto gatoCreacionDto)
        {
            var existeMismoGato = await _context.Gatos.FirstOrDefaultAsync(x => x.Nombre == gatoCreacionDto.Nombre);

            if(existeMismoGato != null)
            {
                if(existeMismoGato.Raza == gatoCreacionDto.Raza) 
                {
                    return BadRequest($"Ya existe un gato con el mismo nombre {gatoCreacionDto.Nombre} y con la misma raza {gatoCreacionDto.Raza}");
                }
            }

            var gato = _mapper.Map<Gato>(gatoCreacionDto);

            _context.Gatos.Add(gato);
            await _context.SaveChangesAsync();
            return Ok();

        }
        #endregion
    }
}
