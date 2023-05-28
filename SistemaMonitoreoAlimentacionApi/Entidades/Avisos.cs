namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Avisos
    {
        public Guid AvisosId { get; set; } = Guid.Empty!;
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        public DateTime LimpiarContenedor { get; set; } = DateTime.Now;
        public DateTime LimpiarPlato { get; set; } = DateTime.Now;
        public DateTime Caducidad { get; set; } = DateTime.Now;
    }
}
