using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Collar
    {
        public Guid CollarId { get; set; } = Guid.Empty!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime FechaSalida { get; set; } = DateTime.Now;
        public DateTime? FechaActivacion{ get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string NumeroRegistro { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Boolean EstatusActivacion { get; set; } = false;
        public Gato? Gato { get; set; }
    }

}
