using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Collar
{
    public class CollarCreacionDto
    {
        public Guid CollarId { get; set; } = Guid.Empty!;
        public DateTime? FechaSalida { get; set; }
        public string NumeroRegistro { get; set; } = null!;
        public string EstatusActivacion { get; set; } = null!;
    }
}
