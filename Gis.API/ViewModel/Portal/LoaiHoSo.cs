using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.Portal
{
    public class LoaiHoSo
    {
        public Guid Id { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string TenDichVuCungCap { get; set; }
        public int ThoiGianThucHien { get; set; }
        public string GiaTien { get; set; }
        public bool MienPhi {get;set;}
    }
}
