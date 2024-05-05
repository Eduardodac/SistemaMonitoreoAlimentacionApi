using SistemaMonitoreoAlimentacionApi.Entidades;
using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Gato
{
    public class GatoCreacionDto
    {
        public Guid? GatoId { get; set; }
        public Guid? UsuarioId { get; set; }
        public string? Nombre { get; set; }
        public string? Raza { get; set; }
        public string? Sexo { get; set; }
        public int Edad { get; set; } = 0;
    }
}