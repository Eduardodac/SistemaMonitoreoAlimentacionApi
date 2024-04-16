namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Collar
    {
        public Guid CollarId { get; set; } = Guid.Empty!;
        public DateTime? Salida { get; set; }
        public string NumeroRegistro { get; set; } = null!;
        public string EstatusActivacion { get; set; } = null!;
    }
}
