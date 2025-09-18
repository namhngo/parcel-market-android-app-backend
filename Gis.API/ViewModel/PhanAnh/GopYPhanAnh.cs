using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.PhanAnh
{
    public class GopYPhanAnh
    {
        public Guid Id { get; set; }
        public string MaPhanAnh { get; set; }
        public string TieuDe { get; set; }
        public string TenNguoiGui { get; set; }
        public string SoDienThoai { get; set; }
        public string TaiKhoanNguoiGui { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public string ThoiGianXuLy { get; set; }
        public string ThoiGianXuLyDuKien { get; set; }
        public string GhiChu { get; set; }
        public string TrangThai { get; set; }
        public Guid IDTrangThaiPA { get; set; }
        public Guid IdChucNang { get; set; }
    }
}
