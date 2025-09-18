using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class Updb71 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Por_TemplatePhieuTiepNhan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Por_TemplatePhieuTiepNhan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CotSql = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Ma = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    NoiDung = table.Column<string>(type: "text", nullable: true),
                    TieuDe = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_TemplatePhieuTiepNhan", x => x.Id);
                });
        }
    }
}
