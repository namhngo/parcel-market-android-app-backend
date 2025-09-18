using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Gis.API.Infrastructure;
using Gis.API.Model;
using Gis.Core.Constant;
using Gis.Core.Core;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gis.API.ViewModel.Portal;
using static Gis.API.Infrastructure.Enums;

namespace Gis.API.Service.Por_FileBuocQuyTrinh
{
    public class Service: RepositoryBase<Model.Por_FileBuocQuyTrinh>, Por_FileBuocQuyTrinh.IService
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
    }
}
