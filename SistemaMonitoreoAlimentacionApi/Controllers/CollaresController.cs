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
        [HttpPost("crear")]
        public async Task<ActionResult> CrearCollar(CollarCreacionDto collarCreacionDto)
        { 
            var collarIdExistente = await context.Collares.AnyAsync(c => c.CollarId.Equals(collarCreacionDto.CollarId));
            if(collarIdExistente == true)
            {
                return BadRequest($"Ya existe un collar con el mismo Id {collarCreacionDto.CollarId}");
            }

            var collarRegistroExistente = await context.Collares.AnyAsync(c => c.NumeroRegistro.Equals(collarCreacionDto.NumeroRegistro));
            if (collarIdExistente == true)
            {
                return BadRequest($"Ya existe un collar con el mismo Número de registro {collarCreacionDto.NumeroRegistro}");
            }

            var collar = mapper.Map<Collar>(collarCreacionDto);

            context.Add(collar);
            await context.SaveChangesAsync();
            return Ok();

        }
        #endregion
        #region Put
        [HttpPut("activar/{NumeroRegistro}")]
        public async Task<ActionResult> ActivarCollar([FromRoute] string NumeroRegistro, [FromBody] CollarActivarDto collarActivarDto)
        {
            var collarExistente = await context.Collares.FirstOrDefaultAsync(c => c.NumeroRegistro.Equals(NumeroRegistro));
            if (collarExistente == null)
            {
                return BadRequest($"El collar con número de registro {NumeroRegistro} no existe");
            }

            var gatoExistente = await context.Gatos.AnyAsync(g => g.GatoId.Equals(collarActivarDto.GatoId));

            if (!gatoExistente)
            {
                return BadRequest($"El id {collarActivarDto.GatoId} no existe");
            }

            if(collarExistente.EstatusActivacion)
            {
                return BadRequest($"El collar ya ha sido activado, dos gatos no pueden estar sujetos al mismo collar");
            }

            collarExistente.EstatusActivacion = true;
            collarExistente.FechaActivacion = DateTime.Now;
            collarExistente.GatoId = collarActivarDto.GatoId;

            context.Update(collarExistente);
            await context.SaveChangesAsync();

            return Ok();


        }

        [HttpPut("desactivar/{NumeroRegistro}")]
        public async Task<ActionResult> DesactivarCollar([FromRoute] string NumeroRegistro, [FromBody] CollarDesactivarDto collarDesactivarDto)
        { 
            

            var gatoExistente = await context.Gatos.FirstOrDefaultAsync(g => g.GatoId.Equals(collarDesactivarDto.GatoId));

            if (gatoExistente == null)
            {
                return BadRequest($"El id {collarDesactivarDto.GatoId} de gato no existe");
            }

            var usuarioExistente = await context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioId.Equals(collarDesactivarDto.UsuarioId));

            if(usuarioExistente == null)
            {
                return BadRequest($"El id {collarDesactivarDto.UsuarioId} de usuario no existe");
            }

            if (gatoExistente.UsuarioId != usuarioExistente.UsuarioId)
            {
                return BadRequest($"No tienes acceso a este collar");
            }
            
            var collarExistente = await context.Collares.FirstOrDefaultAsync(c => c.NumeroRegistro.Equals(NumeroRegistro));

            if (collarExistente == null)
            {
                return BadRequest($"El collar con número de registro {NumeroRegistro} no existe");
            }

            collarExistente.EstatusActivacion = false;
            collarExistente.FechaActivacion = null;
            collarExistente.GatoId = null;

            context.Update(collarExistente);
            await context.SaveChangesAsync();

            return Ok();


        }
        #endregion
    }
}
