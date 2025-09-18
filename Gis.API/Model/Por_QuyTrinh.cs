using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Model
{
    [Table("Por_QuyTrinh")]
    public class Por_QuyTrinh : AuditEntity
    {
        [StringLength(155)]
        public string MaThuTuc { get; set; }
        [StringLength(155)]
        public string TenThuTuc{ get; set; }        
        public Guid IDLinhVuc { get; set; }
        public Guid? IDLoaiPhanAnh { get; set; }
        public Guid IDDichVuCungCap { get; set; }
        public Guid IDCapDoThuTuc { get; set; }
        public Guid IDMucDo { get; set; }
        public Guid IDHinhThucCap { get; set; }
        public int ThoiGianThucHien { get; set; }
        public bool CongKhai { get; set; }
        public bool MienPhi { get; set; }
        [StringLength(100)]
        public string GiaTien { get; set; }
    }
}
