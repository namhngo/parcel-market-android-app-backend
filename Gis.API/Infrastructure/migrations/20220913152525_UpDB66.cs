using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB66 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NhuCauChuyenMucDichGCN",
                table: "Por_NCCDMDSDD",
                newName: "NhuCauChuyenMucDich");

            migrationBuilder.RenameColumn(
                name: "DiaDiemMaPX",
                table: "Por_NCCDMDSDD",
                newName: "MaPX");

            migrationBuilder.AddColumn<string>(
                name: "DiaDiem",
                table: "Por_NCCDMDSDD",
                type: "character varying(155)",
                maxLength: 155,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GCN",
                table: "Por_NCCDMDSDD",
                type: "character varying(155)",
                maxLength: 155,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaDiem",
                table: "Por_NCCDMDSDD");

            migrationBuilder.DropColumn(
                name: "GCN",
                table: "Por_NCCDMDSDD");

            migrationBuilder.RenameColumn(
                name: "NhuCauChuyenMucDich",
                table: "Por_NCCDMDSDD",
                newName: "NhuCauChuyenMucDichGCN");

            migrationBuilder.RenameColumn(
                name: "MaPX",
                table: "Por_NCCDMDSDD",
                newName: "DiaDiemMaPX");
        }
    }
}
