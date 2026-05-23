using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LSalto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedGrupos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Grupos",
                columns: new[] { "Id", "IdAnciaoResponsavel", "Local", "Nome" },
                values: new object[,]
                {
                    { 1, null, null, "Salto" },
                    { 2, null, null, "Itu" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Grupos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Grupos",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
