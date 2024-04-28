using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMonitoreoAlimentacionApi.Migrations
{
    public partial class gatoModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gatos_Collares_CollarId",
                table: "Gatos");

            migrationBuilder.DropIndex(
                name: "IX_Gatos_CollarId",
                table: "Gatos");

            migrationBuilder.AlterColumn<Guid>(
                name: "CollarId",
                table: "Gatos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Gatos_CollarId",
                table: "Gatos",
                column: "CollarId",
                unique: true,
                filter: "[CollarId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Gatos_Collares_CollarId",
                table: "Gatos",
                column: "CollarId",
                principalTable: "Collares",
                principalColumn: "CollarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gatos_Collares_CollarId",
                table: "Gatos");

            migrationBuilder.DropIndex(
                name: "IX_Gatos_CollarId",
                table: "Gatos");

            migrationBuilder.AlterColumn<Guid>(
                name: "CollarId",
                table: "Gatos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gatos_CollarId",
                table: "Gatos",
                column: "CollarId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gatos_Collares_CollarId",
                table: "Gatos",
                column: "CollarId",
                principalTable: "Collares",
                principalColumn: "CollarId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
