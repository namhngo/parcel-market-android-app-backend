using Gis.API.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Gis.API.Infrastructure.Enums;

namespace Gis.API.ViewModel.HoSoQuyTrinh
{
    public class LoaiHoSo
    {
        public Guid IDQuyTrinh { get; set; }
        public Guid IDBuocQuyTrinh { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string DichVuCungCap { get; set; }
        public string ThoiGianThucHien { get; set; }
        public string GiaTien { get; set; }
        public string NgayDuKienNhanKQ { get; set; }

    }
    public class ThongTinNguoiNop
    {
        public Guid Id { get; set; }
        public string SoHoSo { get; set; }
        public string HoTen { get; set; }
        public string CMND { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public Guid TinhThanhPho { get; set; }
        public string TenTinhThanhPho { get; set; }
        public Guid QuanHuyen { get; set; }
        public string TenQuanHuyen { get; set; }
        public Guid PhuongXa { get; set; }
        public string TenPhuongXa { get; set; }
        public string SoNha { get; set; }
        public string TenDuong { get; set; }
    }
    public class ThongTinKhuDat
    {
        public Guid Id { get; set; }
        public string SoTo { get; set; }
        public string SoThua { get; set; }
        public Guid TinhThanhPho { get; set; }
        public string TenTinhThanhPho { get; set; }
        public Guid QuanHuyen { get; set; }
        public string TenQuanHuyen { get; set; }
        public Guid PhuongXa { get; set; }
        public string TenPhuongXa { get; set; }
        public string SoNha { get; set; }
        public string TenDuong { get; set; }
    }
    public class HoSoDinhKem
    {
        public Guid IDMucDichSuDung { get; set; }
        public string TenMucDichSuDung { get; set; }
        public List<FileHoSoNguoiNop> fileHoSoNguoiNops { get; set; }
    }
    public class FileHoSoNguoiNop
    {
        public Guid Id { get; set; }
        public string Ten { get; set; }
        public string URL { get; set; }
        public Guid IDFileMauThanhPhanHStrongQT { get; set; }
    }
    public class HinhThucThanhToan
    {
        public Guid? IDHinhThucThanhToan { get; set; }
        public string TenHinhThucThanhToan { get; set; }
        public bool? ThanhToan { get; set; }
    }
    public class HinhThucNhanKetQua
    {
        public Guid IDHinhThucNhanKetQua { get; set; }
        public string TenHinhThucNhanKetQua { get; set; }
    }
    public class SuaHoSoQuyTrinh
    {
        public ThongTinNguoiNop thongTinNguoiNop { get; set; }
        public ThongTinKhuDat thongTinKhuDat { get; set; }
        public HoSoDinhKem hoSoDinhKem { get; set; }
        public List<Por_FileHoSoNguoiNop> fileHoSoNguoiNops { get; set; }
    } 
    public class HoSoQuyTrinh
    {
        public Guid id { get; set; }
        public string token { get; set; }
        public string taiKhoanNguoiNop { get; set; }
        //
        public string NguoiTamDung { get; set; }
        public Guid? TieuChiTamDung { get; set; }
        public string NoiDungTamDung { get; set; }
        public string NgayBatDauTamDung { get; set; }
        public string NgayKetThucTamDung { get; set; }
        //
        public LoaiHoSo loaiHoSo { get; set; }
        public ThongTinNguoiNop thongTinNguoiNop { get; set; }
        public HoSoDinhKem hoSoDinhKem { get; set; }
    }
}
