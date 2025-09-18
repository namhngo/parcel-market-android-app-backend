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
    [Table("Por_TemplatePhieuBienNhan")]
    public class Por_TemplatePhieuBienNhan : AuditEntity
    {  
        [StringLength(55)]
        [ColumnNameAttr("category")]
        public string Ma { get; set; }
        [StringLength(500)]
        public string CotSql { get; set; }
        [StringLength(155)]
        [ColumnNameAttr("category")]
        public string TieuDe { get; set; }
        [MaxLength]
        public string NoiDung { get; set; }
    }
}
