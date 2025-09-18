using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SohoSo",
                table: "Por_HoSoNguoiNop",
                newName: "SoHoSo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoHoSo",
                table: "Por_HoSoNguoiNop",
                newName: "SohoSo");
        }
    }
}
