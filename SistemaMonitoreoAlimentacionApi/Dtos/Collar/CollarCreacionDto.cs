using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Collar
{
    public class CollarEntidadDto
    {
        public Guid CollarId { get; set; } = Guid.Empty!;
        public DateTime? FechaSalida { get; set; }
        public DateTime? FechaActivacion { get; set; }
        public string NumeroRegistro { get; set; } = null!;
        public Boolean EstatusActivacion { get; set; } = false;
    }
}
