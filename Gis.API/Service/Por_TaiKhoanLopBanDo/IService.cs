using Gis.Core.Interfaces;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_TaiKhoanLopBanDo
{
    public interface IService: IRepositoryBase<Model.Por_TaiKhoanLopBanDo>
    {
        Task DeleteByIdLopBanDo(Guid Id);
    }
}
