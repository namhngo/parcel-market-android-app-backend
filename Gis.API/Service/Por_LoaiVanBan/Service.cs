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

namespace Gis.API.Service.Por_LoaiVanBan
{
    public class Service : RepositoryBase<Por_LoaiVanBanPhapQuy>, IService
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
    }
}
