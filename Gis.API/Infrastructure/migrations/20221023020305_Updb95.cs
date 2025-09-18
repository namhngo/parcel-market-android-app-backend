using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class Updb95 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sys_EmailSms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmailFrom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    EmailPort = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    EmailHost = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    EmailPass = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SmSUser = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SmSPass = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SmSUrl = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_EmailSms", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_EmailSms");
        }
    }
}
