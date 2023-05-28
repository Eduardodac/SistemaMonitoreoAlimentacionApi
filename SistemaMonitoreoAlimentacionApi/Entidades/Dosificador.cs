namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Dosificador
    {
        public Guid DosificadorId { get; set; } = Guid.Empty!;
        public DateTime? Salida { get; set; }
        public string Registro { get; set; } = null!;
        public Boolean EstatusActivacion { get; set; } = false;

    }
}
