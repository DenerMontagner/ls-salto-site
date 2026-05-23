using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814

namespace LSalto.Infrastructure.Migrations
{
    public partial class SeedGrupos : Migration
    {
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
