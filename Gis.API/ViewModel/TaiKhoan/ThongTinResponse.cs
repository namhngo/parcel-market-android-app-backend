using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.TaiKhoan
{
    public class ThongTinResponse
    {
        public Guid Id { get; set; }        
        public string HoTen { get; set; }        
        public string TenDangNhap { get; set; }        
        public string Email { get; set; }
        public string SoDienThoai { get; set; }        
        public string DiaChi { get; set; }
    }
}
