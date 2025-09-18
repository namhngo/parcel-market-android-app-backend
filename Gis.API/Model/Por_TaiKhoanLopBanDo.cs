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
    [Table("Por_TaiKhoanLopBanDo")]
    public class Por_TaiKhoanLopBanDo : AuditEntity
    {
        public Guid IdLopBanDo { get; set; }
                    
    }
}
