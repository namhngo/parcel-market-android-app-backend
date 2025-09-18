using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.PhanAnh
{
    public class ListPhanAnh
    {
        public Guid Id { get; set; }
        public string MaPhanAnh { get; set; }
        public string LinhVuc { get; set; }
        public string TenNguoiGui { get; set; }
        public string TieuDe { get; set; }
        public string NgayGui { get; set; } 
        public DateTimeOffset NgayGuiDate { get; set; }
        public string TrangThai { get; set; }
        public string SoDienThoai { get; set; }
        public int ThoiGianXuLy { get; set; }
    }
}
