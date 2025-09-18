using Gis.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.PhanAnh
{
    public class BinhLuanPhanAnh
    {
        public Guid Id { get; set; }
        public string Token { get; set; }        
        public string NoiDung { get; set; }
        public bool Thich { get; set; }
        public string TaiKhoan { get; set; }
        public int SLThich { get; set; }
        public bool CoThich { get; set; }
        public int SLKhongThich { get; set; }
        public bool KhongThich { get; set; }
    }
}
