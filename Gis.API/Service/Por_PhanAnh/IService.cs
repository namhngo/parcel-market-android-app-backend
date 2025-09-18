using Gis.API.ViewModel.Portal;
using Gis.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gis.API.Model;
using System;
using Gis.API.ViewModel.PhanAnh;
using Gis.Core.Models;

namespace Gis.API.Service.Por_PhanAnh
{
    public interface IService : IRepositoryBase<Por_GopYPhanAnh>
    {
        Task<Paged<ListPhanAnh>> GetPagedCustomAsync(int page, int pageSize, int totalLimitItems, string search);
        public Task<List<GopYPhanAnh>> GetDSPhanAnh(string search, string userName);
        public Task<NoiDungPhanAnh> XemNoiDungPhanAnh(Guid Id);
        public Task<ChiTietPhanAnh> XemChiTietPhanAnh(Guid Id);
        public Task<Por_GopYPhanAnh> GuiPhanAnh(Por_GopYPhanAnh model);
        public Task GhiChuPhanAnh(GhiChuPhanAnh ghiChuPhanAnh);
        public Task BinhLuanPhanAnh(BinhLuanPhanAnh binhLuanPhanAnh);
        public Task CongKhaiPA(Guid id, bool congKhai);
        public Task<BinhLuanPhanAnh> LayDSBinhLuanPhanAnh(Guid IDGopYPhanAnh, string TaiKhoan);
    }
}

