using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.HoSoQuyTrinh
{
    public class InPhieuTiepNhan
    {
        public string SoHoSo { get; set; }
        public string HovatenNguoiNop { get; set; }
        public string HovatenNguoiNhan { get; set; }
        public string Email { get; set; }
        public string Dienthoai { get; set; }
        public string DiaChi { get; set; }
        public string VeViec { get; set; }
        public string NgayNhan { get; set; }
        public string NgayTraKetQua { get; set; }
        public List<Model.Por_FileMauThanhPhanHStrongQT> Thanhphanhoso { get; set; }
    }
}
