using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Collar
{
    public class CollarActivarDto
    {
        public Guid? GatoId { get; set; }
        public string NumeroRegistro { get; set; } = null!;
        public string EstatusActivacion { get; set; } = null!;

    }
}
