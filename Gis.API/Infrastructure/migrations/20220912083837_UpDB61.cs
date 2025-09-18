using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB61 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GuiEmail",
                table: "Por_QuyTrinh",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GuiSms",
                table: "Por_QuyTrinh",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuiEmail",
                table: "Por_QuyTrinh");

            migrationBuilder.DropColumn(
                name: "GuiSms",
                table: "Por_QuyTrinh");
        }
    }
}
