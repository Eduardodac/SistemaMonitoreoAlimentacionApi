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

            modelBuilder.Entity<Collar>()
                .HasOne(g => g.Gato)
                .WithOne(c => c.Collar)
                .HasForeignKey<Gato>(g => g.CollarId);

            modelBuilder.Entity<Usuario>()
                .HasOne(a => a.Aviso)
                .WithOne(u => u.Usuario)
                .HasForeignKey<Aviso>(a => a.AvisoId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Dosificador> Dosificadores { get; set; }
        public DbSet<Gato> Gatos { get; set; }
        public DbSet<Collar> Collares { get; set; }
        public DbSet<ActividadFelina> ActividadesFelinas { get; set; }
        public DbSet<Registro> Registros { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Aviso> Avisos { get; set; }
        public DbSet<DiadelaSemana> DiadelaSemana { get; set; }


    }
}
