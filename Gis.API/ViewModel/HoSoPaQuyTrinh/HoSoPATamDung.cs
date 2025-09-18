using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.HoSoPaQuyTrinh
{
    public class HoSoPATamDung
    {
        public Guid Id { get; set; }
        public string MaPhanAnh { get; set; }
        public string TrangThai { get; set; }
        public string NgayNop { get; set; }
        public string NgayTamNgung { get; set; }
    }
}
