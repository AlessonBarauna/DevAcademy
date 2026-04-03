using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpAcademy.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSrsToProgresso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NivelRetencao",
                table: "Progressos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProximaRevisao",
                table: "Progressos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalRevisoes",
                table: "Progressos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NivelRetencao",
                table: "Progressos");

            migrationBuilder.DropColumn(
                name: "ProximaRevisao",
                table: "Progressos");

            migrationBuilder.DropColumn(
                name: "TotalRevisoes",
                table: "Progressos");
        }
    }
}
