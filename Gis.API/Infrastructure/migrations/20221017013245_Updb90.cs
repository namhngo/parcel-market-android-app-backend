using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class Updb90 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Por_GisSoNha",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SoThua = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    SoTo = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    SoNha = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    TenDuong = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    ApKhuPho = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    TenPhuongXa = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    MaPX = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_GisSoNha", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Por_GisSoNha");
        }
    }
}
