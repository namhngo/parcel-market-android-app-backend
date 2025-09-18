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
    [Table("Por_FileHoSoNguoiNop")]
    public class Por_FileHoSoNguoiNop : AuditEntity
    {
        public Guid IDFileMauThanhPhanHStrongQT { get; set; }
        [StringLength(150)]
        public string Ten { get; set; }
        [StringLength(250)]
        public string URL { get; set; }
        public Guid IDHoSoNguoiNop { get; set; }
    }
}
