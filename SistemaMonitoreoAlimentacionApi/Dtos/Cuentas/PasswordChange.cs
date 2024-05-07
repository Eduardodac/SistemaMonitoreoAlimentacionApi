using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Cuentas
{
    public class PasswordChange
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string username { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string OldPassword { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string NewPassword { get; set; } = null!;
    }
}
