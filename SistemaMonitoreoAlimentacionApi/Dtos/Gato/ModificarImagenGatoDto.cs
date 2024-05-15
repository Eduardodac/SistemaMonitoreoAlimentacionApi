using SistemaMonitoreoAlimentacionApi.Validaciones;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Gato
{
    public class ModificarImagenGatoDto
    {
        [PesoImagenValidacion(PesoMaximoEnMegaBytes: 2)]
        [TipoImagenValidacion]
        public IFormFile Imagen { get; set; }
    }
}
