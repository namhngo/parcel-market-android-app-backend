using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.Files
{
    public class FileView
    {
        public Guid Id { get; set; }
        public Guid IdBuocQuyTrinh { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }        
    }
}
