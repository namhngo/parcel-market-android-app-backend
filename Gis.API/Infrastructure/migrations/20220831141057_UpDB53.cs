using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB53 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PhuongXa",
                table: "Por_GopYPhanAnh",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "QuanHuyen",
                table: "Por_GopYPhanAnh",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "SoNha",
                table: "Por_GopYPhanAnh",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenDuong",
                table: "Por_GopYPhanAnh",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TinhThanhPho",
                table: "Por_GopYPhanAnh",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhuongXa",
                table: "Por_GopYPhanAnh");

            migrationBuilder.DropColumn(
                name: "QuanHuyen",
                table: "Por_GopYPhanAnh");

            migrationBuilder.DropColumn(
                name: "SoNha",
                table: "Por_GopYPhanAnh");

            migrationBuilder.DropColumn(
                name: "TenDuong",
                table: "Por_GopYPhanAnh");

            migrationBuilder.DropColumn(
                name: "TinhThanhPho",
                table: "Por_GopYPhanAnh");
        }
    }
}
