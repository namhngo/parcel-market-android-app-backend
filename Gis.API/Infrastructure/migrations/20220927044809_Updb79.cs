using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class Updb79 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaTP",
                table: "Por_PhuongXaThiTran",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhThanhPho",
                table: "Por_PhuongXaThiTran",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaTP",
                table: "Por_PhuongXaThiTran");

            migrationBuilder.DropColumn(
                name: "TinhThanhPho",
                table: "Por_PhuongXaThiTran");
        }
    }
}
