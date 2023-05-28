namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Eventos
    {
        public Guid EventoId { get; set; } = Guid.Empty!;
        public Guid DosificicadorId { get; set; } = Guid.Empty!;
        public Guid GatoId { get; set; } = Guid.Empty!;
        public DateTime Duracion { get; set; } = DateTime.Now;
        public double Consumo { get; set; }
        public DateTime Hora { get; set; } = DateTime.Now!;
    }
}
