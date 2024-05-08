using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Aviso
{
    public class NuevoAvisoDto
    {
        public Guid AvisoId { get; set; } = Guid.Empty!;
        [Required]
        public DateTime LimpiarContenedor { get; set; } = DateTime.Now;
        [Required]
        public DateTime LimpiarPlato { get; set; } = DateTime.Now;
        [Required]
        public DateTime Caducidad { get; set; } = DateTime.Now;
        public int AlimentoDisponible { get; set; } = 0;
    }
}
