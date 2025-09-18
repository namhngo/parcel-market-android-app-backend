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
    [Table("Por_LinhVuc")]
    public class Por_LinhVuc : AuditEntity
    {
        [StringLength(55)]
        public string Ma { get; set; }
        [StringLength(55)]
        public string So { get; set; }
        [StringLength(155)]
        public string Ten { get; set; }
        public Guid IDCha { get; set; } = Guid.Empty;
    }
}
