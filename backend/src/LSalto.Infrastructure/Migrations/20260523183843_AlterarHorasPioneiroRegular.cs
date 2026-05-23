using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LSalto.Infrastructure.Migrations
{
    public partial class AlterarHorasPioneiroRegular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Privilegios",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequisitoHoras",
                value: 50);
        }

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
