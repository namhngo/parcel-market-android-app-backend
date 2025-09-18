using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB59 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ten",
                table: "Por_LinhVuc",
                type: "character varying(155)",
                maxLength: 155,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(55)",
                oldMaxLength: 55,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ma",
                table: "Por_LinhVuc",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ma",
                table: "Por_LinhVuc");

            migrationBuilder.AlterColumn<string>(
                name: "Ten",
                table: "Por_LinhVuc",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(155)",
                oldMaxLength: 155,
                oldNullable: true);
        }
    }
}
