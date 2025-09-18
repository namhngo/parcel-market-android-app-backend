using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB57 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoaiHinhThucThanhToan",
                table: "Por_HoSoNguoiNop");

            migrationBuilder.AddColumn<Guid>(
                name: "IDHinhThucThanhToan",
                table: "Por_HoSoNguoiNop",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDHinhThucThanhToan",
                table: "Por_HoSoNguoiNop");

            migrationBuilder.AddColumn<int>(
                name: "LoaiHinhThucThanhToan",
                table: "Por_HoSoNguoiNop",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
