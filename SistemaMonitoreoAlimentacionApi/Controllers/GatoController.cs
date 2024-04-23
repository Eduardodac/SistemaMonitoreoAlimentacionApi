﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GatoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GatoController(ApplicationDbContext context) { 
            this._context = context;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<List<Gato>>> GetGatos() 
        { 
            return await _context.Gatos.ToListAsync();
        }

        [HttpGet("primero")]
        public async Task<ActionResult<Gato>> GetGato([FromQuery] Guid Id)
        {
            var gato =  await _context.Gatos.FirstOrDefaultAsync(x => x.GatoId == Id);

            if(gato == null)
            {
                return new NotFoundResult();
            }

            return gato;
        }

        #endregion

    }
}
