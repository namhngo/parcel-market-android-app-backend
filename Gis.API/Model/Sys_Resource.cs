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
    [Table("Sys_Resources")]
    public class Sys_Resource : AuditEntity
    {
        [StringLength(55)]
        public string Code { get; set; }
        [StringLength(55)]
        public string Name { get; set; }
        [StringLength(55)]
        public string Url { get; set; }
        [StringLength(55)]
        public string Icon { get; set; }
        public ResourceType Type { get; set; }
        public Guid ParentId { get; set; } = Guid.Empty;
    }
}
