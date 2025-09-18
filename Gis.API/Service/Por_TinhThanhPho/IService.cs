using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_TinhThanhPho
{
    public interface IService: IRepositoryBase<Model.Por_TinhThanhPho>
    {
        public List<Model.Por_TinhThanhPho> GetTinhThanhPho();
        public List<Model.Por_TinhThanhPho> GetTinhThanhPhoByMaTP(string MaTP);
    }
}
