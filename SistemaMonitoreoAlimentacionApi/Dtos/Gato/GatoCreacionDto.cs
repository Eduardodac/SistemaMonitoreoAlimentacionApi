using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Gato
{
    public class GatoCreacionDto
    {
        public Guid GatoId { get; set; } = Guid.Empty!;
        [Required]
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        [Required]
        public string Nombre { get; set; } = null!;
        public string? Raza { get; set; }
        public string? Sexo { get; set; }
        public string? Edad { get; set; }
    }
}
