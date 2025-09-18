using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB42 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IDTrangThaiHS",
                table: "Por_HoSo_Buoc_QuyTrinh",
                newName: "IDTrangThai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IDTrangThai",
                table: "Por_HoSo_Buoc_QuyTrinh",
                newName: "IDTrangThaiHS");
        }
    }
}
