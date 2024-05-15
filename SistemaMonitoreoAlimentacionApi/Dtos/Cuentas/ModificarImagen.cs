using SistemaMonitoreoAlimentacionApi.Validaciones;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Cuentas
{
    public class ModificarImagen
    {
        [PesoImagenValidacion(PesoMaximoEnMegaBytes: 2)]
        [TipoImagenValidacion]
        public IFormFile Imagen { get; set; }
    }
}
