using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB64 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenPhuongXaMaPX",
                table: "Por_GCNQSDD",
                newName: "TenPhuongXa");

            migrationBuilder.RenameColumn(
                name: "NguoiKySoThua",
                table: "Por_GCNQSDD",
                newName: "SoThua");

            migrationBuilder.AddColumn<string>(
                name: "MaPX",
                table: "Por_GCNQSDD",
                type: "character varying(155)",
                maxLength: 155,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiKy",
                table: "Por_GCNQSDD",
                type: "character varying(155)",
                maxLength: 155,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaPX",
                table: "Por_GCNQSDD");

            migrationBuilder.DropColumn(
                name: "NguoiKy",
                table: "Por_GCNQSDD");

            migrationBuilder.RenameColumn(
                name: "TenPhuongXa",
                table: "Por_GCNQSDD",
                newName: "TenPhuongXaMaPX");

            migrationBuilder.RenameColumn(
                name: "SoThua",
                table: "Por_GCNQSDD",
                newName: "NguoiKySoThua");
        }
    }
}
