using Microsoft.EntityFrameworkCore;
using Gis.API.Infrastructure;
using Gis.Core.Constant;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Sys_EmailSms
{
    public class Service:RepositoryBase<Model.Sys_EmailSms>, Sys_EmailSms.IService
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
        public async Task<bool> IsDupicateAttributesAsync(Guid? Id, string EmailFrom)
        {
            bool result = false;
            if (string.IsNullOrEmpty(EmailFrom))
            {
                throw new Exception(Sys_Const.Message.SERVICE_CODE_NOT_EMPTY);
            }
            if (GuidHelpers.IsNullOrEmpty(Id))
            {
                result = await _dbContext.Sys_EmailSmss.Where(o => o.EmailFrom == EmailFrom).AnyAsync();
            }
            else
            {
                var count = await _dbContext.Sys_EmailSmss.Where(o => o.Id == Id && o.EmailFrom == EmailFrom).CountAsync();
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
        public async Task DeleteById(Guid Id)
        {
            var user = await _dbContext.Sys_EmailSmss.FirstOrDefaultAsync(o => o.Id == Id);
            if (user != null)
            {
                throw new Exception(Sys_Const.Message.SERVICE_ROLE_EXIST_USER);
            }
            var role = await _dbContext.Sys_EmailSmss.FirstOrDefaultAsync(o => o.Id == Id);
            if (role == null)
            {
                throw new Exception(Sys_Const.Message.SERVICE_ROLE_UNEXISTED);
            }
            _dbContext.Sys_EmailSmss.Remove(role);
            await UnitOfWork.SaveAsync();
        }
    }
}
