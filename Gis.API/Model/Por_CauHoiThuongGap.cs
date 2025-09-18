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
    [Table("Por_CauHoiThuongGap")]
    public class Por_CauHoiThuongGap : AuditEntity
    {
        [StringLength(250)]
        public string CauHoi { get; set; }
        [StringLength(500)]
        public string CauTraLoi { get; set; }
        public bool TrangThai { get; set; }
        public int STT { get; set; }
    }
}
