using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class Updb91 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "So",
                table: "Por_LinhVuc",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "So",
                table: "Por_LinhVuc");
        }
    }
}
