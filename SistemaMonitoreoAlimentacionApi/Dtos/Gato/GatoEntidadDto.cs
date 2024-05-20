using SistemaMonitoreoAlimentacionApi.Dtos.Collar;
using SistemaMonitoreoAlimentacionApi.Entidades;
using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Gato
{
    public class GatoEntidadDto
    {
        public Guid? gatoId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombre { get; set; }
        public string? Raza { get; set; }
        public string? Sexo { get; set; }
        public int Edad { get; set; } = 0;
        public string? ImagenGato { get; set; }
        public CollarEntidadDto? Collar { get; set; }
    }
}