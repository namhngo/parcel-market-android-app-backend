using Gis.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.ViewModel.PhanAnh
{
    public class ChiTietPhanAnh
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string MaPhanAnh { get; set; }
        public string TenNguoiGui { get; set; }        
        public string SoDienThoai { get; set; }        
        public string Email { get; set; }
        public string TinhThanhPho { get; set; }
        public string QuanHuyen { get; set; }
        public string PhuongXa { get; set; }        
        public string SoNha { get; set; }        
        public string TenDuong { get; set; }
        public Guid IDLinhVuc { get; set; }
        public string LinhVuc { get; set; }
        public string TieuDe { get; set; }        
        public string NoiDung { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public bool CongKhai { get; set; }
        public string NoiDungTraKetQua { get; set; }
        public List<Por_FileBuocQuyTrinh> FileTraKetQua { get; set; }
    }
}
