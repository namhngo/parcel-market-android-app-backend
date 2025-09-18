using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.HoSoQuyTrinh
{
    public class HoSoXuLy
    {
        public Guid Id { get; set; }
        public Guid IDQuyTrinh { get; set; }
        public Guid IDBuocQuyTrinh { get; set; }
        public string TenThuTuc { get; set; }
        public string SoHoSo { get; set; }
        public string TrangThai { get; set; }
        public long NgayNopInt { get; set; }
        public string NguoiNop { get; set; }
        public string NgayNop { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public string NgayTiepNhan { get; set; }
        public string NguoiDangTiepNhan { get; set; }
        public bool? ThanhToan { get; set; }
    }
}
