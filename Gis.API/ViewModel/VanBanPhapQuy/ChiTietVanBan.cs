using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.VanBanPhapQuy
{
    public class ChiTietVanBan
    {
        
        public string TieuDe { get; set; }        
        public string NoiDung { get; set; }        
        public string SoHieuVanBan { get; set; }
        public bool TrangThai { get; set; }
        public string TenLoaiVanBanPhapQuy { get; set; }
        public DateTimeOffset NgayBanHanh { get; set; }
        public int STT { get; set; }        
        public string TenFile { get; set; }        
        public string URL { get; set; }
    }
}
