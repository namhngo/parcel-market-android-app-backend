using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.HoSoPaQuyTrinh
{
    public class TraCuuHoSoPA
    {
        public Guid Id { get; set; }
        public string LinhVuc { get; set; }
        public string MaPhanAnh { get; set; }
        public string TrangThai { get; set; }
        public string NgayNop { get; set; }
        public string NguoiNop { get; set; }
        public string NgayTiepNhan { get; set; }
        public string NguoiTiepNhan { get; set; }
        public string TenNguoiGui { get; set; }
        public string TieuDe { get; set; }
        public string NgayGui { get; set; }
        public DateTimeOffset NgayGuiDate { get; set; }
        public string SoDienThoai { get; set; }
        public int ThoiGianXuLy { get; set; }
        public Guid IdThuTuc { get; set; }
    }
}
