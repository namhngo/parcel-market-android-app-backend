using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_PhuongXaThiTran
{
    public interface IService: IRepositoryBase<Model.Por_PhuongXaThiTran>
    {
        public List<Model.Por_PhuongXaThiTran> GetPhuongXaThiTran(Guid IDQH);
        public List<Model.Por_PhuongXaThiTran> GetPhuongXaThiTran(string MaQH);
        public List<Model.Por_PhuongXaThiTran> GetPhuongXaThiTranByMaPX(string MaPX);
    }
}
