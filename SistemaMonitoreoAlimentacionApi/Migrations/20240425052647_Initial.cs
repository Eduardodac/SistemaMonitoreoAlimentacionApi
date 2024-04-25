using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMonitoreoAlimentacionApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiadelaSemana",
                columns: table => new
                {
                    DiadelaSemanaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiadelaSemana", x => x.DiadelaSemanaId);
                });

            migrationBuilder.CreateTable(
                name: "Dosificadores",
                columns: table => new
                {
                    DosificadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumeroRegistro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstatusActivacion = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dosificadores", x => x.DosificadorId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioCorreo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagenUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DosificadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Dosificadores_DosificadorId",
                        column: x => x.DosificadorId,
                        principalTable: "Dosificadores",
                        principalColumn: "DosificadorId");
                });

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
                    table.ForeignKey(
                        name: "FK_Avisos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gatos",
                columns: table => new
                {
                    GatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Raza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Edad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagenGato = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gatos", x => x.GatoId);
                    table.ForeignKey(
                        name: "FK_Gatos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    HorarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiaDeLaSemanaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Hora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.HorarioId);
                    table.ForeignKey(
                        name: "FK_Horarios_DiadelaSemana_DiaDeLaSemanaId",
                        column: x => x.DiaDeLaSemanaId,
                        principalTable: "DiadelaSemana",
                        principalColumn: "DiadelaSemanaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Horarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Collares",
                columns: table => new
                {
                    CollarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActivacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumeroRegistro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstatusActivacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collares", x => x.CollarId);
                    table.ForeignKey(
                        name: "FK_Collares_Gatos_GatoId",
                        column: x => x.GatoId,
                        principalTable: "Gatos",
                        principalColumn: "GatoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cronologias",
                columns: table => new
                {
                    CronologiaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Aproximaciones = table.Column<int>(type: "int", nullable: false),
                    AlimentoConsumido = table.Column<double>(type: "float", nullable: false),
                    AproximacionesSinConsumo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cronologias", x => x.CronologiaId);
                    table.ForeignKey(
                        name: "FK_Cronologias_Gatos_GatoId",
                        column: x => x.GatoId,
                        principalTable: "Gatos",
                        principalColumn: "GatoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    EventoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DosificadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Duracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Consumo = table.Column<double>(type: "float", nullable: false),
                    Hora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IntegradoAnalisis = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.EventoId);
                    table.ForeignKey(
                        name: "FK_Eventos_Dosificadores_DosificadorId",
                        column: x => x.DosificadorId,
                        principalTable: "Dosificadores",
                        principalColumn: "DosificadorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Eventos_Gatos_GatoId",
                        column: x => x.GatoId,
                        principalTable: "Gatos",
                        principalColumn: "GatoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avisos_UsuarioId",
                table: "Avisos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Collares_GatoId",
                table: "Collares",
                column: "GatoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cronologias_GatoId",
                table: "Cronologias",
                column: "GatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_DosificadorId",
                table: "Eventos",
                column: "DosificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_GatoId",
                table: "Eventos",
                column: "GatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Gatos_UsuarioId",
                table: "Gatos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_DiaDeLaSemanaId",
                table: "Horarios",
                column: "DiaDeLaSemanaId");

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_UsuarioId",
                table: "Horarios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_DosificadorId",
                table: "Usuarios",
                column: "DosificadorId",
                unique: true,
                filter: "[DosificadorId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avisos");

            migrationBuilder.DropTable(
                name: "Collares");

            migrationBuilder.DropTable(
                name: "Cronologias");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropTable(
                name: "Gatos");

            migrationBuilder.DropTable(
                name: "DiadelaSemana");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Dosificadores");
        }
    }
}
