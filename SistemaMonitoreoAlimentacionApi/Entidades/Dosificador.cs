using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Dosificador
    {
        public Guid DosificadorId { get; set; } = Guid.Empty!;
        public Guid AuxiliarId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime FechaSalida { get; set; } = DateTime.Now;
        public DateTime? FechaActivacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string NumeroRegistro { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Boolean EstatusActivacion { get; set; } = false;
        public Usuario? Usuario { get; set; }

    }
}
