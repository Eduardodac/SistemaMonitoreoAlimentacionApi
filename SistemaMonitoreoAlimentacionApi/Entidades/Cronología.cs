namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Cronología
    {
        public Guid CronologiaId { get; set; } = Guid.Empty!;
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        public Guid GatoId { get; set; } = Guid.Empty!;
        public Guid EventoId { get; set; } = Guid.Empty!;
    }
}
