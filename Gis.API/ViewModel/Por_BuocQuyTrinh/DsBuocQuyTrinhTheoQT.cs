using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.Por_BuocQuyTrinh
{
    public class DsBuocQuyTrinhTheoQT
    {
        public Guid Id { get; set; }
        public string Ten { get; set; }
        public int ThoiGianThucHien { get; set; }
        public int ThuocThoiGianThucHien { get; set; }
        public Guid IDQuyTrinh { get; set; }
        public Guid IDChucNangBuocQuyTrinh { get; set; }
        public string TenChucNangBuocQuyTrinh { get; set; }
        public int ThuTuBuoc { get; set; }
        public string IDsNguoiDungThamGia { get; set; }
        public bool GuiEmail { get; set; }
        public Guid? IDMauEmail { get; set; }
        public bool GuiSms { get; set; }
        public Guid? IDMauSms { get; set; }
    }
}
