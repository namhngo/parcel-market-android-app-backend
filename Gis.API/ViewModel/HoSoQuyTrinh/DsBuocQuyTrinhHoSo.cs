using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Gis.API.Infrastructure.Enums;

namespace Gis.API.ViewModel.HoSoQuyTrinh
{
    public class DsBuocQuyTrinhHoSo
    {
        public Guid Id { get; set; }
        public Guid IdQuyTrinh { get; set; }
        public Guid IdHoSoBuocQuyTrinh { get; set; }
        public string TenBuoc { get; set; }
        public string TenChucNang { get; set; }
        public int ThoiGianThucHien { get; set; }
        public int ThuTuBuoc { get;set; }
        public string NguoiDungThamGia { get; set; }
        public string TrangThai { get; set; }
        public bool DangHoatDong { get; set; }
        public string TreHan { get; set; }
        public string NguoiXuLy { get; set; }
        public string NguoiTraLai { get; set; }
        public string NguoiGui { get; set; }
        public string NoiDungXuLy { get; set; }
        public string NoiDungTraLai { get; set; }
        public string NgayTraLai { get; set; }
        public string NgayBatDauXuLy { get; set; }
        public string NgayKetThucXuLy { get; set; }
        public string NgayGuiHoSo { get; set; }
        public string NguoiTamDung { get; set; }
        public string NoiDungTamDung { get; set; }
        public Guid? TieuChiTamDung { get; set; }
        public string NgayBatDauTamDung { get; set; }
        public string NgayKetThucTamDung { get; set; }
        public List<ViewModel.Files.FileView> Files { get; set; } 
    }
}
