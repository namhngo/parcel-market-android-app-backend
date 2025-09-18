using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayXuLy",
                table: "Por_HoSo_QuyTrinh");

            migrationBuilder.AddColumn<string>(
                name: "NguoiTraNguocLai",
                table: "Por_HoSo_Buoc_QuyTrinh",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoiDungTraNguocLai",
                table: "Por_HoSo_Buoc_QuyTrinh",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NguoiTraNguocLai",
                table: "Por_HoSo_Buoc_QuyTrinh");

            migrationBuilder.DropColumn(
                name: "NoiDungTraNguocLai",
                table: "Por_HoSo_Buoc_QuyTrinh");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "NgayXuLy",
                table: "Por_HoSo_QuyTrinh",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
