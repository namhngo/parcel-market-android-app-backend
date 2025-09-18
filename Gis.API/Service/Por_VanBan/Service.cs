using Gis.API.Infrastructure;
using Gis.Core.Interfaces;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Gis.API.Model;
using Microsoft.EntityFrameworkCore;
using Gis.Core.Constant;
using Gis.API.ViewModel.VanBanPhapQuy;
using Gis.Core.Helpers;

namespace Gis.API.Service.Por_VanBan
{
    public class Service : RepositoryBase<Por_VanBanPhapQuy>, IService
    {
        private readonly DomainDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public Service(DomainDbContext dbContext, IDateTimeProvider dateTimeProvider, IUserProvider userService) : base(dbContext,  dateTimeProvider, userService)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userService;
        }
        public async Task<Paged<Model.Por_VanBanPhapQuy>> GetItemsByLoaiVanBanId(int page, int pageSize, int totalLimitItems, string loaiVanBanId)
        {
            var query = _dbContext.Por_VanBanPhapQuys.Where(o => o.IDLoaiVanBanPhapQuy == Guid.Parse(loaiVanBanId)).AsQueryable();
            Paged<Model.Por_VanBanPhapQuy> result = new Paged<Model.Por_VanBanPhapQuy>(query, page, pageSize, totalLimitItems);
            result.Items = await query.Paged(page, pageSize, totalLimitItems).ToListAsync();
            return result;
        }    
        public async Task<List<Por_LoaiVanBanPhapQuy>> GetDsLoaiVanBan()
        {
            var items = await (from x in _dbContext.Por_LoaiVanBanPhapQuys
                               select new Por_LoaiVanBanPhapQuy()
                               {
                                   Id = x.Id,
                                   Ten = x.Ten
                               }).OrderBy(o => o.Ten).ToListAsync();
            return items;
        }
        public async Task<ChiTietVanBan> GetChiTietVanBan(Guid Id)
        {
            var item = await (from x in _dbContext.Por_VanBanPhapQuys
                              join y in _dbContext.Por_LoaiVanBanPhapQuys on x.IDLoaiVanBanPhapQuy equals y.Id
                              where x.Id == Id & x.TrangThai == true
                              select new ChiTietVanBan() {
                                  TieuDe = x.TieuDe,
                                  SoHieuVanBan = x.SoHieuVanBan,
                                  TrangThai = x.TrangThai,
                                  TenLoaiVanBanPhapQuy = y.Ten,
                                  NgayBanHanh = x.NgayBanHanh,
                                  STT = x.STT,
                                  TenFile = x.TenFile,
                                  URL = x.URL
                              }).FirstOrDefaultAsync();
            return item;
        }
        public async Task<List<Por_VanBanPhapQuy>> GetDSVanBan(Guid idLoaiVanBan, string search)
        {
            var query = _dbContext.Por_VanBanPhapQuys.Where(o => o.IDLoaiVanBanPhapQuy == idLoaiVanBan && o.TrangThai == true).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(o => o.SoHieuVanBan.ToLower().Contains(search.ToLower()) || o.TieuDe.ToLower().Contains(search.ToLower()));
            }
            var items = await query.OrderBy(o => o.STT).ToListAsync();
            return items;
        }
        public async Task<List<Por_VanBanPhapQuy>> GetVanBanAdmin()
        {
            var items = await (from x in _dbContext.Por_VanBanPhapQuys
                               join y in _dbContext.Por_LoaiVanBanPhapQuys on x.IDLoaiVanBanPhapQuy equals y.Id
                               select new Por_VanBanPhapQuy()
                               {
                                   Id = x.Id,
                                   STT = x.STT,
                                   IDLoaiVanBanPhapQuy = x.IDLoaiVanBanPhapQuy,
                                   TieuDe = x.TieuDe,
                                   NgayBanHanh = x.NgayBanHanh,
                                   CreatedBy = x.CreatedBy,
                                   CreatedDateTime = x.CreatedDateTime

                               }).ToListAsync();
            return items;

        }
        public async Task<List<Por_VanBanPhapQuy>> GetVanBanPortal()
        {
            var items = await (from x in _dbContext.Por_VanBanPhapQuys
                               join y in _dbContext.Por_LoaiVanBanPhapQuys on x.IDLoaiVanBanPhapQuy equals y.Id
                               select new Por_VanBanPhapQuy()
                             {
                                   Id = x.Id,
                                   STT = x.STT,
                                   TieuDe = x.TieuDe,
                                   SoHieuVanBan = x.SoHieuVanBan,
                                   NgayBanHanh = x.NgayBanHanh,
                                   UpdatedDateTime = x.UpdatedDateTime

                             } ).ToListAsync();
            return items;

        }
        
        public async Task AddLoaiVanBanAdmin(Por_LoaiVanBanPhapQuy lvb)
        {
            var checkLoai = await _dbContext.Por_LoaiVanBanPhapQuys.AnyAsync(x => x.Ten.Equals(lvb.Ten));
            if (checkLoai)
            {
                throw new Exception("Type is already existed");
            }
            
            lvb.CreatedBy = _userProvider.UserName;
            lvb.CreatedDateTime = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone); ;

            await _dbContext.Por_LoaiVanBanPhapQuys.AddAsync(lvb);
            await UnitOfWork.SaveAsync();
            
        }
        public async Task AddVanBanAdmin(Por_VanBanPhapQuy vb)
        {
            var checkvb = await _dbContext.Por_VanBanPhapQuys.AnyAsync(x => x.TieuDe.Equals(vb.TieuDe));
            if (checkvb)
            {
                throw new Exception("Name of VanBan is already existed");
            }

            await _dbContext.Por_VanBanPhapQuys.AddAsync(vb);
            await UnitOfWork.SaveAsync();
        }


        public async Task DeleteLoaiVanBanById(Guid Id)
        {
            var loaivb = await _dbContext.Por_LoaiVanBanPhapQuys.FirstOrDefaultAsync(x => x.Id == Id);
            if (loaivb == null)
            {
                throw new Exception(Sys_Const.Message.SERVICE_ORGAN_UNEXISTED);
            }
            _dbContext.Por_LoaiVanBanPhapQuys.Remove(loaivb);
            await UnitOfWork.SaveAsync();
        }

        public async Task DeleteVanBanById(Guid Id)
        {
            var vb = await _dbContext.Por_VanBanPhapQuys.FirstOrDefaultAsync(x => x.Id == Id);
            if (vb == null)
            {
                throw new Exception(Sys_Const.Message.SERVICE_ORGAN_UNEXISTED);
            }
            _dbContext.Por_VanBanPhapQuys.Remove(vb);
            await UnitOfWork.SaveAsync();
        }


    }
}
