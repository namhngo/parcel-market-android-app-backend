using Microsoft.EntityFrameworkCore;
using Gis.API.Infrastructure;
using Gis.Core.Constant;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gis.Core.Core;
using System.Linq.Dynamic.Core;

namespace Gis.API.Service.Por_TinhThanhPho
{
    public class Service : RepositoryBase<Model.Por_TinhThanhPho>, Por_TinhThanhPho.IService
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
        public List<Model.Por_TinhThanhPho> GetTinhThanhPhoByMaTP(string MaTP)
        {
            List<string> columnNames = ReflectionUtil.GetColumnNameAttr<Model.Por_TinhThanhPho>("category");
            if (columnNames.Count == 0)
                return null;
            string strColumns = ListHelpers.ConcatStrings(columnNames);
            var result = _dbContext.Por_TinhThanhPhos.Where(o => o.Ma == MaTP).Select(LinQHelpers.DynamicSelectGenerator<Model.Por_TinhThanhPho>(strColumns)).OrderBy(o => o.Ten).ToList();
            return result;
        }
        public List<Model.Por_TinhThanhPho> GetTinhThanhPho()
        {
            List<string> columnNames = ReflectionUtil.GetColumnNameAttr<Model.Por_TinhThanhPho>("category");
            if (columnNames.Count == 0)
                return null;
            string strColumns = ListHelpers.ConcatStrings(columnNames);
            var result = _dbContext.Por_TinhThanhPhos.Select(LinQHelpers.DynamicSelectGenerator<Model.Por_TinhThanhPho>(strColumns)).OrderBy(o => o.Ten).ToList();
            return result;
        }
    }
}
