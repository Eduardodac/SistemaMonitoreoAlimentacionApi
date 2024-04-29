using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Dtos.Horario;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorariosController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public HorariosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        #region Get
        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<List<HorarioDto>>> ListaHorarios([FromRoute] Guid usuarioId)
        { 
            var usuarioExistente = await context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioId.Equals(usuarioId));

            if(usuarioExistente == null)
            {
                return NotFound($"El usuario con id {usuarioId} no existe");
            }

            var horarios = await context.Horarios.Where(h => h.UsuarioId == usuarioId).ToListAsync();

            var HorariosDto = mapper.Map<List<Horario>, List<HorarioDto>>(horarios);

            return HorariosDto;
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<List<Horario>>> CrearHorarios([FromBody] Horario horario)
        {
            var usuarioExistente = await context.Usuarios.AnyAsync(u => u.UsuarioId.Equals(horario.UsuarioId));

            if (!usuarioExistente)
            {
                return NotFound($"El usuario con id {horario.UsuarioId} no existe");
            }

            var diadelaSemanaExistente = await context.DiadelaSemana.AnyAsync(d => d.DiadelaSemanaId == horario.DiaDeLaSemanaId);

            if (!diadelaSemanaExistente)
            {
                return NotFound($"El Dia de la semana con id {horario.DiaDeLaSemanaId} no existe");
            }

            context.Add(horario);
            await context.SaveChangesAsync();
            return Ok();
        }
        #endregion
    }
}
