using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.HoSoQuyTrinh
{
    public class HoSoHoanThanh
    {
        public Guid Id { get; set; }
        public string TenThuTuc { get; set; }
        public string SoHoSo { get; set; }
        public string TrangThai { get; set; }
        public string NgayNop { get; set; }        
        public string NgayHoanThanh { get; set; }
    }
}
