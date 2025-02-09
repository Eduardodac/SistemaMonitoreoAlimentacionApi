namespace SistemaMonitoreoAlimentacionApi.Dtos.Horario
{
    public class ConfirmarDosificacionDto
    {
        public bool dosificar { get; set; }
        public bool habilitado { get; set; }
        public DateTime? Hora { get; set; } = DateTime.Now;


    }
}
