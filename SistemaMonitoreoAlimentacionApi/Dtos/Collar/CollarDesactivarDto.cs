namespace SistemaMonitoreoAlimentacionApi.Dtos.Collar
{
    public class CollarDesactivarDto
    {
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        public Guid GatoId { get; set; } = Guid.Empty!;
    }
}
