using Microsoft.EntityFrameworkCore;
using Gis.API.Infrastructure;
using Gis.Core.Constant;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_CauHoiThuongGap
{
    public class Service:RepositoryBase<Model.Por_CauHoiThuongGap>, Por_CauHoiThuongGap.IService
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
        public async Task<List<Model.Por_CauHoiThuongGap>> LayDSCauHoiThuongGap()
        {
            return await _dbContext.Por_CauHoiThuongGaps.Where(o => o.TrangThai == true).OrderByDescending(o => o.STT).ToListAsync();
        }
    }
}
