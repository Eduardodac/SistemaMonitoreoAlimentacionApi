using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Dosificador> Dosificadores { get; set; }
        public DbSet<Gato> Gatos { get; set; }
        public DbSet<Collar> Collares { get; set; }
        public DbSet<Cronologia> Cronologias { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Calendario> Calendarios { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Aviso> Avisos { get; set; }



    }
}
