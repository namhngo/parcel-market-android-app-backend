using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Por_FileVanBanPhapQuy");

            migrationBuilder.AddColumn<string>(
                name: "TenFile",
                table: "Por_VanBanPhapQuy",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Por_VanBanPhapQuy",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenFile",
                table: "Por_VanBanPhapQuy");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "Por_VanBanPhapQuy");

            migrationBuilder.CreateTable(
                name: "Por_FileVanBanPhapQuy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IDVanBanPhapQuy = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    URL = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_FileVanBanPhapQuy", x => x.Id);
                });
        }
    }
}
