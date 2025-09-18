using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.HoSoPaQuyTrinh
{
    public class HoSoPAXuLy
    {
        public Guid Id { get; set; }
        public Guid IDQuyTrinh { get; set; }
        public Guid IDBuocQuyTrinh { get; set; }
        public string MaPhanAnh { get; set; }
        public string LoaiPhanAnh { get; set; }
        public string TrangThai { get; set; }
        public long NgayNopInt { get; set; }
        public string NguoiNop { get; set; }
        public string NgayNop { get; set; }
        //public string Email { get; set; }
        //public string SDT { get; set; }
        public string NgayTiepNhan { get; set; }
        public string NguoiDangTiepNhan { get; set; }
        public string TieuDe { get; set; }
        public string SoNha { get; set; }
        public string TenDuong { get; set; }
        public string PhuongXa { get; set; }
    }
}
