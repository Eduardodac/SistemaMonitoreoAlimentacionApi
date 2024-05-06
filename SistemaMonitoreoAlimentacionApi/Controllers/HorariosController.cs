﻿using AutoMapper;
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
        public async Task<ActionResult<List<HorarioCrearDto>>> ListaHorarios([FromRoute] Guid usuarioId)
        { 
            var usuarioExistente = await context.Users.FirstOrDefaultAsync(u => u.Id.Equals(usuarioId));

            if(usuarioExistente == null)
            {
                return NotFound($"El usuario con id {usuarioId} no existe");
            }

            var horarios = await context.Horarios.Where(h => h.UsuarioId == usuarioId).ToListAsync();

            var HorariosDto = mapper.Map<List<Horario>, List<HorarioCrearDto>>(horarios);

            return HorariosDto;
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<List<Horario>>> CrearHorarios([FromBody] Horario horario)
        {
            var usuarioExistente = await context.Users.AnyAsync(u => u.Id.Equals(horario.UsuarioId));

            if (!usuarioExistente)
            {
                return NotFound($"El usuario con id {horario.UsuarioId} no existe");
            }

            var horarioExistente = await context.Horarios.AnyAsync(h => h.HorarioId.Equals(horario.HorarioId));

            if (horarioExistente)
            {
                return NotFound($"El horario con id {horario.HorarioId} ya existe");
            }

            var diadelaSemanaExistente = await context.DiadelaSemana.AnyAsync(d => d.DiadelaSemanaId == horario.DiaDeLaSemanaId);

            if (!diadelaSemanaExistente)
            {
                return NotFound($"El Dia de la semana con id {horario.DiaDeLaSemanaId} no existe");
            }

            var listaHorarios = await context.Horarios.Where(h => h.UsuarioId == horario.UsuarioId && h.DiaDeLaSemanaId == horario.DiaDeLaSemanaId).Select(h=>h.Hora).ToListAsync();

            TimeSpan mediaHora = TimeSpan.FromMinutes(30);//tiempo mínimo de intervalo

            bool comparacionMediaHoraDiferencia = listaHorarios.Any(hora => Math.Abs((horario.Hora - hora).Ticks) < mediaHora.Ticks);

            if (comparacionMediaHoraDiferencia)
            {
                return BadRequest($"La hora ingresada {horario.Hora} no tiene más de media hora de diferencia con otros del mismo día");
            }

            context.Add(horario);
            await context.SaveChangesAsync();
            return Ok();
        }
        #endregion

        #region Put
        [HttpPut("{horarioId}")]
        public async Task<ActionResult> ModificarHorario([FromRoute] Guid horarioId, [FromBody] HorarioModificarDto horarioModificarDto)
        { 
            var horarioExistente = await context.Horarios.FirstOrDefaultAsync(h => h.HorarioId == horarioId);

            if (horarioExistente == null)
            {
                return BadRequest($"El horario con id {horarioId} no existe");
            }

            var diadelaSemanaExistente = await context.DiadelaSemana.AnyAsync(d => d.DiadelaSemanaId == horarioModificarDto.DiaDeLaSemanaId);

            if (horarioModificarDto.DiaDeLaSemanaId != null)
            {
                if (!diadelaSemanaExistente)
                {
                    return NotFound($"El Dia de la semana con id {horarioModificarDto.DiaDeLaSemanaId} no existe");
                }
                else {
                    horarioExistente.DiaDeLaSemanaId = (int)horarioModificarDto.DiaDeLaSemanaId;
                }
            }

            if(horarioModificarDto.Hora != null)
            {
                horarioExistente.Hora = (DateTime)horarioModificarDto.Hora;
            }

            var listaHorarios = await context.Horarios
                .Where(h => h.UsuarioId == horarioExistente.UsuarioId 
                        && h.DiaDeLaSemanaId == horarioExistente.DiaDeLaSemanaId
                        && h.HorarioId != horarioExistente.HorarioId)
                .Select(h => h.Hora).ToListAsync();

            TimeSpan mediaHora = TimeSpan.FromMinutes(30);//tiempo mínimo de intervalo

            bool comparacionMediaHoraDiferencia = listaHorarios.Any(hora => Math.Abs((horarioExistente.Hora - hora).Ticks) < mediaHora.Ticks);

            if (comparacionMediaHoraDiferencia)
            {
                return BadRequest($"La hora ingresada {horarioExistente.Hora} no tiene más de media hora de diferencia con otros del mismo día");
            }

            context.Update(horarioExistente);
            await context.SaveChangesAsync();
            
            return Ok();
        }
        #endregion
    }
}
