using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Model
{
    [Table("Por_GPXD")]
    public class Por_GPXD : AuditEntity
    {
        [StringLength(155)]
        public string ChuDauTu { get; set; }
        [StringLength(155)]
        public string SoThua { get; set; }
        [StringLength(155)]
        public string SoTo { get; set; }
        [StringLength(155)]
        public string TenXa { get; set; }
        [StringLength(155)]
        public string MaPX { get; set; }
        [StringLength(155)]
        public string SoGPXD { get; set; }
        [StringLength(155)]
        public string NgayCapGPXD { get; set; }
        [StringLength(155)]
        public string DienTichXayDung { get; set; }
        [StringLength(155)]
        public string TongDienTichSanXayDung { get; set; }
        [StringLength(155)]
        public string SoTang { get; set; }
        [StringLength(155)]
        public string GhiChu { get; set; }
    }
}
