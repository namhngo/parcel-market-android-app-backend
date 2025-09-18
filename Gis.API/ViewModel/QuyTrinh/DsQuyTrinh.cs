using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.QuyTrinh
{
    public class DsQuyTrinh
    {
        public Guid Id { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string TenLinhVuc { get; set; }
        public string TenMucDo { get; set; }
        public int ThoiGianThucHien { get; set; }
        public string GiaTien { get; set; }        
    }
}
