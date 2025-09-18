using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Model
{
    [Table("Por_HoSoPA_Buoc_QuyTrinh")]
    public class Por_HoSoPA_Buoc_QuyTrinh : AuditEntity
    {
        public Guid IDHoSo { get; set; }
        public Guid IDQuyTrinh { get; set; }
        public Guid IDBuocQuyTrinh { get; set; }
        public Guid IDTrangThai { get; set; }
        [StringLength(55)]
        public string NguoiXuLy { get; set; }
        [StringLength(500)]
        public string NoiDungXuLy { get; set; }
        [StringLength(55)]
        public string NguoiTraNguocLai { get; set; }
        [StringLength(500)]
        public string NoiDungTraNguocLai { get; set; }        
        public DateTimeOffset? NgayTraNguocLai { get; set; }
        public DateTimeOffset NgayBatDau { get; set; }
        public DateTimeOffset? NgayKetThuc { get; set; }                
    }
}
