using System;

namespace Gis.API.ViewModel.BaoCao
{
    public class SearchTiepNhanHoSo
    {
        public Guid[] LoaiHoSo { get; set; }
        public Guid[] TrangThaiHoSo { get; set; }
        public DateTimeOffset? TuNgay { get; set; }
        public DateTimeOffset? DenNgay { get; set; }
    }
}
