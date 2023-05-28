namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Horario
    {
        public Guid HorarioId { get; set; } = Guid.Empty!;
        public Guid UsuarioId { get; set; } = Guid.Empty!;
        public Boolean PM12 { get; set; } = false;
        public Boolean PM1 { get; set; } = false;
        public Boolean PM2 { get; set; } = false;
        public Boolean PM3 { get; set; } = false;
        public Boolean PM4 { get; set; } = false;
        public Boolean PM5 { get; set; } = false;
        public Boolean PM6 { get; set; } = false;
        public Boolean PM7 { get; set; } = false;
        public Boolean PM8 { get; set; } = false;
        public Boolean PM9 { get; set; } = false;
        public Boolean PM10 { get; set; } = false;
        public Boolean PM11 { get; set; } = false;
        public Boolean AM12 { get; set; } = false;
        public Boolean AM1 { get; set; } = false;
        public Boolean AM2 { get; set; } = false;
        public Boolean AM3 { get; set; } = false;
        public Boolean AM4 { get; set; } = false;
        public Boolean AM5 { get; set; } = false;
        public Boolean AM6 { get; set; } = false;
        public Boolean AM7 { get; set; } = false;
        public Boolean AM8 { get; set; } = false;
        public Boolean AM9 { get; set; } = false;
        public Boolean AM10 { get; set; } = false;
        public Boolean AM11 { get; set; } = false;
    }
}
