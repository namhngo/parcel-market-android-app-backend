using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_CauHoiThuongGap
{
    public interface IService: IRepositoryBase<Model.Por_CauHoiThuongGap>
    {
        public Task<List<Model.Por_CauHoiThuongGap>> LayDSCauHoiThuongGap();
    }
}
