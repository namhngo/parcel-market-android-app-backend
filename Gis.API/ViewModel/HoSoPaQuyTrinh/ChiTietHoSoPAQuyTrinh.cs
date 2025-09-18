using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.HoSoPaQuyTrinh
{
    public class ChiTietHoSoPAQuyTrinh
    {
        public Guid Id { get; set; }
        public Guid IDQuyTrinh { get; set; }        
        public string HoTen { get; set; }        
        public string Email { get; set; }
        public string SoDienThoai { get; set; }        
        public Guid TinhThanhPho { get; set; }
        public Guid QuanHuyen { get; set; }
        public Guid PhuongXa { get; set; }        
        public string SoNha { get; set; }        
        public string TenDuong { get; set; }
        public Guid IDHinhThucNhanKetQua { get; set; }
        public Guid IDMucDichSuDung { get; set; }
        public Guid IDHinhThucThanhToan { get; set; }
        public List<Model.Por_FileHoSoNguoiNop> FileHoSoNguoiNop { get; set; }
    }
}
