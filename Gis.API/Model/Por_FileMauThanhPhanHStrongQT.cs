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
    [Table("Por_FileMauThanhPhanHStrongQT")]
    public class Por_FileMauThanhPhanHStrongQT : AuditEntity
    {
        [StringLength(150)]
        public string Ten { get; set; }
        [StringLength(150)]
        public string GhiChu { get; set; }
        [StringLength(250)]
        public string TenFile { get; set; }
        [StringLength(250)]
        public string URL { get; set; }
        public Guid IDQuyTrinh { get; set; }
    }
}