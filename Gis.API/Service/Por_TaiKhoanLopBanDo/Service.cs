using Microsoft.EntityFrameworkCore;
using Gis.API.Infrastructure;
using Gis.Core.Constant;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Gis.API.Service.Por_TaiKhoanLopBanDo
{
    public class Service : RepositoryBase<Model.Por_TaiKhoanLopBanDo>, Por_TaiKhoanLopBanDo.IService
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
        public async Task DeleteByIdLopBanDo(Guid Id)
        {
            var DsTaiKhoanLopBanDo = await _dbContext.Por_TaiKhoanLopBanDos.Where(o => o.IdLopBanDo == Id).ToArrayAsync();
            _dbContext.Por_TaiKhoanLopBanDos.RemoveRange(DsTaiKhoanLopBanDo);
            await UnitOfWork.SaveAsync();
        }
    }
}
