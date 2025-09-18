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
using Gis.Core.Helpers;
using k8s.KubeConfigModels;
using Gis.API.ViewModel.Por_LogEmailSms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Gis.API.Service.Por_LogSearch
{
    public class Service : RepositoryBase<Por_LogSearchGis>, IService
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

        public async Task<List<Por_LogSearchGis>> SearchDate(DateTimeOffset? toDate, DateTimeOffset? endDate)
        {
            var query = _dbContext.Por_LogSearchGiss.AsQueryable();
            if(toDate.HasValue)
            {
                query = query.Where(o => o.CreatedDateTime >= toDate);
            }
            if (endDate.HasValue)
            {
                query = query.Where(o => o.CreatedDateTime <= endDate);
            }
            return await query.ToListAsync();
        }
        public async Task<List<LogEmailSms>> SearchDateEmailSms(DateTimeOffset? toDate, DateTimeOffset? endDate)
        {
            var logs = new List<LogEmailSms>();
            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                string sql = "select * from public.por_smssend ps where 1=1 ";
                if (toDate.HasValue)
                {
                    toDate = toDate.Value.AddHours(Sys_Const.TimeZone);
                    sql += "and to_timestamp(time_create, 'dd/MM/yyyy HH24:MI:SS') >= to_timestamp('"+ toDate.Value.ToString("dd/MM/yyyy HH:mm:ss") + "', 'dd/MM/yyyy HH24:MI:SS') ";
                }
                if (endDate.HasValue)
                {
                    endDate = endDate.Value.AddHours(Sys_Const.TimeZone);
                    sql += "and to_timestamp(time_create, 'dd/MM/yyyy HH24:MI:SS') <= to_timestamp('"+ endDate.Value.ToString("dd/MM/yyyy HH:mm:ss") + "', 'dd/MM/yyyy HH24:MI:SS') ";
                }
                sql += "order by time_create desc ";
                command.CommandText = sql;
                _dbContext.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var item = new LogEmailSms();
                            item.Id = reader.GetInt32(0);
                            item.Mobile = reader.GetString(1);
                            //item.Content = reader.GetString(2);
                            item.Status = (reader.GetInt16(3) == 1 ? "Đã gửi" : "Chưa gửi");
                            item.Time_create = reader.GetString(4);
                            item.Time_send = reader.GetString(5);
                            item.Email = reader.GetString(6);
                            if(reader.GetString(7) == "HSDC")
                            {
                                item.Type = "Hồ sơ dịch vụ công";
                            }    
                            else if (reader.GetString(7) == "HSPA")
                            {
                                item.Type = "Hồ sơ phản ánh";
                            }
                            else if(reader.GetString(7) == "GuiVanChuyen")
                            {
                                item.Type = "Gửi vận chuyển";
                            }
                            else if (reader.GetString(7) == "ResetPass")
                            {
                                item.Type = "Gửi lấy lại mật khẩu";
                            }
                            //item.Contentemail = reader.GetString(8);
                            item.Statusmail = (reader.GetInt16(9) == 1 ? "Đã gửi" : "Chưa gửi");
                            logs.Add(item);
                        }
                    }
                }
            }
            return logs;
        }
    }
}
