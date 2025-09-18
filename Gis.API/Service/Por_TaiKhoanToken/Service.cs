using Microsoft.EntityFrameworkCore;
using Gis.API.Infrastructure;
using Gis.Core.Constant;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_TaiKhoanToken
{
    public class Service : RepositoryBase<Model.Por_TaiKhoanToken>, Por_TaiKhoanToken.IService
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
        public async Task SaveByUserNameAsync(string UserName, Model.Por_TaiKhoanToken authToken)
        {
            var token = _dbContext.Por_TaiKhoanTokens.Where(o => o.TenDangNhap == UserName).FirstOrDefault();
            if (token == null)
            {
                authToken.TenDangNhap = UserName;
                authToken.CreatedBy = UserName;
                authToken.CreatedDateTime = _dateTimeProvider.OffsetNow;
                await _dbContext.Por_TaiKhoanTokens.AddAsync(authToken);
            }
            else
            {
                ObjectHelpers.Mapping<Model.Por_TaiKhoanToken, Model.Por_TaiKhoanToken>(authToken, token, new string[] { "id" });
                token.TenDangNhap = UserName;
                token.UpdatedBy = UserName;
                token.UpdatedDateTime = _dateTimeProvider.OffsetNow;
                //_dbContext.Por_TaiKhoanToken.Update(token);
                _dbContext.Entry(token).CurrentValues.SetValues(token);
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Model.Por_TaiKhoanToken> GetByUserNameAsync(string UserName)
        {
            return await _dbContext.Por_TaiKhoanTokens.Where(o => o.TenDangNhap == UserName).FirstOrDefaultAsync();
        }
    }
}
