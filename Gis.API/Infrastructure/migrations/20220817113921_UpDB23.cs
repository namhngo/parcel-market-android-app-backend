using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDTrangThaiHS",
                table: "Por_HoSo_QuyTrinh");

            migrationBuilder.AddColumn<Guid>(
                name: "IDTrangThaiHS",
                table: "Por_HoSoNguoiNop",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDTrangThaiHS",
                table: "Por_HoSoNguoiNop");

            migrationBuilder.AddColumn<Guid>(
                name: "IDTrangThaiHS",
                table: "Por_HoSo_QuyTrinh",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
