using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_GPXD
{
    public interface IService: IRepositoryBase<Model.Por_GPXD>
    {
        Task<List<Model.Por_GPXD>> Search(string SoThua, string SoTo, string TenXa);
        Task<bool> IsDupicateAttributesAsync(Guid? Id, string SoTo, string SoThua);
        Task<List<Model.Por_GPXD>> SearchMaPX(string soThua, string soTo, string MaPX);
    }
}
