using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB48 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PhuongXa",
                table: "Por_ThuatDat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "QuanHuyen",
                table: "Por_ThuatDat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TinhThanhPho",
                table: "Por_ThuatDat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "LoaiHinhThucThanhToan",
                table: "Por_HoSoNguoiNop",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PhuongXa",
                table: "Por_HoSoNguoiNop",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "QuanHuyen",
                table: "Por_HoSoNguoiNop",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TinhThanhPho",
                table: "Por_HoSoNguoiNop",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "LoaiFileHoSoNguoiNop",
                table: "Por_FileHoSoNguoiNop",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhuongXa",
                table: "Por_ThuatDat");

            migrationBuilder.DropColumn(
                name: "QuanHuyen",
                table: "Por_ThuatDat");

            migrationBuilder.DropColumn(
                name: "TinhThanhPho",
                table: "Por_ThuatDat");

            migrationBuilder.DropColumn(
                name: "LoaiHinhThucThanhToan",
                table: "Por_HoSoNguoiNop");

            migrationBuilder.DropColumn(
                name: "PhuongXa",
                table: "Por_HoSoNguoiNop");

            migrationBuilder.DropColumn(
                name: "QuanHuyen",
                table: "Por_HoSoNguoiNop");

            migrationBuilder.DropColumn(
                name: "TinhThanhPho",
                table: "Por_HoSoNguoiNop");

            migrationBuilder.DropColumn(
                name: "LoaiFileHoSoNguoiNop",
                table: "Por_FileHoSoNguoiNop");
        }
    }
}
