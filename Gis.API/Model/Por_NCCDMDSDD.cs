using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Model
{
    [Table("Por_NCCDMDSDD")]
    public class Por_NCCDMDSDD : AuditEntity
    {
        [StringLength(155)]
        public string MaHS { get; set; }
        [StringLength(155)]
        public string TenChuSuDung { get; set; }
        [StringLength(155)]
        public string DiaChiThuongTru { get; set; }
        [StringLength(155)]
        public string DiaDiem { get; set; }
        [StringLength(155)]
        public string MaPX { get; set; }
        [StringLength(155)]
        public string TenPhuongXa { get; set; }
        [StringLength(155)]
        public string DiaChiThuaDat { get; set; }
        [StringLength(155)]
        public string SoTo { get; set; }
        [StringLength(155)]
        public string SoThua { get; set; }
        [StringLength(155)]
        public string DienTich { get; set; }
        [StringLength(155)]
        public string LoaiDatHienTrangTheoGCN { get; set; }
        [StringLength(155)]
        public string NhuCauChuyenMucDich { get; set; }
        [StringLength(155)]
        public string GCN { get; set; }
        [StringLength(155)]
        public string ThongTinQuyHoach { get; set; }
        [StringLength(155)]
        public string GhiChu { get; set; }
        [StringLength(155)]
        public string SoDienThoai { get; set; }
    }
}
