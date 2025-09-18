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

namespace Gis.API.Service.Por_QuanHuyen
{
    public class Service : RepositoryBase<Model.Por_QuanHuyen>, Por_QuanHuyen.IService
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
        public List<Model.Por_QuanHuyen> GetQuanHuyenByMaQH(string MaQH)
        {
            List<string> columnNames = ReflectionUtil.GetColumnNameAttr<Model.Por_QuanHuyen>("category");
            if (columnNames.Count == 0)
                return null;
            string strColumns = ListHelpers.ConcatStrings(columnNames);
            var result = _dbContext.Por_QuanHuyens.Where(o => o.Ma == MaQH).Select(LinQHelpers.DynamicSelectGenerator<Model.Por_QuanHuyen>(strColumns)).OrderBy(o => o.Ten).ToList();
            return result;
        }
        public List<Model.Por_QuanHuyen> GetQuanHuyen(string MaTP)
        {
            List<string> columnNames = ReflectionUtil.GetColumnNameAttr<Model.Por_QuanHuyen>("category");
            if (columnNames.Count == 0)
                return null;
            string strColumns = ListHelpers.ConcatStrings(columnNames);
            var result = _dbContext.Set<Model.Por_QuanHuyen>().Where(o => o.MaTP == MaTP)
                                   .Select(LinQHelpers.DynamicSelectGenerator<Model.Por_QuanHuyen>(strColumns)).OrderBy(o => o.Ten).ToList();
            return result;
        }
        public List<Model.Por_QuanHuyen> GetQuanHuyen(Guid IDTP)
        {
            List<string> columnNames = ReflectionUtil.GetColumnNameAttr<Model.Por_QuanHuyen>("category");
            if (columnNames.Count == 0)
                return null;
            string strColumns = ListHelpers.ConcatStrings(columnNames);
            string MaTP = _dbContext.Por_TinhThanhPhos.First(o => o.Id == IDTP).Ma;
            var result = _dbContext.Set<Model.Por_QuanHuyen>().Where(o => o.MaTP == MaTP)
                                   .Select(LinQHelpers.DynamicSelectGenerator<Model.Por_QuanHuyen>(strColumns)).OrderBy(o => o.Ten).ToList();
            return result;
        }
    }
}
