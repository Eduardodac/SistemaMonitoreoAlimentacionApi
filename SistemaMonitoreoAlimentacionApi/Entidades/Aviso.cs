using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Aviso
    {
        public Guid AvisoId { get; set; } = Guid.Empty!;
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        [Required]
        public DateTime LimpiarContenedor { get; set; } = DateTime.Now;
        [Required]
        public DateTime LimpiarPlato { get; set; } = DateTime.Now;
        [Required]
        public DateTime Caducidad { get; set; } = DateTime.Now;
    }
}
