using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaMonitoreoAlimentacionApi.Dtos.Cuentas;
using SistemaMonitoreoAlimentacionApi.Dtos.Gato;
using SistemaMonitoreoAlimentacionApi.Dtos.Usuario;
using SistemaMonitoreoAlimentacionApi.Entidades;
using SistemaMonitoreoAlimentacionApi.Servicios;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Usuario> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<Usuario> signInManager;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "usuarios";

        public CuentasController(ApplicationDbContext context, 
            UserManager<Usuario> userManager, 
            IConfiguration configuration,
            SignInManager<Usuario> signInManager,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        #region Registrar
        [HttpPost("registrar")]
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar(NuevoUsuario nuevoUsuario)
        {
            var Usuario = new Usuario {
                UserName = nuevoUsuario.UserName,
                Email = nuevoUsuario.Email
            };
            var resultado = await userManager.CreateAsync(Usuario, nuevoUsuario.Password);

            if(resultado.Succeeded)
            {
                return ConstruirToken(new CredencialesUsuario { Username = nuevoUsuario.UserName, Password = nuevoUsuario.Password });
            }
            else
            {
                return BadRequest(resultado.Errors);    
            }
        }
        #endregion

        #region Login
        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesUsuario credencialesUsuario)
        {
            var resultado = await signInManager.PasswordSignInAsync(credencialesUsuario.Username,
                credencialesUsuario.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if(resultado.Succeeded)
            {
                return ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest("Login Incorrecto");
            }
        }
        #endregion

        #region Renovar
        [HttpGet("RenovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<RespuestaAutenticacion> Renovar()
        {
            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var credencialesUsuario = new CredencialesUsuario()
            {
                Username = Username
            };

            return ConstruirToken(credencialesUsuario);

        }
        #endregion

        #region CambioPassword
        [HttpPut("cambiarPassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaAutenticacion>> CambiarPasswordAsync([FromBody] PasswordChange password)
        {
            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";

            var userExistente = await userManager.FindByNameAsync(password.username);
            if (Username != userExistente.UserName)
            {
                return Forbid();
            }


            if(userExistente != null)
            {
                var cambioResult = await userManager.ChangePasswordAsync(userExistente, password.OldPassword, password.NewPassword);
                if (cambioResult.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest(cambioResult.Errors);
                }
            }
            else
            {
                return BadRequest();
            }
            

        }
        #endregion

        #region getInfo
        [HttpGet("usuario")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ModificarUsuario>> GetInfo()
        {
            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var usuario = await userManager.FindByNameAsync(Username);

            var userDto = mapper.Map<ModificarUsuario>(usuario);

            return userDto;
        }
        #endregion

        #region Put Users
        [HttpPut("modificarUsuario")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> ModificarUsuario([FromBody] ModificarUsuario modificar)
        {
            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var usuario = await userManager.FindByNameAsync(Username);

            if(modificar.Nombre != null && modificar.Nombre !="")
            {
                usuario.Nombre = modificar.Nombre;
            }

            if (modificar.ApellidoMaterno != null && modificar.ApellidoMaterno != "")
            {
                usuario.ApellidoMaterno = modificar.ApellidoMaterno;
            }

            if (modificar.ApellidoPaterno != null && modificar.ApellidoPaterno != "")
            {
                usuario.ApellidoPaterno = modificar.ApellidoPaterno;
            }

            var result = await userManager.UpdateAsync(usuario);
            if(result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors);
            }

        }
        #endregion

        #region ModificarImagen
        [HttpPut("Imagen")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<string>> Put([FromForm] ModificarImagen imagen)
        {
            var UsernamelClaim = HttpContext.User.Claims.Where(claim => claim.Type == "username").FirstOrDefault();
            var Username = UsernamelClaim != null ? UsernamelClaim.Value : "";
            var usuario = await userManager.FindByNameAsync(Username);

            using (var memoryStream = new MemoryStream())
            {
                await imagen.Imagen.CopyToAsync(memoryStream);
                var contenido = memoryStream.ToArray();
                var extension = Path.GetExtension(imagen.Imagen.FileName);
                usuario.ImagenUsuario = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor, usuario.ImagenUsuario, imagen.Imagen.ContentType);
            }

            var result = await userManager.UpdateAsync(usuario);
            if (result.Succeeded)
            {
                return usuario.ImagenUsuario;
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        #endregion

        private RespuestaAutenticacion ConstruirToken(CredencialesUsuario credencialesUsuario)
        {
            var claims = new List<Claim>()
            {
                new Claim("username", credencialesUsuario.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyJwt"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMonths(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: credentials);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiration
            };
        }
    }
}
