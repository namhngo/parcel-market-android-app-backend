using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenFile",
                table: "Por_GopYPhanAnh",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenNguoiGui",
                table: "Por_GopYPhanAnh",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Por_GopYPhanAnh",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenFile",
                table: "Por_GopYPhanAnh");

            migrationBuilder.DropColumn(
                name: "TenNguoiGui",
                table: "Por_GopYPhanAnh");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "Por_GopYPhanAnh");
        }
    }
}
