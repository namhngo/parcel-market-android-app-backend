using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Por_FileMauHuongDanQT");

            migrationBuilder.AddColumn<Guid>(
                name: "IDDichVuCungCap",
                table: "Por_QuyTrinh",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IDPhongChucNang",
                table: "Por_BuocQuyTrinh",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Por_FileMauThanhPhanHStrongQT",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    GhiChu = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    TenFile = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    URL = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    IDQuyTrinh = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_FileMauThanhPhanHStrongQT", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Por_FileMauThanhPhanHStrongQT");

            migrationBuilder.DropColumn(
                name: "IDDichVuCungCap",
                table: "Por_QuyTrinh");

            migrationBuilder.DropColumn(
                name: "IDPhongChucNang",
                table: "Por_BuocQuyTrinh");

            migrationBuilder.CreateTable(
                name: "Por_FileMauHuongDanQT",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IDQuyTrinh = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    URL = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_FileMauHuongDanQT", x => x.Id);
                });
        }
    }
}
