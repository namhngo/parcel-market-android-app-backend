using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Model
{
    [Table("Por_GisSoNha")]
    public class Por_GisSoNha : AuditEntity
    {
        [StringLength(155)]
        public string SoThua { get; set; }
        [StringLength(155)]
        public string SoTo { get; set; }
        [StringLength(155)]
        public string SoNha { get; set; }
        [StringLength(155)]
        public string TenDuong { get; set; }
        [StringLength(155)]
        public string ApKhuPho { get; set; }
        [StringLength(155)]
        public string TenPhuongXa { get; set; }
        [StringLength(155)]
        public string MaPX { get; set; }
       
    }
}
