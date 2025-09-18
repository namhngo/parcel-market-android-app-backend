using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDThuaDat",
                table: "Por_HoSoNguoiNop");

            migrationBuilder.AddColumn<Guid>(
                name: "IDHoSoNguoiNop",
                table: "Por_ThuatDat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDHoSoNguoiNop",
                table: "Por_ThuatDat");

            migrationBuilder.AddColumn<Guid>(
                name: "IDThuaDat",
                table: "Por_HoSoNguoiNop",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
