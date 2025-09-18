using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_NCCDMDSDD
{
    public interface IService: IRepositoryBase<Model.Por_NCCDMDSDD>
    {
        Task<List<Model.Por_NCCDMDSDD>> SearchMaPX(string SoThua, string SoTo, string MaPx);
        Task<List<Model.Por_NCCDMDSDD>> Search(string SoThua, string SoTo, string TenPhuongXa);
        Task<bool> IsDupicateAttributesAsync(Guid? Id, string SoTo, string SoThua);
    }
}
