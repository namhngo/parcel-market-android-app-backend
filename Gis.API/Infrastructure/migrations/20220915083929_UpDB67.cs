using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB67 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NguoiXuLy",
                table: "Por_HoSoPA_QuyTrinh",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NguoiTraKetQua",
                table: "Por_HoSoPA_QuyTrinh",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NguoiTiepNhan",
                table: "Por_HoSoPA_QuyTrinh",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NguoiHuy",
                table: "Por_HoSoPA_QuyTrinh",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoiDungHuy",
                table: "Por_HoSoPA_QuyTrinh",
                type: "character varying(550)",
                maxLength: 550,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NguoiXuLy",
                table: "Por_HoSo_QuyTrinh",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NguoiTraKetQua",
                table: "Por_HoSo_QuyTrinh",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NguoiTiepNhan",
                table: "Por_HoSo_QuyTrinh",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NguoiHuy",
                table: "Por_HoSo_QuyTrinh",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoiDungHuy",
                table: "Por_HoSo_QuyTrinh",
                type: "character varying(550)",
                maxLength: 550,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoiDungHuy",
                table: "Por_HoSoPA_QuyTrinh");

            migrationBuilder.DropColumn(
                name: "NoiDungHuy",
                table: "Por_HoSo_QuyTrinh");

            migrationBuilder.AlterColumn<string>(
                name: "NguoiXuLy",
                table: "Por_HoSoPA_QuyTrinh",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(55)",
                oldMaxLength: 55,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NguoiTraKetQua",
                table: "Por_HoSoPA_QuyTrinh",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(55)",
                oldMaxLength: 55,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NguoiTiepNhan",
                table: "Por_HoSoPA_QuyTrinh",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(55)",
                oldMaxLength: 55,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NguoiHuy",
                table: "Por_HoSoPA_QuyTrinh",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(55)",
                oldMaxLength: 55,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NguoiXuLy",
                table: "Por_HoSo_QuyTrinh",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(55)",
                oldMaxLength: 55,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NguoiTraKetQua",
                table: "Por_HoSo_QuyTrinh",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(55)",
                oldMaxLength: 55,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NguoiTiepNhan",
                table: "Por_HoSo_QuyTrinh",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(55)",
                oldMaxLength: 55,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NguoiHuy",
                table: "Por_HoSo_QuyTrinh",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(55)",
                oldMaxLength: 55,
                oldNullable: true);
        }
    }
}
