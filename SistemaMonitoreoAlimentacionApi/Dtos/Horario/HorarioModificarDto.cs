using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Horario
{
    public class HorarioModificarDto
    {
        public DateTime? Hora { get; set; } = DateTime.Now;
    }
}
