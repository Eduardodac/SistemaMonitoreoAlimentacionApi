using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Registro
{
    public class NuevoRegistroDto
    {
        public Guid RegistroId { get; set; } = Guid.Empty!;
        [Required]
        public Guid DosificadorId { get; set; } = Guid.Empty!;
        [Required]
        public Guid CollarId { get; set; } = Guid.Empty!;
        [Required]
        public DateTime Duracion { get; set; } = DateTime.Now;
        [Required]
        public double Consumo { get; set; } = 0.0;
        [Required]
        public DateTime Hora { get; set; } = DateTime.Now!;
        public Boolean IntegradoAAnalisis { get; set; } = false;
    }
}
