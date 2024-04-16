using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Cronologia
    {
        public Guid CronologiaId { get; set; } = Guid.Empty!;
        [Required]
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        [Required]
        public Guid GatoId { get; set; } = Guid.Empty!;
        [Required]
        public Guid EventoId { get; set; } = Guid.Empty!;
    }
}
