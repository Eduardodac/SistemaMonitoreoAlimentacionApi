using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Dosificador
    {
        public Guid DosificadorId { get; set; } = Guid.Empty!;
        public DateTime? FechaSalida { get; set; }
        [Required]
        public string NumeroRegistro { get; set; } = null!;
        [Required]
        public Boolean EstatusActivacion { get; set; } = false;
        public Usuario? Usuario { get; set; }
    }
}
