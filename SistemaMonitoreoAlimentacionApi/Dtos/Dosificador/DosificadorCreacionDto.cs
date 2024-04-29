using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Dosificador
{
    public class DosificadorCreacionDto
    {
        public Guid DosificadorId { get; set; } = Guid.Empty!;
        public DateTime? FechaSalida { get; set; }
        [Required]
        public string NumeroRegistro { get; set; } = null!;
        [Required]
        public Boolean EstatusActivacion { get; set; } = false;
    }
}
