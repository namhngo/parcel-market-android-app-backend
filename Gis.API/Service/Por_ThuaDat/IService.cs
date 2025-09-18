using Gis.Core.Interfaces;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_ThuaDat
{
    public interface IService: IRepositoryBase<Model.Por_ThuatDat>
    {
        Task<Model.Por_ThuatDat> GetByHoSoId(Guid hosoId);
    }
}
