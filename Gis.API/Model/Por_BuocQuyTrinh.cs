using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Model
{
    [Table("Por_BuocQuyTrinh")]
    public class Por_BuocQuyTrinh : AuditEntity
    {
        [StringLength(155)]
        public string Ten { get; set; }
        public int ThoiGianThucHien { get; set; }
        public int ThuocThoiGianThucHien { get; set; }
        public Guid IDQuyTrinh { get; set; }
        public Guid IDChucNangBuocQuyTrinh { get; set; }
        public int ThuTuBuoc { get; set; }
        public string IDsNguoiDungThamGia { get; set; }
        public bool GuiEmail { get; set; }
        public Guid? IDMauEmail { get; set; }
        public bool GuiSms { get; set; }
        public Guid? IDMauSms { get; set; }
    }
}
