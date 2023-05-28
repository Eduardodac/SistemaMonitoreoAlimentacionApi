namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Cronologia
    {
        public Guid CronologiaId { get; set; } = Guid.Empty!;
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        public Guid GatoId { get; set; } = Guid.Empty!;
        public Guid EventoId { get; set; } = Guid.Empty!;
    }
}
