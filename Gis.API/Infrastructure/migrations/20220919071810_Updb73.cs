using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class Updb73 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IDMauEmail",
                table: "Por_BuocQuyTrinh",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IDMauSms",
                table: "Por_BuocQuyTrinh",
                type: "uuid",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDMauEmail",
                table: "Por_BuocQuyTrinh");

            migrationBuilder.DropColumn(
                name: "IDMauSms",
                table: "Por_BuocQuyTrinh");
        }
    }
}
