using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class Updb82 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NgayTamNgung",
                table: "Por_HoSo_QuyTrinh",
                newName: "NgayKetThucTamDung");

            migrationBuilder.AddColumn<Guid>(
                name: "IDBuocQuyTrinhTamDung",
                table: "Por_HoSo_QuyTrinh",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "NgayBatDauTamDung",
                table: "Por_HoSo_QuyTrinh",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiTamDung",
                table: "Por_HoSo_QuyTrinh",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoiDungTamDung",
                table: "Por_HoSo_QuyTrinh",
                type: "character varying(550)",
                maxLength: 550,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDBuocQuyTrinhTamDung",
                table: "Por_HoSo_QuyTrinh");

            migrationBuilder.DropColumn(
                name: "NgayBatDauTamDung",
                table: "Por_HoSo_QuyTrinh");

            migrationBuilder.DropColumn(
                name: "NguoiTamDung",
                table: "Por_HoSo_QuyTrinh");

            migrationBuilder.DropColumn(
                name: "NoiDungTamDung",
                table: "Por_HoSo_QuyTrinh");

            migrationBuilder.RenameColumn(
                name: "NgayKetThucTamDung",
                table: "Por_HoSo_QuyTrinh",
                newName: "NgayTamNgung");
        }
    }
}
