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
    [Table("Por_HoSo_Buoc_QuyTrinh")]
    public class Por_HoSo_Buoc_QuyTrinh : AuditEntity
    {
        public Guid IDHoSo { get; set; }
        public Guid IDQuyTrinh { get; set; }
        public Guid IDBuocQuyTrinh { get; set; }
        public Guid IDTrangThai { get; set; }
        public Guid IDTrangThai_TamDung { get; set; }
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
        public Guid TieuChiTamDung { get; set; }
        [StringLength(55)]
        public string NguoiTamDung { get; set; }
        [StringLength(55)]
        public string NguoiTiepTuc { get; set; }
        [StringLength(550)]
        public string NoiDungTamDung { get; set; }
        public DateTimeOffset? NgayBatDauTamDung { get; set; }
        public DateTimeOffset? NgayKetThucTamDung { get; set; }
    }
}
