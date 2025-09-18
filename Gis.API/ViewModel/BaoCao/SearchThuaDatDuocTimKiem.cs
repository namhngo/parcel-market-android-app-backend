using System;

namespace Gis.API.ViewModel.BaoCao
{
    public class SearchThuaDatDuocTimKiem
    {
        public Guid[] XaPhuong { get; set; }
        public string SoTo { get; set; }
        public string SoThua { get; set; }
        public DateTimeOffset? TuNgay { get; set; }
        public DateTimeOffset? DenNgay { get; set; }
    }
}
