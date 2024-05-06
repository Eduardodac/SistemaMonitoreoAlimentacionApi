using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Horario
{
    public class HorarioModificarDto
    {
        public int? DiaDeLaSemanaId { get; set; } = 0;
        public DateTime? Hora { get; set; } = DateTime.Now;
    }
}
