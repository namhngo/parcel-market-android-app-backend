using Microsoft.EntityFrameworkCore;
using Gis.API.Infrastructure;
using Gis.Core.Constant;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gis.API.ViewModel.QuyTrinh;

namespace Gis.API.Service.Por_QuyTrinh
{
    public class Service:RepositoryBase<Model.Por_QuyTrinh>, Por_QuyTrinh.IService
    {
        private readonly DomainDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public Service(DomainDbContext dbContext, IDateTimeProvider dateTimeProvider, IUserProvider userService):base(dbContext, dateTimeProvider, userService)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userService;
        }
        public async Task<List<DsQuyTrinh>> DsQuyTrinhHoatDong()
        {
            return await _dbContext.Por_QuyTrinhs
                .Where(o => o.CongKhai == true)
                .Select(o => new DsQuyTrinh() { Id = o.Id, Ten = o.TenThuTuc }).ToListAsync();
        }
        public async Task<List<DsQuyTrinh>> DsQuyTrinhTheoLinhVuc(Guid Id)
        {
            return await _dbContext.Por_QuyTrinhs
                .Where(o => o.IDLinhVuc == Id && o.CongKhai == true)
                .Select(o => new DsQuyTrinh() { Id = o.Id, Ten = o.TenThuTuc }).ToListAsync();
        }    
        public async Task<List<DsQuyTrinh>> DsQuyTrinh()
        {
            var items = await (from a in _dbContext.Por_QuyTrinhs
                         join b in _dbContext.Por_LinhVucs on a.IDLinhVuc equals b.Id
                         join c in _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.MucDo) on a.IDMucDo equals c.Id
                         select new DsQuyTrinh()
                         {
                             Id = a.Id,
                             Ma = a.MaThuTuc,
                             Ten = a.TenThuTuc,
                             TenLinhVuc = b.Ten,
                             TenMucDo = c.Name,
                             ThoiGianThucHien = a.ThoiGianThucHien,
                             GiaTien = a.GiaTien
                         }).ToListAsync();
            return items;
        }
        public async Task<bool> IsDupicateAttributesAsync(Guid? Id, string TenThuTuc)
        {
            bool result = false;
            if (string.IsNullOrEmpty(TenThuTuc))
            {
                throw new Exception(Sys_Const.Message.SERVICE_NAME_NOT_EMPTY);
            }
            if (GuidHelpers.IsNullOrEmpty(Id))
            {
                result = await _dbContext.Por_QuyTrinhs.Where(o => o.TenThuTuc == TenThuTuc).AnyAsync();
            }
            else
            {
                var count = await _dbContext.Por_QuyTrinhs.Where(o => o.Id == Id && o.TenThuTuc == TenThuTuc).CountAsync();
                if (count <= 1)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            return await Task.FromResult(result);
        }
        public async Task XoaQuyTrinh(Guid Id)
        {
            var quytrinh = await _dbContext.Por_QuyTrinhs.Where(o => o.Id == Id).FirstAsync();
            _dbContext.Por_QuyTrinhs.Remove(quytrinh);
            var buocQuyTrinhs = await _dbContext.Por_BuocQuyTrinhs.Where(o => o.IDQuyTrinh == Id).ToArrayAsync();
            _dbContext.Por_BuocQuyTrinhs.RemoveRange(buocQuyTrinhs);
            var fileMauThanhPhanHStrongQTs = await _dbContext.Por_FileMauThanhPhanHStrongQTs.Where(o => o.IDQuyTrinh == Id).ToArrayAsync();
            _dbContext.Por_FileMauThanhPhanHStrongQTs.RemoveRange(fileMauThanhPhanHStrongQTs);
            await UnitOfWork.SaveAsync();
        }    
    }
}
