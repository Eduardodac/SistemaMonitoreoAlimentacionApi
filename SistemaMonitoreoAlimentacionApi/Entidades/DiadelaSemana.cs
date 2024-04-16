using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class DiadelaSemana
    {
        public int DiadelaSemanaId { get; set; } = 0;
        [Required]
        public string Dia { get; set; } = null!;
    }
}
