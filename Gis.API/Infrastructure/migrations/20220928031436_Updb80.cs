using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class Updb80 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrangThai",
                table: "Por_QuyTrinh",
                newName: "MienPhi");

            migrationBuilder.AddColumn<bool>(
                name: "CongKhai",
                table: "Por_QuyTrinh",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CongKhai",
                table: "Por_QuyTrinh");

            migrationBuilder.RenameColumn(
                name: "MienPhi",
                table: "Por_QuyTrinh",
                newName: "TrangThai");
        }
    }
}
