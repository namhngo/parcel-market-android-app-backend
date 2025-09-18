using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.HoSoQuyTrinh
{
    public class HoSoTamDung
    {
        public Guid Id { get; set; }
        public string SoHoSo { get; set; }
        public string TrangThai { get; set; }
        public string NgayNop { get; set; }
        public string NgayBatDauTamDung { get; set; }
        public string NgayKetThucTamDung { get; set; }
    }
}
