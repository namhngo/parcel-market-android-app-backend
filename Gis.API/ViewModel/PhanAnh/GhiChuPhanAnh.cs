using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.PhanAnh
{
    public class GhiChuPhanAnh
    {
        public Guid Id { get; set; }
        public Guid IDTrangThaiPA { get; set; }
        public int ThoiGianXuLy { get; set; }
        public string GhiChu { get; set; }
    }
}
