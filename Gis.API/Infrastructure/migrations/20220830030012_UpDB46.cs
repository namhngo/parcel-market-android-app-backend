using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB46 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "Por_ThuatDat",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoNha",
                table: "Por_HoSoNguoiNop",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenDuong",
                table: "Por_HoSoNguoiNop",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "Por_ThuatDat");

            migrationBuilder.DropColumn(
                name: "SoNha",
                table: "Por_HoSoNguoiNop");

            migrationBuilder.DropColumn(
                name: "TenDuong",
                table: "Por_HoSoNguoiNop");
        }
    }
}
