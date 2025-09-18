using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gis.API.Infrastructure.migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Por_BuocQuyTrinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    ThoiGianThucHien = table.Column<int>(type: "integer", nullable: false),
                    IDQuyTrinh = table.Column<Guid>(type: "uuid", nullable: false),
                    BuocTiepTheo = table.Column<Guid>(type: "uuid", nullable: false),
                    DSNguoiDungThamGia = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_BuocQuyTrinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_CauHoiThuongGap",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CauHoi = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    CauTraLoi = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TrangThai = table.Column<bool>(type: "boolean", nullable: false),
                    STT = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_CauHoiThuongGap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_FileGopYPhanAnh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    URL = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    IDGopYPhanAnh = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_FileGopYPhanAnh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_FileHoSoNguoiNop",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    URL = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    IDHoSoNguoiNop = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_FileHoSoNguoiNop", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_FileMauBienNhanHS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    URL = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    IDQuyTrinh = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_FileMauBienNhanHS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_FileMauHuongDanQT",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    URL = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    IDQuyTrinh = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_FileMauHuongDanQT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_FileVanBanPhapQuy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    URL = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    IDVanBanPhapQuy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_FileVanBanPhapQuy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_GopYPhanAnh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LinhVuc = table.Column<int>(type: "integer", maxLength: 55, nullable: false),
                    SoDienThoai = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    DiaChi = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    TieuDe = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    NoiDung = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    TrangThai = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_GopYPhanAnh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_HoSo_Buoc_QuyTrinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IDHoSo = table.Column<Guid>(type: "uuid", nullable: false),
                    IDQuyTrinh = table.Column<Guid>(type: "uuid", nullable: false),
                    IDBuocQuyTrinh = table.Column<Guid>(type: "uuid", nullable: false),
                    NguoiXuLy = table.Column<Guid>(type: "uuid", nullable: true),
                    NgayBatDau = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    NgayKetThuc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_HoSo_Buoc_QuyTrinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_HoSo_QuyTrinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IDHoSo = table.Column<Guid>(type: "uuid", nullable: false),
                    IDTrangThaiHS = table.Column<Guid>(type: "uuid", nullable: false),
                    IDQuyTrinh = table.Column<Guid>(type: "uuid", nullable: false),
                    IDBuocQuyTrinh = table.Column<Guid>(type: "uuid", nullable: false),
                    NgayNop = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    NgayTiepNhan = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NgayNhanKQ = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NgayDuKienNhanKQ = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_HoSo_QuyTrinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_HoSoNguoiNop",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HoTen = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    CMND = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    Email = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    SoDienThoai = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    DiaChi = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MaHS = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IDMucDichSuDung = table.Column<Guid>(type: "uuid", nullable: false),
                    IDThuaDat = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_HoSoNguoiNop", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_HuongDanSuDung",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TieuDe = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    NoiDung = table.Column<string>(type: "text", nullable: true),
                    TrangThai = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_HuongDanSuDung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_LinhVuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_LinhVuc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_LoaiVanBanPhapQuy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_LoaiVanBanPhapQuy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_MucDichSuDung",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TrangThai = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_MucDichSuDung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_PhuongXaThiTran",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IDTinhThanhPho = table.Column<Guid>(type: "uuid", nullable: false),
                    IDQuanHuyen = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_PhuongXaThiTran", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_QuanHuyen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IDTinhThanhPho = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_QuanHuyen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_QuyTrinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    DichVuCungCap = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ThoiGianThucHien = table.Column<int>(type: "integer", nullable: false),
                    GiaTien = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_QuyTrinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_TaiKhoan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CMND = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    HoTen = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    TenDangNhap = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    MatKhau = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    Email = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    SoDienThoai = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    DiaChi = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TrangThai = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_TaiKhoan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_TaiKhoanToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenDangNhap = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    TokenTruyCap = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true),
                    TokenLamMoi = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_TaiKhoanToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_ThuatDat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SoTo = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    SoThua = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    SoNha = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TenDuong = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    IDPhuongXaThiTran = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_ThuatDat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_TinhThanhPho",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_TinhThanhPho", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_TrangThai",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_TrangThai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Por_VanBanPhapQuy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TieuDe = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    NoiDung = table.Column<string>(type: "text", nullable: true),
                    TrangThai = table.Column<bool>(type: "boolean", nullable: false),
                    IDLoaiVanBanPhapQuy = table.Column<Guid>(type: "uuid", nullable: false),
                    STT = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Por_VanBanPhapQuy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Authtokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    AccessToken = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true),
                    RefeshToken = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Authtokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    Name = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Configs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    Value = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Configs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Extension = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Path = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ObjectId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ObjectType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Content = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Receiver = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    DetailsURL = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ObjectId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ObjectType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    Name = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsFunc = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    Name = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    Url = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    Icon = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    Name = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    UserName = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    PassWord = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    Email = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Users_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: true),
                    OrganId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Users_Roles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Por_BuocQuyTrinh");

            migrationBuilder.DropTable(
                name: "Por_CauHoiThuongGap");

            migrationBuilder.DropTable(
                name: "Por_FileGopYPhanAnh");

            migrationBuilder.DropTable(
                name: "Por_FileHoSoNguoiNop");

            migrationBuilder.DropTable(
                name: "Por_FileMauBienNhanHS");

            migrationBuilder.DropTable(
                name: "Por_FileMauHuongDanQT");

            migrationBuilder.DropTable(
                name: "Por_FileVanBanPhapQuy");

            migrationBuilder.DropTable(
                name: "Por_GopYPhanAnh");

            migrationBuilder.DropTable(
                name: "Por_HoSo_Buoc_QuyTrinh");

            migrationBuilder.DropTable(
                name: "Por_HoSo_QuyTrinh");

            migrationBuilder.DropTable(
                name: "Por_HoSoNguoiNop");

            migrationBuilder.DropTable(
                name: "Por_HuongDanSuDung");

            migrationBuilder.DropTable(
                name: "Por_LinhVuc");

            migrationBuilder.DropTable(
                name: "Por_LoaiVanBanPhapQuy");

            migrationBuilder.DropTable(
                name: "Por_MucDichSuDung");

            migrationBuilder.DropTable(
                name: "Por_PhuongXaThiTran");

            migrationBuilder.DropTable(
                name: "Por_QuanHuyen");

            migrationBuilder.DropTable(
                name: "Por_QuyTrinh");

            migrationBuilder.DropTable(
                name: "Por_TaiKhoan");

            migrationBuilder.DropTable(
                name: "Por_TaiKhoanToken");

            migrationBuilder.DropTable(
                name: "Por_ThuatDat");

            migrationBuilder.DropTable(
                name: "Por_TinhThanhPho");

            migrationBuilder.DropTable(
                name: "Por_TrangThai");

            migrationBuilder.DropTable(
                name: "Por_VanBanPhapQuy");

            migrationBuilder.DropTable(
                name: "Sys_Authtokens");

            migrationBuilder.DropTable(
                name: "Sys_Categories");

            migrationBuilder.DropTable(
                name: "Sys_Configs");

            migrationBuilder.DropTable(
                name: "Sys_Files");

            migrationBuilder.DropTable(
                name: "Sys_Notification");

            migrationBuilder.DropTable(
                name: "Sys_Organizations");

            migrationBuilder.DropTable(
                name: "Sys_Permissions");

            migrationBuilder.DropTable(
                name: "Sys_Resources");

            migrationBuilder.DropTable(
                name: "Sys_Roles");

            migrationBuilder.DropTable(
                name: "Sys_Users");

            migrationBuilder.DropTable(
                name: "Sys_Users_Roles");
        }
    }
}
