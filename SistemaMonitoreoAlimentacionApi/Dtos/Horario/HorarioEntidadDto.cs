using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Horario
{
    public class HorarioEntidadDto
    {
        public Guid HorarioId { get; set; } = Guid.Empty!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int DiaDeLaSemanaId { get; set; } = 0;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime Hora { get; set; } = DateTime.Now;
    }
}
