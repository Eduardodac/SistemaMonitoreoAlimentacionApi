using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Gato
    {
        public Guid GatoId { get; set; } = Guid.Empty!;
        [Required (ErrorMessage = "El campo {0} es requerido")]
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        public Guid? CollarId { get; set; } 
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombre { get; set; } = null!;
        public string? Raza { get; set; }
        public string? Sexo { get; set; }
        [Range(0, 25, ErrorMessage = "El campo {0} debe estar entre 0 y 25")]
        public int? Edad { get; set; }
        public string? ImagenGato { get; set; }
        public Usuario? Usuario { get; set; }
        public Collar? Collar { get; set; }
    }
}
