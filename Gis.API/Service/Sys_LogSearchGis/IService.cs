using Gis.API.ViewModel.Portal;
using Gis.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gis.API.Model;
using System;
using Gis.Core.Models;
using Gis.API.ViewModel.Por_LogEmailSms;

namespace Gis.API.Service.Por_LogSearch
{
    public interface IService : IRepositoryBase<Por_LogSearchGis>
    {
        public Task<List<Por_LogSearchGis>> SearchDate(DateTimeOffset? toDate, DateTimeOffset? endDate);
        public Task<List<LogEmailSms>> SearchDateEmailSms(DateTimeOffset? toDate, DateTimeOffset? endDate);
    }
}
