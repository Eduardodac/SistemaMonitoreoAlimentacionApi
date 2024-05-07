namespace SistemaMonitoreoAlimentacionApi.Dtos.Cuentas
{
    public class UsuarioModificarDto
    {
        public Guid DosificadorId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? ImagenUsuario { get; set; }
    }
}
