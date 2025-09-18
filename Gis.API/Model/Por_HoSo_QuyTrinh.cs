using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static Gis.API.Infrastructure.Enums;

namespace Gis.API.Model
{
    [Table("Por_HoSo_QuyTrinh")]
    public class Por_HoSo_QuyTrinh : AuditEntity
    {
        public Guid IDHoSo { get; set; }
        public Guid IDQuyTrinh { get; set; }
        public Guid IDBuocQuyTrinh { get; set; }
        public DateTimeOffset NgayNop { get; set; }        
        public DateTimeOffset? NgayNhanKQ { get; set; }
        public DateTimeOffset? NgayHuy { get; set; }        
        public DateTimeOffset? NgayTiepNhan { get; set; }
        public DateTimeOffset? NgayGuiHoSo { get; set; }
        public DateTimeOffset NgayDuKienNhanKQ { get; set; }
        [StringLength(55)]
        public string NguoiHuy { get; set; }
        [StringLength(550)]
        public string NoiDungHuy { get; set; }
        [StringLength(55)]
        public string NguoiTraKetQua { get; set; }
        [StringLength(55)]
        public string NguoiXuLy { get; set; }
        [StringLength(55)]
        public string NguoiGui { get; set; }
        [StringLength(55)]
        public string NguoiTiepNhan { get; set; }

        public int ThoiGianThucHien { get; set; }
    }
}
