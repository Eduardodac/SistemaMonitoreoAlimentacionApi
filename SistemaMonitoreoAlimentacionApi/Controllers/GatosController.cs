using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Dtos.Collar;
using SistemaMonitoreoAlimentacionApi.Dtos.Gato;
using SistemaMonitoreoAlimentacionApi.Dtos.Horario;
using SistemaMonitoreoAlimentacionApi.Entidades;
using SistemaMonitoreoAlimentacionApi.Servicios;
using static System.Net.Mime.MediaTypeNames;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GatosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<Usuario> userManager;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "gatos";

        public GatosController(ApplicationDbContext context, 
            IMapper mapper, 
            UserManager<Usuario> userManager,
            IAlmacenadorArchivos almacenadorArchivos) { 
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<List<GatoEntidadDto>>> GetGatos() 
        {
            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var usuario = await userManager.FindByNameAsync(Username);

            var gatos = await context.Gatos
                .Include(g=>g.Collar)
                .Where(h => h.UsuarioId == usuario.Id)
                .ToListAsync();

            var gatosDto = mapper.Map<List<Gato>, List<GatoEntidadDto>>(gatos);

            return gatosDto;
        }

        [HttpGet("{gatoId}")]
        public async Task<ActionResult<Gato>> GetGato([FromRoute]Guid gatoId)
        {

            var gato =  await context.Gatos
                .Include(g =>g.Collar)
                .FirstOrDefaultAsync(x => x.GatoId == gatoId);

            if(gato == null)
            {
                return NotFound($"El gato con id {gatoId} no existe");
            }

            return gato;
        }

        #endregion

        #region Post
        [HttpPost("{gatoId}")]
        public async Task<ActionResult> PostGato([FromRoute] Guid gatoId, [FromBody] GatoEntidadDto gatoEntidad)
        {
            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var usuario = await userManager.FindByNameAsync(Username);

            var gatoIdExistente = await context.Gatos.AnyAsync(x => x.GatoId == gatoId);
            var existeMismoGato = await context.Gatos.FirstOrDefaultAsync(x => x.Nombre == gatoEntidad.Nombre);

            if(gatoIdExistente)
            {
                return BadRequest($"Ya existe un gato con la misma Id {gatoId}");
            }

            if(existeMismoGato != null)
            {
                if(existeMismoGato.Raza == gatoEntidad.Raza) 
                {
                    return BadRequest($"Ya existe un gato con el mismo nombre {gatoEntidad.Nombre} y con la misma raza {gatoEntidad.Raza}");
                }
            }

            var gato = mapper.Map<Gato>(gatoEntidad);
            gato.GatoId = gatoId;
            gato.UsuarioId = usuario.Id;

            context.Gatos.Add(gato);
            await context.SaveChangesAsync();
            return Ok();

        }
        #endregion

        #region Put
        [HttpPut("{gatoId}")]
        public async Task<ActionResult> ModificarGato([FromRoute] Guid gatoId, [FromBody] ModificarGatoDto modificarGatoDto)
        {
            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var usuario = await userManager.FindByNameAsync(Username);

            var gatoExistente = await context.Gatos.FirstOrDefaultAsync(g => g.GatoId.Equals(gatoId));

            if (gatoExistente == null)
            {
                return BadRequest($"El id {gatoId} no existe");
            }

            if (gatoExistente.UsuarioId != usuario.Id)
            {
                return Forbid($"No tienes permisos de modificacion");
            }

            if (modificarGatoDto.Nombre != null && modificarGatoDto.Nombre != "")
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

        [HttpPut("imagen/{gatoId}")]
        public async Task<ActionResult> ModificarImagenGato([FromRoute] Guid gatoId, [FromForm] ModificarImagenGatoDto imagen)
        {
            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var usuario = await userManager.FindByNameAsync(Username);

            var gatoExistente = await context.Gatos.FirstOrDefaultAsync(g => g.GatoId.Equals(gatoId));

            if (gatoExistente == null)
            {
                return BadRequest($"El id {gatoId} no existe");
            }

            if (gatoExistente.UsuarioId != usuario.Id)
            {
                return Forbid($"No tienes permisos de modificacion");
            }

            using (var memoryStream = new MemoryStream())
            { 
                await imagen.Imagen.CopyToAsync(memoryStream);
                var contenido = memoryStream.ToArray();
                var extension = Path.GetExtension(imagen.Imagen.FileName);
                gatoExistente.ImagenGato = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor, gatoExistente.ImagenGato, imagen.Imagen.ContentType);
            }

            context.Update(gatoExistente);
            await context.SaveChangesAsync();

            return Ok(gatoExistente.ImagenGato);
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

            return Ok(collarExistente);
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

            if(gatoExistente.CollarId == null || gatoExistente.CollarId == Guid.Empty)
            {
                return BadRequest($"El gato con id {gatoExistente.GatoId} no tiene collar asignado");
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
