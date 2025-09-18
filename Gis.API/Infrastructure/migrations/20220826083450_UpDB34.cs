using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NguoiHuy",
                table: "Por_HoSo_QuyTrinh");

            migrationBuilder.AddColumn<bool>(
                name: "CongKhai",
                table: "Por_HoSoNguoiNop",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "NgayTamNgung",
                table: "Por_HoSo_QuyTrinh",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "NgayTraKetQua",
                table: "Por_HoSo_QuyTrinh",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "NgayXuLy",
                table: "Por_HoSo_QuyTrinh",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CongKhai",
                table: "Por_GopYPhanAnh",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CongKhai",
                table: "Por_HoSoNguoiNop");

            migrationBuilder.DropColumn(
                name: "NgayTamNgung",
                table: "Por_HoSo_QuyTrinh");

            migrationBuilder.DropColumn(
                name: "NgayTraKetQua",
                table: "Por_HoSo_QuyTrinh");

            migrationBuilder.DropColumn(
                name: "NgayXuLy",
                table: "Por_HoSo_QuyTrinh");

            migrationBuilder.DropColumn(
                name: "CongKhai",
                table: "Por_GopYPhanAnh");

            migrationBuilder.AddColumn<string>(
                name: "NguoiHuy",
                table: "Por_HoSo_QuyTrinh",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true);
        }
    }
}
