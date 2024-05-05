using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Gato
{
    public class ModificarGatoDto
    {
        public string? Nombre { get; set; }
        public string? Raza { get; set; }
        public string? Sexo { get; set; }
        [Range(0, 25, ErrorMessage = "El campo {0} debe estar entre 0 y 25")]
        public int? Edad { get; set; }
    }
}
