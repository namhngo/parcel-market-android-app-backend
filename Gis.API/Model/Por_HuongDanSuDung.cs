using Gis.Core.Enums;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Gis.Core.Core;

namespace Gis.API.Model
{
    [Table("Por_HuongDanSuDung")]
    public class Por_HuongDanSuDung : AuditEntity
    {
        [StringLength(250)]
        public string TieuDe { get; set; }
        [MaxLength]
        public string NoiDung { get; set; }
        public bool TrangThai { get; set; }
    }
}
