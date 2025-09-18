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
    [Table("Por_VanBanPhapQuy")]
    public class Por_VanBanPhapQuy : AuditEntity
    {
        [StringLength(250)]
        public string TieuDe { get; set; }
        [MaxLength(255)]
        public string NoiDung { get; set; }
        [MaxLength(100)]
        public string SoHieuVanBan { get; set; }
        public bool TrangThai {get;set;}
        public Guid IDLoaiVanBanPhapQuy { get; set; }
        public DateTimeOffset NgayBanHanh { get; set; }
        public int STT { get; set; }
        [StringLength(250)]
        public string TenFile { get; set; }
        [StringLength(250)]
        public string URL { get; set; }
    }
}
