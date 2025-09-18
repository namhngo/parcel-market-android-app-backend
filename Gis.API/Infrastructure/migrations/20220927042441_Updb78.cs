using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class Updb78 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDTinhThanhPho",
                table: "Por_QuanHuyen");

            migrationBuilder.DropColumn(
                name: "IDQuanHuyen",
                table: "Por_PhuongXaThiTran");

            migrationBuilder.DropColumn(
                name: "IDTinhThanhPho",
                table: "Por_PhuongXaThiTran");

            migrationBuilder.AlterColumn<string>(
                name: "Ten",
                table: "Por_TinhThanhPho",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap",
                table: "Por_TinhThanhPho",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ma",
                table: "Por_TinhThanhPho",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenTiengAnh",
                table: "Por_TinhThanhPho",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ten",
                table: "Por_QuanHuyen",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap",
                table: "Por_QuanHuyen",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ma",
                table: "Por_QuanHuyen",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaTP",
                table: "Por_QuanHuyen",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenTiengAnh",
                table: "Por_QuanHuyen",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhThanhPho",
                table: "Por_QuanHuyen",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ten",
                table: "Por_PhuongXaThiTran",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap",
                table: "Por_PhuongXaThiTran",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ma",
                table: "Por_PhuongXaThiTran",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaQH",
                table: "Por_PhuongXaThiTran",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuanHuyen",
                table: "Por_PhuongXaThiTran",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenTiengAnh",
                table: "Por_PhuongXaThiTran",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cap",
                table: "Por_TinhThanhPho");

            migrationBuilder.DropColumn(
                name: "Ma",
                table: "Por_TinhThanhPho");

            migrationBuilder.DropColumn(
                name: "TenTiengAnh",
                table: "Por_TinhThanhPho");

            migrationBuilder.DropColumn(
                name: "Cap",
                table: "Por_QuanHuyen");

            migrationBuilder.DropColumn(
                name: "Ma",
                table: "Por_QuanHuyen");

            migrationBuilder.DropColumn(
                name: "MaTP",
                table: "Por_QuanHuyen");

            migrationBuilder.DropColumn(
                name: "TenTiengAnh",
                table: "Por_QuanHuyen");

            migrationBuilder.DropColumn(
                name: "TinhThanhPho",
                table: "Por_QuanHuyen");

            migrationBuilder.DropColumn(
                name: "Cap",
                table: "Por_PhuongXaThiTran");

            migrationBuilder.DropColumn(
                name: "Ma",
                table: "Por_PhuongXaThiTran");

            migrationBuilder.DropColumn(
                name: "MaQH",
                table: "Por_PhuongXaThiTran");

            migrationBuilder.DropColumn(
                name: "QuanHuyen",
                table: "Por_PhuongXaThiTran");

            migrationBuilder.DropColumn(
                name: "TenTiengAnh",
                table: "Por_PhuongXaThiTran");

            migrationBuilder.AlterColumn<string>(
                name: "Ten",
                table: "Por_TinhThanhPho",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ten",
                table: "Por_QuanHuyen",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IDTinhThanhPho",
                table: "Por_QuanHuyen",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Ten",
                table: "Por_PhuongXaThiTran",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IDQuanHuyen",
                table: "Por_PhuongXaThiTran",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IDTinhThanhPho",
                table: "Por_PhuongXaThiTran",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
