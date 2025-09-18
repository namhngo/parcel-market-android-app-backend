using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB38 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CapNhatHoSo",
                table: "Por_ChucNang_BuocQuyTrinh",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ChuyenBuocKeTiep",
                table: "Por_ChucNang_BuocQuyTrinh",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HuyHoSo",
                table: "Por_ChucNang_BuocQuyTrinh",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TamDungHoSo",
                table: "Por_ChucNang_BuocQuyTrinh",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TiepNhanHoSo",
                table: "Por_ChucNang_BuocQuyTrinh",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TraKetQua",
                table: "Por_ChucNang_BuocQuyTrinh",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TraNguocLai",
                table: "Por_ChucNang_BuocQuyTrinh",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "XemQuyTrinh",
                table: "Por_ChucNang_BuocQuyTrinh",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapNhatHoSo",
                table: "Por_ChucNang_BuocQuyTrinh");

            migrationBuilder.DropColumn(
                name: "ChuyenBuocKeTiep",
                table: "Por_ChucNang_BuocQuyTrinh");

            migrationBuilder.DropColumn(
                name: "HuyHoSo",
                table: "Por_ChucNang_BuocQuyTrinh");

            migrationBuilder.DropColumn(
                name: "TamDungHoSo",
                table: "Por_ChucNang_BuocQuyTrinh");

            migrationBuilder.DropColumn(
                name: "TiepNhanHoSo",
                table: "Por_ChucNang_BuocQuyTrinh");

            migrationBuilder.DropColumn(
                name: "TraKetQua",
                table: "Por_ChucNang_BuocQuyTrinh");

            migrationBuilder.DropColumn(
                name: "TraNguocLai",
                table: "Por_ChucNang_BuocQuyTrinh");

            migrationBuilder.DropColumn(
                name: "XemQuyTrinh",
                table: "Por_ChucNang_BuocQuyTrinh");
        }
    }
}
