using System;

namespace Gis.API.ViewModel.BaoCao
{
    public class SearchHoSoTheoTungLoai
    {
        public Guid[] LoaiHoSo { get; set; }
        public string[] CanBoXuLy { get; set; }
        public Guid[] TrangThaiHoSo { get; set; }
        public DateTimeOffset? TuNgay { get; set; }
        public DateTimeOffset? DenNgay { get; set; }
    }
}
