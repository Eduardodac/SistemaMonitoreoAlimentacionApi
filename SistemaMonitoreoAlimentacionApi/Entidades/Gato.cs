using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Gato
    {
        public Guid GatoId { get; set; } = Guid.Empty!;
        [Required]
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        [Required]
        public Guid CollarId { get;set; } = Guid.Empty!;
        [Required]
        public string Nombre { get; set; } = null!;
        public string? Raza { get; set; }
        public string? Sexo { get; set; }
        public string? Edad { get; set; }
        public string? ImagenGato { get; set; }
        public Usuario? Usuario { get; set; }
        public Collar? Collar { get; set; }
    }
}
