using Microsoft.EntityFrameworkCore;
using Gis.API.Infrastructure;
using Gis.Core.Constant;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_ChucNang_BuocQuyTrinh
{
    public class Service:RepositoryBase<Model.Por_ChucNang_BuocQuyTrinh>, Por_ChucNang_BuocQuyTrinh.IService
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
        public async Task<bool> IsDupicateAttributesAsync(Guid? Id, string Name)
        {
            bool result = false;
            if (GuidHelpers.IsNullOrEmpty(Id))
            {
                result = await _dbContext.Por_ChucNang_BuocQuyTrinhs.Where(o => o.Ten == Name).AnyAsync();
            }
            else
            {
                var count = await _dbContext.Por_ChucNang_BuocQuyTrinhs.Where(o => o.Id == Id && o.Ten == Name).CountAsync();
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
    }
}
