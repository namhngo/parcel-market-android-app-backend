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

namespace Gis.API.Service.Por_PhuongXaThiTran
{
    public class Service : RepositoryBase<Model.Por_PhuongXaThiTran>, Por_PhuongXaThiTran.IService
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
        public List<Model.Por_PhuongXaThiTran> GetPhuongXaThiTranByMaPX(string MaPX)
        {
            List<string> columnNames = ReflectionUtil.GetColumnNameAttr<Model.Por_PhuongXaThiTran>("category");
            if (columnNames.Count == 0)
                return null;
            string strColumns = ListHelpers.ConcatStrings(columnNames);
            var result = _dbContext.Por_PhuongXaThiTrans.Where(o => o.Ma == MaPX).Select(LinQHelpers.DynamicSelectGenerator<Model.Por_PhuongXaThiTran>(strColumns)).OrderBy(o => o.Ten).ToList();
            return result;
        }
        public List<Model.Por_PhuongXaThiTran> GetPhuongXaThiTran(string MaQH)
        {
            List<string> columnNames = ReflectionUtil.GetColumnNameAttr<Model.Por_QuanHuyen>("category");
            if (columnNames.Count == 0)
                return null;
            string strColumns = ListHelpers.ConcatStrings(columnNames);
            var result = _dbContext.Set<Model.Por_PhuongXaThiTran>().Where(o => o.MaQH == MaQH).Select(LinQHelpers.DynamicSelectGenerator<Model.Por_PhuongXaThiTran>(strColumns)).OrderBy(o => o.Ten).ToList();
            return result;
        }
        public List<Model.Por_PhuongXaThiTran> GetPhuongXaThiTran(Guid IDQH)
        {
            List<string> columnNames = ReflectionUtil.GetColumnNameAttr<Model.Por_QuanHuyen>("category");
            if (columnNames.Count == 0)
                return null;
            string strColumns = ListHelpers.ConcatStrings(columnNames);
            string MaQH = _dbContext.Por_QuanHuyens.First(o => o.Id == IDQH).Ma;
            var result = _dbContext.Set<Model.Por_PhuongXaThiTran>().Where(o => o.MaQH == MaQH).Select(LinQHelpers.DynamicSelectGenerator<Model.Por_PhuongXaThiTran>(strColumns)).OrderBy(o => o.Ten).ToList();
            return result;
        }
    }
}
