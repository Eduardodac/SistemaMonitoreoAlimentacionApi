namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Gato
    {
        public Guid GatoId { get; set; } = Guid.Empty!;
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        public Guid CollaId { get;set; } = Guid.Empty!;
        public string Nombre { get; set; } = null!;
        public string? Raza { get; set; }
        public string? Sexo { get; set; }
        public string? Edad { get; set; }
        public string? ImagenGato { get; set; }
    }
}
