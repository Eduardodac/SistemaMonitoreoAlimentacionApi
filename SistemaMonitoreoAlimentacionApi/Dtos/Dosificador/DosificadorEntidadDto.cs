using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Dosificador
{
    public class DosificadorEntidadDto
    {
        public Guid DosificadorId { get; set; } = Guid.Empty!;
        public DateTime? FechaSalida { get; set; }
        public DateTime? FechaActivacion { get; set; }
        public string NumeroRegistro { get; set; } = null!;
        public Boolean EstatusActivacion { get; set; } = false;
    }
}
