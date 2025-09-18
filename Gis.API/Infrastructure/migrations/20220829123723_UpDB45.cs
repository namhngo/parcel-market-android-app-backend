using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB45 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NguoiHuy",
                table: "Por_HoSo_QuyTrinh",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiTraKetQua",
                table: "Por_HoSo_QuyTrinh",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NguoiHuy",
                table: "Por_HoSo_QuyTrinh");

            migrationBuilder.DropColumn(
                name: "NguoiTraKetQua",
                table: "Por_HoSo_QuyTrinh");
        }
    }
}
