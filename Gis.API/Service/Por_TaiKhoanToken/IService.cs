using Gis.Core.Interfaces;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_TaiKhoanToken
{
    public interface IService: IRepositoryBase<Model.Por_TaiKhoanToken>
    {
        Task SaveByUserNameAsync(string UserName, Model.Por_TaiKhoanToken authToken);
        Task<Model.Por_TaiKhoanToken> GetByUserNameAsync(string UserName);
    }
}
