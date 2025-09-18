using Microsoft.EntityFrameworkCore;
using Gis.API.Infrastructure;
using Gis.Core.Constant;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Gis.API.ViewModel.Por_GisSoNha;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace Gis.API.Service.Por_GisSoNha
{
    public class Service : RepositoryBase<Model.Por_GisSoNha>, Por_GisSoNha.IService
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
            if (GuidHelpers.IsNullOrEmpty(Id))
            {
                result = await _dbContext.Por_GisSoNhas.Where(o => o.SoTo == SoTo && o.SoThua == SoThua).AnyAsync();
            }
            else
            {
                var count = await _dbContext.Por_GisSoNhas.Where(o => o.Id == Id && o.SoTo == SoTo && o.SoThua == SoThua).CountAsync();
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
        public async Task<List<Model.Por_GisSoNha>> Search(string SoThua, string SoTo, string TenXa)
        {
            var query = _dbContext.Por_GisSoNhas.AsQueryable();
            if (!string.IsNullOrEmpty(SoTo))
            {
                query = query.Where(o => o.SoTo == SoTo);
            }
            if (!string.IsNullOrEmpty(SoThua))
            {
                query = query.Where(o => o.SoThua == SoThua);
            }
            if (!string.IsNullOrEmpty(TenXa))
            {
                query = query.Where(o => o.TenPhuongXa == TenXa);
            }
            var items = await query.ToListAsync();
            return items;
        }
        public async Task<List<Model.Por_GisSoNha>> SearchMaPX(string SoThua, string SoTo, string MaPX)
        {
            var query = _dbContext.Por_GisSoNhas.AsQueryable();
            if (!string.IsNullOrEmpty(SoTo))
            {
                query = query.Where(o => o.SoTo == SoTo);
            }
            if (!string.IsNullOrEmpty(SoThua))
            {
                query = query.Where(o => o.SoThua == SoThua);
            }
            if (!string.IsNullOrEmpty(MaPX))
            {
                query = query.Where(o => o.MaPX == MaPX);
            }
            var items = await query.ToListAsync();
            return items;
        }
        public async Task<List<Model.Por_GisSoNha>> SearchSoNha(TimKiemSoNha model)
        {
            var query = _dbContext.Por_GisSoNhas.AsQueryable();
            if (!string.IsNullOrEmpty(model.SoNha))
            {
                query = query.Where(o => o.SoNha == model.SoNha);
            }
            if (!string.IsNullOrEmpty(model.TenDuong))
            {
                query = query.Where(o => o.TenDuong.ToLower().Contains(model.TenDuong.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.MaPX))
            {
                query = query.Where(o => o.MaPX == model.MaPX);
            }
            var items = await query.ToListAsync();
            return items;
        }
    }
}
