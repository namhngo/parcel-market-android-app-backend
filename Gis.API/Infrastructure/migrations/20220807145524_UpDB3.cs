using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DichVuCungCap",
                table: "Por_QuyTrinh");

            migrationBuilder.AddColumn<Guid>(
                name: "IDDichVuCungCap",
                table: "Por_QuyTrinh",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDDichVuCungCap",
                table: "Por_QuyTrinh");

            migrationBuilder.AddColumn<string>(
                name: "DichVuCungCap",
                table: "Por_QuyTrinh",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
