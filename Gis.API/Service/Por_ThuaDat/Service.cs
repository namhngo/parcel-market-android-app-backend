using Microsoft.EntityFrameworkCore;
using Gis.API.Infrastructure;
using Gis.Core.Constant;
using Gis.Core.Core;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_ThuaDat
{
    public class Service : RepositoryBase<Model.Por_ThuatDat>, Por_ThuaDat.IService
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
        public async Task<Model.Por_ThuatDat> GetByHoSoId(Guid hosoId)
        {
            var item = await _dbContext.Por_ThuatDats.Where(o => o.IDHoSoNguoiNop == hosoId).FirstOrDefaultAsync();
            return item;
        }
    }
}
