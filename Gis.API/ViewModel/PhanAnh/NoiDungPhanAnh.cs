using Gis.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.PhanAnh
{
    public class NoiDungPhanAnh
    {
        public Guid Id { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string NoiDungTraKetQua { get; set; }
        public List<Por_FileBuocQuyTrinh> FileTraKetQua { get; set; }
    }
}
