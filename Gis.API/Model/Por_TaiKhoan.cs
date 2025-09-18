using Gis.Core.Enums;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Gis.Core.Core;

namespace Gis.API.Model
{
    [Table("Por_TaiKhoan")]
    public class Por_TaiKhoan : AuditEntity
    {
        [StringLength(55)]
        public string CMND { get; set; }
        [StringLength(55)]
        public string HoTen { get; set; }
        [StringLength(55)]
        public string TenDangNhap { get; set; }
        [StringLength(55)]
        public string MatKhau { get; set; }
        [StringLength(55)]
        public string Email { get; set; }
        [StringLength(20)]
        public string SoDienThoai { get; set; }
        [StringLength(100)]
        public string DiaChi { get; set; }
        public bool TrangThai { get; set; }             
    }
}
