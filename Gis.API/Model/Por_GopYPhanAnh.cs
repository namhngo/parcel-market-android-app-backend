using Gis.Core.Enums;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Gis.Core.Core;
using static Gis.API.Infrastructure.Enums;

namespace Gis.API.Model
{
    [Table("Por_GopYPhanAnh")]
    public class Por_GopYPhanAnh : AuditEntity
    {
        [MaxLength(100)]
        public string MaPhanAnh { get; set; }        
        public Guid IDLinhVuc { get; set; }
        [StringLength(100)]
        public string TenNguoiGui { get; set; }
        [StringLength(55)]
        public string TaiKhoanNguoiGui { get; set; }
        [StringLength(20)]
        public string SoDienThoai { get; set; }
        [StringLength(55)]
        public string Email { get; set; }
        [StringLength(155)]
        public string DiaChi { get; set; }
        [StringLength(200)]
        public string TieuDe { get; set; }
        [StringLength(2000)]
        public string NoiDung { get; set; }
        [StringLength(2000)]
        public string GhiChu { get; set; }
        [StringLength(250)]
        public string TenFile { get; set; }
        [StringLength(250)]
        public string URL { get; set; }
        public int ThoiGianXuLy { get; set; }
        public Guid IDTrangThaiPA { get; set; }
        public bool CongKhai { get; set; }
        public Guid TinhThanhPho { get; set; }
        public Guid QuanHuyen { get; set; }
        public Guid PhuongXa { get; set; }
        [StringLength(100)]
        public string SoNha { get; set; }
        [StringLength(150)]
        public string TenDuong { get; set; }
    }
}
