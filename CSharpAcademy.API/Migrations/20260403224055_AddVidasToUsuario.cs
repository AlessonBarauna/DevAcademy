using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpAcademy.API.Migrations
{
    /// <inheritdoc />
    public partial class AddVidasToUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UltimoRecargaVida",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Vidas",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UltimoRecargaVida",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Vidas",
                table: "Usuarios");
        }
    }
}
