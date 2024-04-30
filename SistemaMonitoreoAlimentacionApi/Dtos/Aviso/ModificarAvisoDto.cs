using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Aviso
{
    public class ModificarAvisoDto
    {
        public DateTime LimpiarContenedor { get; set; } = new DateTime(2000, 1, 1);
        public DateTime LimpiarPlato { get; set; } = new DateTime(2000, 1, 1);
        public DateTime Caducidad { get; set; } = new DateTime(2000, 1, 1);
    }
}
