using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Model
{
    [Table("Por_GCNQSDD")]
    public class Por_GCNQSDD : AuditEntity
    {
        [StringLength(155)]
        public string SoHieu { get; set; }
        [StringLength(155)]
        public string NgayCap { get; set; }
        [StringLength(155)]
        public string NguoiSuDung { get; set; }
        [StringLength(155)]
        public string DiaChiThuongTru { get; set; }
        [StringLength(155)]
        public string CCCD { get; set; }
        [StringLength(155)]
        public string NguoiKy { get; set; }
        [StringLength(155)]
        public string SoThua { get; set; }
        [StringLength(155)]
        public string SoTo { get; set; }
        [StringLength(155)]
        public string DiaChiThuaDat { get; set; }
        [StringLength(155)]
        public string TenPhuongXa { get; set; }
        [StringLength(155)]
        public string MaPX { get; set; }
        [StringLength(155)]
        public string DienTich { get; set; }
        [StringLength(155)]
        public string MucDichSuDung { get; set; }
        [StringLength(155)]
        public string ThoiHanSuDung { get; set; }
        [StringLength(155)]
        public string NhaO { get; set; }
        [StringLength(155)]
        public string CongTrinh { get; set; }
        [StringLength(155)]
        public string RungSanXuat { get; set; }
        [StringLength(155)]
        public string CayLauNam { get; set; }
    }
}
