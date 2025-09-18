using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_GCNQSDD
{
    public interface IService: IRepositoryBase<Model.Por_GCNQSDD>
    {
        Task<List<Model.Por_GCNQSDD>> Search(string SoHieu, string CCCD, string SoThua, string SoTo, string TenPhuongXa, string NguoiSuDung);
        Task<bool> IsDupicateAttributesAsync(Guid? Id, string SoTo, string SoThua);
        Task<List<Model.Por_GCNQSDD>> SearchMaPx(string soHieu, string cCCD, string soThua, string soTo, string maPx);
    }
}
