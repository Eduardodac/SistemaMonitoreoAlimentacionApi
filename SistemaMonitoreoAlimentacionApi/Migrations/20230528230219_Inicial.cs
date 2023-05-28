using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMonitoreoAlimentacionApi.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avisos",
                columns: table => new
                {
                    AvisoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LimpiarContenedor = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LimpiarPlato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Caducidad = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avisos", x => x.AvisoId);
                });

            migrationBuilder.CreateTable(
                name: "Calendarios",
                columns: table => new
                {
                    CalendarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HorarioLunesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HorarioMartesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HorarioMiercolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HorarioJuevesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HorarioViernesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HorarioSabadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HorarioDomingoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendarios", x => x.CalendarioId);
                });

            migrationBuilder.CreateTable(
                name: "Collares",
                columns: table => new
                {
                    CollarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Salida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Registro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstatusActivacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collares", x => x.CollarId);
                });

            migrationBuilder.CreateTable(
                name: "Cronologias",
                columns: table => new
                {
                    CronologiaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cronologias", x => x.CronologiaId);
                });

            migrationBuilder.CreateTable(
                name: "Dosificadores",
                columns: table => new
                {
                    DosificadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Salida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Registro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstatusActivacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dosificadores", x => x.DosificadorId);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    EventoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DosificicadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Duracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Consumo = table.Column<double>(type: "float", nullable: false),
                    Hora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.EventoId);
                });

            migrationBuilder.CreateTable(
                name: "Gatos",
                columns: table => new
                {
                    GatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Raza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagenGato = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gatos", x => x.GatoId);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    HorarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PM12 = table.Column<bool>(type: "bit", nullable: false),
                    PM1 = table.Column<bool>(type: "bit", nullable: false),
                    PM2 = table.Column<bool>(type: "bit", nullable: false),
                    PM3 = table.Column<bool>(type: "bit", nullable: false),
                    PM4 = table.Column<bool>(type: "bit", nullable: false),
                    PM5 = table.Column<bool>(type: "bit", nullable: false),
                    PM6 = table.Column<bool>(type: "bit", nullable: false),
                    PM7 = table.Column<bool>(type: "bit", nullable: false),
                    PM8 = table.Column<bool>(type: "bit", nullable: false),
                    PM9 = table.Column<bool>(type: "bit", nullable: false),
                    PM10 = table.Column<bool>(type: "bit", nullable: false),
                    PM11 = table.Column<bool>(type: "bit", nullable: false),
                    AM12 = table.Column<bool>(type: "bit", nullable: false),
                    AM1 = table.Column<bool>(type: "bit", nullable: false),
                    AM2 = table.Column<bool>(type: "bit", nullable: false),
                    AM3 = table.Column<bool>(type: "bit", nullable: false),
                    AM4 = table.Column<bool>(type: "bit", nullable: false),
                    AM5 = table.Column<bool>(type: "bit", nullable: false),
                    AM6 = table.Column<bool>(type: "bit", nullable: false),
                    AM7 = table.Column<bool>(type: "bit", nullable: false),
                    AM8 = table.Column<bool>(type: "bit", nullable: false),
                    AM9 = table.Column<bool>(type: "bit", nullable: false),
                    AM10 = table.Column<bool>(type: "bit", nullable: false),
                    AM11 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.HorarioId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuariosCorreo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagenUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DosificadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avisos");

            migrationBuilder.DropTable(
                name: "Calendarios");

            migrationBuilder.DropTable(
                name: "Collares");

            migrationBuilder.DropTable(
                name: "Cronologias");

            migrationBuilder.DropTable(
                name: "Dosificadores");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Gatos");

            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
