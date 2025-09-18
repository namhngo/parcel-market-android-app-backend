using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaHS",
                table: "Por_HoSoNguoiNop");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaHS",
                table: "Por_HoSoNguoiNop",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
