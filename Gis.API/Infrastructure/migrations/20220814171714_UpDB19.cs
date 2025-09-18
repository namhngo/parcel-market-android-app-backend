using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SohieuVanBan",
                table: "Por_VanBanPhapQuy",
                newName: "SoHieuVanBan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoHieuVanBan",
                table: "Por_VanBanPhapQuy",
                newName: "SohieuVanBan");
        }
    }
}
