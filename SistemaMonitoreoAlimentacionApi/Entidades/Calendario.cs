namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Calendario
    {
        public Guid CalendarioId { get; set; } = Guid.Empty!;
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        public Guid HorarioLunesId { get; set; } = Guid.Empty!;
        public Guid HorarioMartesId { get; set; } = Guid.Empty!;
        public Guid HorarioMiercolesId { get; set; } = Guid.Empty!;
        public Guid HorarioJuevesId { get; set; } = Guid.Empty!; 
        public Guid HorarioViernesId { get; set; } = Guid.Empty!;
        public Guid HorarioSabadoId { get; set; } = Guid.Empty!;
        public Guid HorarioDomingoId { get; set; } = Guid.Empty!;
    }
}
