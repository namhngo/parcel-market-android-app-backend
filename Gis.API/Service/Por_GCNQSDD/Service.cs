using Microsoft.EntityFrameworkCore;
using Gis.API.Infrastructure;
using Gis.Core.Constant;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_GCNQSDD
{
    public class Service : RepositoryBase<Model.Por_GCNQSDD>, Por_GCNQSDD.IService
    {
        private readonly DomainDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public Service(DomainDbContext dbContext, IDateTimeProvider dateTimeProvider, IUserProvider userService) : base(dbContext, dateTimeProvider, userService)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userService;
        }
        public async Task<bool> IsDupicateAttributesAsync(Guid? Id, string SoTo, string SoThua)
        {
            bool result = false;
            //if (GuidHelpers.IsNullOrEmpty(Id))
            //{
            //    result = await _dbContext.Por_GCNQSDDs.Where(o => o.SoTo == SoTo && o.SoThua == SoThua).AnyAsync();
            //}
            //else
            //{
            //    var count = await _dbContext.Por_GCNQSDDs.Where(o => o.Id == Id && o.SoTo == SoTo && o.SoThua == SoThua).CountAsync();
            //    if (count <= 1)
            //    {
            //        result = false;
            //    }
            //    else
            //    {
            //        result = true;
            //    }
            //}
            return await Task.FromResult(result);
        }
        public async Task<List<Model.Por_GCNQSDD>> Search(string SoHieu, string CCCD, string SoThua, string SoTo, string TenPhuongXa, string nguoiSuDung)
        {
            var query = _dbContext.Por_GCNQSDDs.AsQueryable();
            if (!string.IsNullOrEmpty(SoHieu))
            {
                query = query.Where(o => o.SoHieu == SoHieu);
            }
            if (!string.IsNullOrEmpty(CCCD))
            {
                query = query.Where(o => o.CCCD == CCCD);
            }
            if (!string.IsNullOrEmpty(SoTo))
            {
                query = query.Where(o => o.SoTo == SoTo);
            }
            if (!string.IsNullOrEmpty(SoThua))
            {
                query = query.Where(o => o.SoThua == SoThua);
            }
            if (!string.IsNullOrEmpty(TenPhuongXa))
            {
                query = query.Where(o => o.TenPhuongXa == TenPhuongXa);
            }
            if (!string.IsNullOrEmpty(nguoiSuDung))
            {
                query = query.Where(o => o.NguoiSuDung == nguoiSuDung);
            }
            var items = await query.ToListAsync();
            return items;
        }
        public async Task<List<Model.Por_GCNQSDD>> SearchMaPx(string SoHieu, string CCCD, string SoThua, string SoTo, string MaPx)
        {
            var query = _dbContext.Por_GCNQSDDs.AsQueryable();
            if (!string.IsNullOrEmpty(SoHieu))
            {
                query = query.Where(o => o.SoHieu == SoHieu);
            }
            if (!string.IsNullOrEmpty(CCCD))
            {
                query = query.Where(o => o.CCCD == CCCD);
            }
            if (!string.IsNullOrEmpty(SoTo))
            {
                query = query.Where(o => o.SoTo == SoTo);
            }
            if (!string.IsNullOrEmpty(SoThua))
            {
                query = query.Where(o => o.SoThua == SoThua);
            }
            if (!string.IsNullOrEmpty(MaPx))
            {
                query = query.Where(o => o.MaPX == MaPx);
            }
           
            var items = await query.ToListAsync();
            return items;
        }
    }
}
