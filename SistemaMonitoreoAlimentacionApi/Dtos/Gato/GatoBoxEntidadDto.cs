using SistemaMonitoreoAlimentacionApi.Dtos.Collar;
using SistemaMonitoreoAlimentacionApi.Entidades;
using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Dtos.Gato
{
    public class GatoBoxEntidadDto
    {
        public string Name { get; set; }
        public Guid Code { get; set; }

    }
}