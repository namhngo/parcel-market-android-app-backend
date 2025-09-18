using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.Por_BuocQuyTrinh
{
    public class SaveAndUploadData
    {        
        public string Ten { get; set; }
        public string GhiChu { get; set; }
        public Guid IDQuyTrinh { get; set; }
        public IFormFile Files { get; set; }
    }
}
