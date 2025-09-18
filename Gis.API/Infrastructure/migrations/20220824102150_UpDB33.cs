using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinhVuc",
                table: "Por_GopYPhanAnh");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "Por_GopYPhanAnh");

            migrationBuilder.AddColumn<Guid>(
                name: "IDLinhVuc",
                table: "Por_GopYPhanAnh",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IDTrangThaiPA",
                table: "Por_GopYPhanAnh",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDLinhVuc",
                table: "Por_GopYPhanAnh");

            migrationBuilder.DropColumn(
                name: "IDTrangThaiPA",
                table: "Por_GopYPhanAnh");

            migrationBuilder.AddColumn<int>(
                name: "LinhVuc",
                table: "Por_GopYPhanAnh",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrangThai",
                table: "Por_GopYPhanAnh",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
