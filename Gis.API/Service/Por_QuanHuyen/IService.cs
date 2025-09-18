using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_QuanHuyen
{
    public interface IService: IRepositoryBase<Model.Por_QuanHuyen>
    {
        public List<Model.Por_QuanHuyen> GetQuanHuyen(Guid IDTP);
        public List<Model.Por_QuanHuyen> GetQuanHuyen(string MaTP);
        public List<Model.Por_QuanHuyen> GetQuanHuyenByMaQH(string MaQH);
    }
}
