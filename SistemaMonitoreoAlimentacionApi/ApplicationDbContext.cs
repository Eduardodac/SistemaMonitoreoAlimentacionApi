using Microsoft.EntityFrameworkCore;
using SistemaMonitoreoAlimentacionApi.Entidades;

namespace SistemaMonitoreoAlimentacionApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dosificador>()
                .HasOne(u => u.Usuario)
                .WithOne(d => d.Dosificador)
                .HasForeignKey<Usuario>(u => u.DosificadorId);

            modelBuilder.Entity<Gato>()
                .HasOne(c => c.Collar)
                .WithOne(g => g.Gato)
                .HasForeignKey<Collar>(c => c.GatoId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Dosificador> Dosificadores { get; set; }
        public DbSet<Gato> Gatos { get; set; }
        public DbSet<Collar> Collares { get; set; }
        public DbSet<Cronologia> Cronologias { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Aviso> Avisos { get; set; }
        public DbSet<DiadelaSemana> DiadelaSemana { get; set; }


    }
}
