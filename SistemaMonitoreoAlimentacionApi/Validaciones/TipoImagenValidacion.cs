using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Validaciones
{
    public class TipoImagenValidacion : ValidationAttribute
    {
        private readonly string[] tiposValidos = new string[] { "image/jpeg", "image/png", "image/jpg" };

        public TipoImagenValidacion()
        {
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if (!tiposValidos.Contains(formFile.ContentType))
            {
                return new ValidationResult($"El tipo del archivo debe ser: {string.Join(", ", tiposValidos)} ");
            }
            return ValidationResult.Success;
        }
    }
}
