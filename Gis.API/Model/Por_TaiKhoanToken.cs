using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Model
{
    [Table("Por_TaiKhoanToken")]
    public class Por_TaiKhoanToken : AuditEntity
    {
        [StringLength(55)]
        public string TenDangNhap { get; set; }
        [StringLength(800)]
        public string TokenTruyCap { get; set; }
        [StringLength(500)]
        public string TokenLamMoi { get; set; }
    }
}
