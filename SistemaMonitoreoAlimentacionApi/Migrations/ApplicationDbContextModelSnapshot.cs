﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaMonitoreoAlimentacionApi;

#nullable disable

namespace SistemaMonitoreoAlimentacionApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SistemaMonitoreoAlimentacionApi.Entidades.Aviso", b =>
                {
                    b.Property<Guid>("AvisoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Caducidad")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LimpiarContenedor")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LimpiarPlato")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AvisoId");

                    b.ToTable("Avisos");
                });

            modelBuilder.Entity("SistemaMonitoreoAlimentacionApi.Entidades.Calendario", b =>
                {
                    b.Property<Guid>("CalendarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HorarioDomingoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HorarioJuevesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HorarioLunesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HorarioMartesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HorarioMiercolesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HorarioSabadoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HorarioViernesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CalendarioId");

                    b.ToTable("Calendarios");
                });

            modelBuilder.Entity("SistemaMonitoreoAlimentacionApi.Entidades.Collar", b =>
                {
                    b.Property<Guid>("CollarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EstatusActivacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Registro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salida")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CollarId");

                    b.ToTable("Collares");
                });

            modelBuilder.Entity("SistemaMonitoreoAlimentacionApi.Entidades.Cronologia", b =>
                {
                    b.Property<Guid>("CronologiaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GatoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CronologiaId");

                    b.ToTable("Cronologias");
                });

            modelBuilder.Entity("SistemaMonitoreoAlimentacionApi.Entidades.Dosificador", b =>
                {
                    b.Property<Guid>("DosificadorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EstatusActivacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Registro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salida")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DosificadorId");

                    b.ToTable("Dosificadores");
                });

            modelBuilder.Entity("SistemaMonitoreoAlimentacionApi.Entidades.Evento", b =>
                {
                    b.Property<Guid>("EventoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Consumo")
                        .HasColumnType("float");

                    b.Property<Guid>("DosificicadorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Duracion")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GatoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Hora")
                        .HasColumnType("datetime2");

                    b.HasKey("EventoId");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("SistemaMonitoreoAlimentacionApi.Entidades.Gato", b =>
                {
                    b.Property<Guid>("GatoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Edad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagenGato")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Raza")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GatoId");

                    b.ToTable("Gatos");
                });

            modelBuilder.Entity("SistemaMonitoreoAlimentacionApi.Entidades.Horario", b =>
                {
                    b.Property<Guid>("HorarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AM1")
                        .HasColumnType("bit");

                    b.Property<bool>("AM10")
                        .HasColumnType("bit");

                    b.Property<bool>("AM11")
                        .HasColumnType("bit");

                    b.Property<bool>("AM12")
                        .HasColumnType("bit");

                    b.Property<bool>("AM2")
                        .HasColumnType("bit");

                    b.Property<bool>("AM3")
                        .HasColumnType("bit");

                    b.Property<bool>("AM4")
                        .HasColumnType("bit");

                    b.Property<bool>("AM5")
                        .HasColumnType("bit");

                    b.Property<bool>("AM6")
                        .HasColumnType("bit");

                    b.Property<bool>("AM7")
                        .HasColumnType("bit");

                    b.Property<bool>("AM8")
                        .HasColumnType("bit");

                    b.Property<bool>("AM9")
                        .HasColumnType("bit");

                    b.Property<bool>("PM1")
                        .HasColumnType("bit");

                    b.Property<bool>("PM10")
                        .HasColumnType("bit");

                    b.Property<bool>("PM11")
                        .HasColumnType("bit");

                    b.Property<bool>("PM12")
                        .HasColumnType("bit");

                    b.Property<bool>("PM2")
                        .HasColumnType("bit");

                    b.Property<bool>("PM3")
                        .HasColumnType("bit");

                    b.Property<bool>("PM4")
                        .HasColumnType("bit");

                    b.Property<bool>("PM5")
                        .HasColumnType("bit");

                    b.Property<bool>("PM6")
                        .HasColumnType("bit");

                    b.Property<bool>("PM7")
                        .HasColumnType("bit");

                    b.Property<bool>("PM8")
                        .HasColumnType("bit");

                    b.Property<bool>("PM9")
                        .HasColumnType("bit");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("HorarioId");

                    b.ToTable("Horarios");
                });

            modelBuilder.Entity("SistemaMonitoreoAlimentacionApi.Entidades.Usuario", b =>
                {
                    b.Property<Guid>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApellidoMaterno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApellidoPaterno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DosificadorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImagenUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuariosCorreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
