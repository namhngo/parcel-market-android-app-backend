using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.HoSoPaQuyTrinh
{
    public class HoSoPAHuy
    {
        public Guid Id { get; set; }
        public string NoiDungHuy { get; set; }
        public string MaPhanAnh { get; set; }
        public string LoaiPhanAnh { get; set; }
        public string TrangThai { get; set; }
        public string NgayNop { get; set; }
        public string NgayHuy { get; set; }
        public string NguoiHuy { get; set; }
    }
}
