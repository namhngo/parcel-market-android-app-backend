using Gis.Core.Core;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Model
{
    [Table("Por_ChucNang_BuocQuyTrinh")]
    public class Por_ChucNang_BuocQuyTrinh : AuditEntity
    {
        [StringLength(155)]
        [ColumnNameAttr("category")]
        public string Ten { get; set; }     
        public bool TiepNhanHoSo { get; set; }
        public bool CapNhatHoSo { get; set; }
        public bool ChuyenBuocKeTiep { get; set; }
        public bool TraNguocLai { get; set; }
        public bool TamDungHoSo { get; set; }
        public bool HuyHoSo { get; set; }
        public bool TraKetQua { get; set; }
        public bool XemQuyTrinh { get; set; }
    }
}
