using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Collar
    {
        public Guid CollarId { get; set; } = Guid.Empty!;
        public Guid? GatoId { get; set; }
        [Required]
        public DateTime? FechaSalida { get; set; }
        public DateTime? FechaActivacion{ get; set; }
        [Required]
        public string NumeroRegistro { get; set; } = null!;
        [Required]
        public Boolean EstatusActivacion { get; set; } = false;
        public Gato? Gato { get; set; }
    }

}
