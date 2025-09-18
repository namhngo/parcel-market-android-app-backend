using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB39 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayHoanThanh",
                table: "Por_HoSo_QuyTrinh");

            migrationBuilder.DropColumn(
                name: "NgayTraKetQua",
                table: "Por_HoSo_QuyTrinh");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "NgayHoanThanh",
                table: "Por_HoSo_QuyTrinh",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "NgayTraKetQua",
                table: "Por_HoSo_QuyTrinh",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
