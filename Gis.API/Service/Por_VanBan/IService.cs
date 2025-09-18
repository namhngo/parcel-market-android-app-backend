using Gis.API.ViewModel.Portal;
using Gis.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gis.API.Model;
using System;
using Gis.API.ViewModel.VanBanPhapQuy;
using Gis.Core.Models;

namespace Gis.API.Service.Por_VanBan
{
    public interface IService: IRepositoryBase<Por_VanBanPhapQuy>
    {
        public Task<List<Por_VanBanPhapQuy>> GetVanBanAdmin();
        public Task<Paged<Model.Por_VanBanPhapQuy>> GetItemsByLoaiVanBanId(int page, int pageSize, int totalLimitItems, string loaiVanBanId);
        public Task<List<Por_VanBanPhapQuy>> GetVanBanPortal();
        public Task<ChiTietVanBan> GetChiTietVanBan(Guid Id);
        public Task<List<Por_LoaiVanBanPhapQuy>> GetDsLoaiVanBan();
        public Task<List<Por_VanBanPhapQuy>> GetDSVanBan(Guid idLoaiVanBan, string search);
        public Task AddLoaiVanBanAdmin(Por_LoaiVanBanPhapQuy lvb);
        public Task AddVanBanAdmin(Por_VanBanPhapQuy vb);
        public Task DeleteLoaiVanBanById(Guid Id);
        public Task DeleteVanBanById(Guid Id);
        
    }
}
