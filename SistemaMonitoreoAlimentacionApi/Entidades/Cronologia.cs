using System.ComponentModel.DataAnnotations;

namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Cronologia
    {
        public Guid CronologiaId { get; set; } = Guid.Empty!;
        [Required]
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        [Required]
        public Guid GatoId { get; set; } = Guid.Empty!;
        public DateTime FechaInicio { get; set; } = DateTime.MinValue!;
        public DateTime FechaFin { get; set; } = DateTime.MinValue!;
        public int Aproximaciones { get; set; } = 0;
        public double AlimentoConsumido { get; set; } = 0;
        public int AproximacionesSinConsumo { get; set; } = 0;
        public Usuario? Usuario { get; set; }
        public Gato? Gato { get; set; }  

    }
}
