using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpAcademy.API.Migrations
{
    /// <inheritdoc />
    public partial class AddStreakToUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StreakAtual",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StreakMaximo",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimoEstudo",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StreakAtual",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "StreakMaximo",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UltimoEstudo",
                table: "Usuarios");
        }
    }
}
