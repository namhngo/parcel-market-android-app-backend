using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DSNguoiDungThamGia",
                table: "Por_BuocQuyTrinh",
                newName: "IDsNguoiDungThamGia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IDsNguoiDungThamGia",
                table: "Por_BuocQuyTrinh",
                newName: "DSNguoiDungThamGia");
        }
    }
}
