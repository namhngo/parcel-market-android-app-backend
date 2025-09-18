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
    [Table("Por_HoSoNguoiNop")]
    public class Por_HoSoNguoiNop : AuditEntity
    {
        [StringLength(55)]
        public string SoHoSo { get; set; }
        [StringLength(55)]
        public string HoTen { get; set; }
        [StringLength(55)]
        public string TaiKhoanNguoiGui { get; set; }
        [StringLength(55)]
        public string CMND { get; set; }        
        [StringLength(55)]
        public string Email { get; set; }
        [StringLength(20)]
        public string SoDienThoai { get; set; }
        public Guid TinhThanhPho { get; set; }
        public Guid QuanHuyen { get; set; }
        public Guid PhuongXa { get; set; }
        [StringLength(100)]
        public string SoNha { get; set; }
        [StringLength(150)]
        public string TenDuong { get; set; }
        [StringLength(100)]
        public string DiaChi { get; set; }
        public Guid IDHinhThucNhanKetQua { get; set; }
        public Guid IDMucDichSuDung { get; set; }
        public Guid? IDHinhThucThanhToan { get; set; }
        public Guid IDTrangThaiHS { get; set; }
        //public Guid IDThuaDat { get; set; }
        public bool CongKhai { get; set; }        
        public bool? ThanhToan { get; set; }
    }
}
