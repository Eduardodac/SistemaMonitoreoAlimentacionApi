using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMonitoreoAlimentacionApi.Migrations
{
    public partial class ModificaCollar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collares_Gatos_GatoId",
                table: "Collares");

            migrationBuilder.DropIndex(
                name: "IX_Collares_GatoId",
                table: "Collares");

            migrationBuilder.AlterColumn<Guid>(
                name: "GatoId",
                table: "Collares",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Collares_GatoId",
                table: "Collares",
                column: "GatoId",
                unique: true,
                filter: "[GatoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Collares_Gatos_GatoId",
                table: "Collares",
                column: "GatoId",
                principalTable: "Gatos",
                principalColumn: "GatoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collares_Gatos_GatoId",
                table: "Collares");

            migrationBuilder.DropIndex(
                name: "IX_Collares_GatoId",
                table: "Collares");

            migrationBuilder.AlterColumn<Guid>(
                name: "GatoId",
                table: "Collares",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collares_GatoId",
                table: "Collares",
                column: "GatoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Collares_Gatos_GatoId",
                table: "Collares",
                column: "GatoId",
                principalTable: "Gatos",
                principalColumn: "GatoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
