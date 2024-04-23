using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Evento
    {
        public Guid EventoId { get; set; } = Guid.Empty!;
        [Required]
        public Guid DosificadorId { get; set; } = Guid.Empty!;
        [Required]
        public Guid GatoId { get; set; } = Guid.Empty!;
        [Required]
        public DateTime Duracion { get; set; } = DateTime.Now;
        [Required]
        public double Consumo { get; set; } = 0.0;
        [Required]
        public DateTime Hora { get; set; } = DateTime.Now!;
        public Boolean IntegradoAnalisis { get; set; } = false;
        public Dosificador? Dosificador { get; set; }
        public Gato? Gato { get; set; }
    }
}
