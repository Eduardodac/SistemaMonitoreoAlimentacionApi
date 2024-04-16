namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Usuario
    {
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public string UsuariosCorreo { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? ImagenUsuario { get; set; }
        public Guid? DosificadorId { get; set; }

    }
}
