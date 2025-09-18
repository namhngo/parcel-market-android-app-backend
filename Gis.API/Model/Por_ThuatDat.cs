using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Model
{
    [Table("Por_ThuatDat")]
    public class Por_ThuatDat : AuditEntity
    {
        public Guid IDHoSoNguoiNop { get; set; }
        [StringLength(55)]
        public string SoTo { get; set; }
        [StringLength(55)]
        public string SoThua { get; set; }
        public Guid TinhThanhPho { get; set; }
        public Guid QuanHuyen { get; set; }
        public Guid PhuongXa { get; set; }
        [StringLength(100)]
        public string SoNha { get; set; }
        [StringLength(150)]
        public string TenDuong { get; set; }
        [StringLength(500)]
        public string DiaChi { get; set; }        
    }
}
