using Gis.API.ViewModel.Por_GisSoNha;
using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_GisSoNha
{
    public interface IService: IRepositoryBase<Model.Por_GisSoNha>
    {
        Task<List<Model.Por_GisSoNha>> Search(string SoThua, string SoTo, string TenXa);
        Task<bool> IsDupicateAttributesAsync(Guid? Id, string SoTo, string SoThua);
        Task<List<Model.Por_GisSoNha>> SearchMaPX(string soThua, string soTo, string MaPX);
        Task<List<Model.Por_GisSoNha>> SearchSoNha(TimKiemSoNha model);
        
    }
}
