using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Registro
{
    public class NuevoRegistroDto
    {
        [Required]
        public Guid DosificadorId { get; set; } = Guid.Empty!;
        [Required]
        public Guid CollarId { get; set; } = Guid.Empty!;
        [Required]
        public double Duracion { get; set; } = 0.0;
        [Required]
        public double Consumo { get; set; } = 0.0;
    }
}
