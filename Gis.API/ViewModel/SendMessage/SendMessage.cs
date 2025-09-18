using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.SendMessage
{
    public class SendMessage
    {
        public Guid Id { get; set; }
        public string MaHoSo { get; set; }
        public string HoVaTen { get; set; }
        public string Email { get; set; }
        public string SĐT { get; set; }
        public string SoNha { get; set; }
        public string TenDuong { get; set; }
        public Guid TinhThanhPho { get; set; }
        public Guid QuanHuyen { get; set; }
        public Guid PhuongXa { get; set; }
        public string DiaChi { get; set; }
        public string LinhVuc { get; set; }
        public string LoaiPhanAnh { get; set; }
        public string NguoiTraNguocLai { get; set; }

    }
}
