using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpAcademy.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPreRequisitoToModulo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreRequisitoId",
                table: "Modulos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 1,
                column: "PreRequisitoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 2,
                column: "PreRequisitoId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 3,
                column: "PreRequisitoId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 4,
                column: "PreRequisitoId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 5,
                column: "PreRequisitoId",
                value: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreRequisitoId",
                table: "Modulos");
        }
    }
}
