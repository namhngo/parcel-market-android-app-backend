using Gis.Core.Enums;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Model
{
    [Table("Sys_LogSearchGis")]
    public class Por_LogSearchGis : AuditEntity
    {
        public Guid IDLog { get; set; }
        [StringLength(150)]
        public string Huyen { get; set; }
        [StringLength(150)]
        public string TenPhuongXa { get; set; }
        public string Thua { get; set; }        
        public string To { get; set; }
        [StringLength(150)]
        public string SoNha { get; set; }
        [StringLength(250)]
        public string TenDuong { get; set; }        
        public DateTimeOffset? NgayTim { get; set; }        
        public string UserName { get; set; }        
    }
}
