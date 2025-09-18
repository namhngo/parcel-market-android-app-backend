using Gis.Core.Core;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Model
{
    [Table("Por_TinhThanhPho")]
    public class Por_TinhThanhPho : AuditEntity
    {
        [StringLength(100)]
        [ColumnNameAttr("category")]
        public string Ma { get; set; }
        [StringLength(150)]
        [ColumnNameAttr("category")]
        public string Ten { get; set; }
        [StringLength(150)]
        public string TenTiengAnh { get; set; }
        [StringLength(100)]
        public string Cap { get; set; }
    }
}
