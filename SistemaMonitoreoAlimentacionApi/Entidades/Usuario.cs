using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Usuario
    {
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        [Required]
        public string UsuarioCorreo { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        public string? ImagenUsuario { get; set; }
        public Guid? DosificadorId { get; set; }
        public Dosificador? Dosificador { get; set; }

    }
}
