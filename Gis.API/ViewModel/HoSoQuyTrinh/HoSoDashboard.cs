using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.HoSoQuyTrinh
{
    public class HoSoDashboard
    {
        public List<LoaiHoSoDashboard> items { get; set; }
    }
    public class LoaiHoSoDashboard
    {
        public string TenThuTuc { get; set; }
        public bool HoSoDVC { get; set; }
        public int HoSoChoTiepNhan { get; set; }
        public int HoSoDangXyLy { get; set; }
        public int HoSoTraKetQua { get; set; }
        public int HoSoHuy { get; set; }
    }
}
