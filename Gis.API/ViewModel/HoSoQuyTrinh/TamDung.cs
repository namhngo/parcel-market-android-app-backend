using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.HoSoQuyTrinh
{
    public class TamDung
    {
        public Guid Id { get; set; }
        public string NoiDung { get; set; }
        public Guid? TieuChi { get; set; }
    }
}
