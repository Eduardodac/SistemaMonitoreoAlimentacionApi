using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using SistemaMonitoreoAlimentacionApi.Dtos.ActividadFelina;
using SistemaMonitoreoAlimentacionApi.Dtos.Gato;
using SistemaMonitoreoAlimentacionApi.Entidades;
using System.Collections;
using System.Collections.Generic;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ActividadFelinaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<Usuario> userManager;

        public ActividadFelinaController(ApplicationDbContext context, IMapper mapper,
            UserManager<Usuario> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
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

        #region Put
        [HttpPut]
        public async Task<ActionResult> ActualizarActividades()
        {
            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var usuario = await userManager.FindByNameAsync(Username);

            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var listaIdGatoCollar = await context.Gatos.Where(c => c.CollarId != null).Select(g => new { g.GatoId, g.CollarId }).ToListAsync();

                var listaIdCollares = listaIdGatoCollar.Select(gc => gc.CollarId);

                var registrosPorCollar = await context.Registros
                    .Where(c => listaIdCollares.Contains(c.CollarId) && c.DosificadorId == usuario.DosificadorId && c.IntegradoAAnalisis == false)
                    .ToListAsync();

                var FechaMasAntigua = DateTime.Now;

                if (registrosPorCollar.Count() != 0)
                {
                    FechaMasAntigua = registrosPorCollar.Select(r => r.Hora).Min().AddDays(-1);
                }

                var ActividadesAnteriores = await context.ActividadesFelinas
                    .AsNoTracking() // Evita que Entity Framework Core rastree estas entidades
                    .Where(a => a.FechaHora >= FechaMasAntigua)
                    .ToListAsync();

                var registrosPorGato = registrosPorCollar
                    .Join(listaIdGatoCollar,
                        porCollar => porCollar.CollarId,
                        gatoCollar => gatoCollar.CollarId,
                        (porCollar, gatoCollar) => new {
                            gatoCollar.GatoId,
                            porCollar.Duracion,
                            porCollar.Consumo,
                            porCollar.Hora
                        })
                    .ToList();

                var ListaAgrupada = registrosPorGato
                    .GroupBy(obj => new { obj.GatoId, obj.Hora.Date, obj.Hora.Hour })
                    .Select(grupo => grupo.ToList())
                    .ToList();

                var listaAgrupadaPorFechaYHoraYGatoId = ListaAgrupada
                    .SelectMany(grupo => grupo)
                    .GroupBy(obj => new { obj.GatoId, obj.Hora.Date, obj.Hora.Hour })
                    .Select(grupo => grupo.ToList())
                    .ToList();

                List<ActividadFelina> actividadesNuevas = listaAgrupadaPorFechaYHoraYGatoId.Select(lista => new ActividadFelina
                {
                    ActividadFelinaId = Guid.NewGuid(), // Asegúrate de que sea único
                    GatoId = lista.First().GatoId,
                    FechaHora = new DateTime(lista.First().Hora.Year, lista.First().Hora.Month, lista.First().Hora.Day, lista.First().Hora.Hour, 0, 0, 0),
                    Tiempo = lista.Sum(r => r.Duracion),
                    AlimentoConsumido = lista.Sum(r => r.Consumo)
                }).ToList();

                var actividadesRepetidas = (from a in ActividadesAnteriores
                                            join b in actividadesNuevas
                                            on new { a.FechaHora, a.GatoId } equals new { b.FechaHora, b.GatoId }
                                            select new ActividadFelina
                                            {
                                                ActividadFelinaId = a.ActividadFelinaId,
                                                GatoId = a.GatoId,
                                                FechaHora = a.FechaHora,
                                                Tiempo = a.Tiempo + b.Tiempo,
                                                AlimentoConsumido = a.AlimentoConsumido + b.AlimentoConsumido
                                            })
                                            .GroupBy(a => a.ActividadFelinaId) // Evita duplicados
                                            .Select(g => g.First())
                                            .ToList();

                var actividadesUnicas = (from b in actividadesNuevas
                                         where !ActividadesAnteriores.Any(a => a.FechaHora == b.FechaHora && a.GatoId == b.GatoId)
                                         select b).ToList();

                // Actualiza actividades repetidas
                foreach (var actividad in actividadesRepetidas)
                {
                    context.Attach(actividad);
                    context.Entry(actividad).State = EntityState.Modified;
                }
                await context.SaveChangesAsync();

                // Agrega actividades únicas
                context.ActividadesFelinas.AddRange(actividadesUnicas);
                await context.SaveChangesAsync();

                // Cambia registros a registros analizados
                registrosPorCollar.ForEach(r => r.IntegradoAAnalisis = true);
                context.Registros.UpdateRange(registrosPorCollar);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                return BadRequest();
            }

            return Ok();
        }
        #endregion
    }
}
