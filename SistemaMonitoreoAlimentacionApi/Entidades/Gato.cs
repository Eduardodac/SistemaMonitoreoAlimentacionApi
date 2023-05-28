namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Gato
    {
        public Guid GatosId { get; set; } = Guid.Empty!;
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        public string Nombre { get; set; } = null!;
        public string Raza { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public string Edad { get; set; } = null!;
        public string ImagenGato { get; set; } = null!;
    }
}
