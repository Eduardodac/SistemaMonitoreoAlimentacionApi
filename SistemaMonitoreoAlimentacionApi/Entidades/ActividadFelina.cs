using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class ActividadFelina
    {
        public Guid ActividadFelinaId { get; set; } = Guid.Empty!;
        [Required]
        public Guid GatoId { get; set; } = Guid.Empty!;
        [Required]
        public DateTime FechaInicio { get; set; } = DateTime.MinValue!;
        [Required]
        public DateTime FechaFin { get; set; } = DateTime.MinValue!;
        public int Aproximaciones { get; set; } = 0;
        public double AlimentoConsumido { get; set; } = 0.0;
        public int AproximacionesSinConsumo { get; set; } = 0;
        public Gato? Gato { get; set; }  

    }
}
