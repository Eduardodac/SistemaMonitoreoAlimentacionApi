using AutoMapper;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using SistemaMonitoreoAlimentacionApi.Dtos.Registro;
using SistemaMonitoreoAlimentacionApi.Entidades;
using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrosController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RegistrosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        #region Get
        [HttpGet("{collarId}")]
        public async Task<ActionResult<List<Registro>>> ListaActividadesFelinas([FromRoute] Guid collarId)
        {
            var gatoExistente = await context.Gatos.FirstOrDefaultAsync(g => g.CollarId.Equals(collarId));

            if (gatoExistente == null)
            {
                return NotFound($"Este collar con id {collarId} no está asignado");
            }

            return await context.Registros.Where(ac => ac.CollarId.Equals(collarId)).ToListAsync();
        }
        #endregion

        #region Post
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Registrar([FromBody] NuevoRegistroDto nuevoRegistroDto)
        {
            var gatoAsignado = await context.Gatos.FirstOrDefaultAsync(g => g.CollarId.Equals(nuevoRegistroDto.CollarId));

            if (gatoAsignado == null)
            {
                return NotFound($"Este collar con id {nuevoRegistroDto.CollarId} no está asignado");
            }

            var usuarioAsignado = await context.Users.FirstOrDefaultAsync(g => g.DosificadorId.Equals(nuevoRegistroDto.DosificadorId));

            if (usuarioAsignado == null)
            {
                return NotFound($"Este dosificador con id {nuevoRegistroDto.CollarId} no está asignado");
            }

            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");
            DateTime horaUtc = DateTime.UtcNow;
            DateTime horaLocal = TimeZoneInfo.ConvertTimeFromUtc(horaUtc, zonaHoraria);

            var nuevoRegistro = new Registro();
            nuevoRegistro.RegistroId = Guid.NewGuid();
            nuevoRegistro.DosificadorId = nuevoRegistroDto.DosificadorId;
            nuevoRegistro.CollarId = nuevoRegistroDto.CollarId;
            nuevoRegistro.Duracion = nuevoRegistroDto.Duracion;
            nuevoRegistro.Consumo = nuevoRegistroDto.Consumo;
            nuevoRegistro.Hora = horaLocal;
            nuevoRegistro.IntegradoAAnalisis = false;

            var registro = mapper.Map<Registro>(nuevoRegistro);

            context.Add(registro);
            await context.SaveChangesAsync();

            return Ok();
        }

        #endregion
    }
}
