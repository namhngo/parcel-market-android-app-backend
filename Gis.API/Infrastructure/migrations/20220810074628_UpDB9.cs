using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuocTiepTheo",
                table: "Por_BuocQuyTrinh");

            migrationBuilder.AddColumn<int>(
                name: "ThuTuBuoc",
                table: "Por_BuocQuyTrinh",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThuTuBuoc",
                table: "Por_BuocQuyTrinh");

            migrationBuilder.AddColumn<Guid>(
                name: "BuocTiepTheo",
                table: "Por_BuocQuyTrinh",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
