using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB37 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayTraLai",
                table: "Por_HoSo_Buoc_QuyTrinh");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "NgayTraNguocLai",
                table: "Por_HoSo_Buoc_QuyTrinh",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayTraNguocLai",
                table: "Por_HoSo_Buoc_QuyTrinh");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "NgayTraLai",
                table: "Por_HoSo_Buoc_QuyTrinh",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
