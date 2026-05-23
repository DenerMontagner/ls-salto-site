using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LSalto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterarHorasPioneiroRegular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Privilegios",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequisitoHoras",
                value: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Privilegios",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequisitoHoras",
                value: 840);
        }
    }
}
