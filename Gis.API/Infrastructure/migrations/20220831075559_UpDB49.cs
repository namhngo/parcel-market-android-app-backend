using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB49 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoaiFileHoSoNguoiNop",
                table: "Por_FileHoSoNguoiNop");

            migrationBuilder.AddColumn<Guid>(
                name: "IDFileMauThanhPhanHStrongQT",
                table: "Por_FileHoSoNguoiNop",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDFileMauThanhPhanHStrongQT",
                table: "Por_FileHoSoNguoiNop");

            migrationBuilder.AddColumn<int>(
                name: "LoaiFileHoSoNguoiNop",
                table: "Por_FileHoSoNguoiNop",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
