using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.HoSoQuyTrinh
{
    public class HoSo
    {
        public Guid Id { get; set; }
        public string SoHoSo { get; set; }
        public string TaiKhoanNguoiGui { get; set; }
        public string NgayTiepNhan { get; set; }
        public string CreatedDateTime { get; set; }
        public string TenThuTuc { get; set; }
        public string GiaTien { get; set; }
        public string TinhTrang { get; set; }
        public string ThoiDiemDuKienXuLyHoanTat { get; set; }
        public string ThoiDiemXuLyHoanTat { get; set; }
        public bool ChoPhepSua { get; set; }
        public bool ChoTiepTuc { get; set; }
    }
}
