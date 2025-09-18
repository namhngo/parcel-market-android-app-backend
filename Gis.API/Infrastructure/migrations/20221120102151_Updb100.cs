using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class Updb100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LopBanDo",
                table: "Por_TaiKhoanLopBanDo");

            migrationBuilder.AddColumn<Guid>(
                name: "IdLopBanDo",
                table: "Por_TaiKhoanLopBanDo",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdLopBanDo",
                table: "Por_TaiKhoanLopBanDo");

            migrationBuilder.AddColumn<string>(
                name: "LopBanDo",
                table: "Por_TaiKhoanLopBanDo",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
