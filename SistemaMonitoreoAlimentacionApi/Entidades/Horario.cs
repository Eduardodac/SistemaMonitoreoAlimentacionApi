using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Horario
    {
        public Guid HorarioId { get; set; } = Guid.Empty!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int DiaDeLaSemanaId { get; set; } = 0;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime Hora { get; set; } = DateTime.Now;
        public Usuario? Usuario { get; set;}
        public DiadelaSemana? DiadelaSemana { get; set; }
    }
}
