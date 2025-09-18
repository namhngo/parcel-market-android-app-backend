using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.HoSoQuyTrinh
{
    public class HoSoPAHoanThanh
    {
        public Guid Id { get; set; }
        public string MaPhanAnh { get; set; }
        public string LoaiPhanAnh { get; set; }
        public string TrangThai { get; set; }
        public string NgayNop { get; set; }        
        public string NgayHoanThanh { get; set; }
    }
}
