using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.HoSoPaQuyTrinh
{
    public class LuuHoSoPAQuyTrinh
    {
        public Guid Id { get; set; }        
        public Guid IDQuyTrinh { get; set; }
        public Guid IDBuocQuyTrinhDauTien { get; set; }
        public Guid IDMucDichSuDung { get; set; }
        public string HoTen { get; set; }        
        public string CMND { get; set; }        
        public string Email { get; set; }        
        public string SoDienThoai { get; set; }        
        public string DiaChi { get; set; }
    }
}
