using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class ActividadFelina
    {
        public Guid ActividadFelinaId { get; set; } = Guid.Empty!;
        [Required]
        public Guid GatoId { get; set; } = Guid.Empty!;
        [Required]
        public DateTime FechaHora { get; set; } = DateTime.MinValue!;
        [Required]
        public double Tiempo { get; set; } = 0;
        public double AlimentoConsumido { get; set; } = 0.0;
        public Gato? Gato { get; set; }  

    }
}
