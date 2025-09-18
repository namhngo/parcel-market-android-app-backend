using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class UpDB63 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Por_GCNQSDD",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SoHieu = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    NgayCap = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    NguoiSuDung = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    DiaChiThuongTru = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    CCCD = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    NguoiKySoThua = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    SoTo = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    DiaChiThuaDat = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    TenPhuongXaMaPX = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    DienTich = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    MucDichSuDung = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    ThoiHanSuDung = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    NhaO = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    CongTrinh = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    RungSanXuat = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    CayLauNam = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_GCNQSDD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_GPXD",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChuDauTu = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    SoThua = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    SoTo = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    TenXa = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    MaPX = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    SoGPXD = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    NgayCapGPXD = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    DienTichXayDung = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    TongDienTichSanXayDung = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    SoTang = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    GhiChu = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_GPXD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_HoSoPA_Buoc_QuyTrinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IDHoSo = table.Column<Guid>(type: "uuid", nullable: false),
                    IDQuyTrinh = table.Column<Guid>(type: "uuid", nullable: false),
                    IDBuocQuyTrinh = table.Column<Guid>(type: "uuid", nullable: false),
                    IDTrangThai = table.Column<Guid>(type: "uuid", nullable: false),
                    NguoiXuLy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    NoiDungXuLy = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    NguoiTraNguocLai = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    NoiDungTraNguocLai = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    NgayTraNguocLai = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NgayBatDau = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    NgayKetThuc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_HoSoPA_Buoc_QuyTrinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_HoSoPA_QuyTrinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IDHoSo = table.Column<Guid>(type: "uuid", nullable: false),
                    IDQuyTrinh = table.Column<Guid>(type: "uuid", nullable: false),
                    IDBuocQuyTrinh = table.Column<Guid>(type: "uuid", nullable: false),
                    NgayNop = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    NgayNhanKQ = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NgayHuy = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NgayTiepNhan = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NgayTamNgung = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NgayDuKienNhanKQ = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    NguoiHuy = table.Column<string>(type: "text", nullable: true),
                    NguoiTraKetQua = table.Column<string>(type: "text", nullable: true),
                    NguoiXuLy = table.Column<string>(type: "text", nullable: true),
                    NguoiTiepNhan = table.Column<string>(type: "text", nullable: true),
                    ThoiGianThucHien = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_HoSoPA_QuyTrinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_NCCDMDSDD",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaHS = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    TenChuSuDung = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    DiaChiThuongTru = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    DiaDiemMaPX = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    DiaChiThuaDat = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    SoTo = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    SoThua = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    DienTich = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    LoaiDatHienTrangTheoGCN = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    NhuCauChuyenMucDichGCN = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    ThongTinQuyHoach = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    GhiChu = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    SoDienThoai = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_NCCDMDSDD", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Por_GCNQSDD");

            migrationBuilder.DropTable(
                name: "Por_GPXD");

            migrationBuilder.DropTable(
                name: "Por_HoSoPA_Buoc_QuyTrinh");

            migrationBuilder.DropTable(
                name: "Por_HoSoPA_QuyTrinh");

            migrationBuilder.DropTable(
                name: "Por_NCCDMDSDD");
        }
    }
}
