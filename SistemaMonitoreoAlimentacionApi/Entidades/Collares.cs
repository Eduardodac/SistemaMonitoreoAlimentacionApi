﻿namespace SistemaMonitoreoAlimentacionApi.Entidades
{
    public class Collares
    {
        public Guid CollarId { get; set; } = Guid.Empty!;
        public string Salida { get; set; } = null!;
        public string Registro { get; set; } = null!;
        public string EstatusActivacion { get; set; } = null!;
    }
}
