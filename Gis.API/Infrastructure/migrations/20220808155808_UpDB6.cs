using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiaTien",
                table: "Por_QuyTrinh");

            migrationBuilder.RenameColumn(
                name: "Ten",
                table: "Por_QuyTrinh",
                newName: "TenThuTuc");

            migrationBuilder.RenameColumn(
                name: "IDDichVuCungCap",
                table: "Por_QuyTrinh",
                newName: "IDMucDo");

            migrationBuilder.AddColumn<Guid>(
                name: "IDCapDoThuTuc",
                table: "Por_QuyTrinh",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IDHinhThucCap",
                table: "Por_QuyTrinh",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IDLinhVuc",
                table: "Por_QuyTrinh",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Por_LinhVuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    IDCha = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_LinhVuc", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Por_LinhVuc");

            migrationBuilder.DropColumn(
                name: "IDCapDoThuTuc",
                table: "Por_QuyTrinh");

            migrationBuilder.DropColumn(
                name: "IDHinhThucCap",
                table: "Por_QuyTrinh");

            migrationBuilder.DropColumn(
                name: "IDLinhVuc",
                table: "Por_QuyTrinh");

            migrationBuilder.RenameColumn(
                name: "TenThuTuc",
                table: "Por_QuyTrinh",
                newName: "Ten");

            migrationBuilder.RenameColumn(
                name: "IDMucDo",
                table: "Por_QuyTrinh",
                newName: "IDDichVuCungCap");

            migrationBuilder.AddColumn<string>(
                name: "GiaTien",
                table: "Por_QuyTrinh",
                type: "character varying(155)",
                maxLength: 155,
                nullable: true);
        }
    }
}
