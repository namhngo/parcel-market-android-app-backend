using Gis.Core.Enums;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Gis.Core.Core;
using static Gis.API.Infrastructure.Enums;

namespace Gis.API.Model
{
    [Table("Por_BinhLuanThich")]
    public class Por_BinhLuanThich : AuditEntity
    {
        public Guid IDGopYPhanAnh { get; set; }
        public Guid IDBinhLuanGopYPhanAnh { get; set; }        
    }
}
