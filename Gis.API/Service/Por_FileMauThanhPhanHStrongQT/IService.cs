using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_FileMauThanhPhanHStrongQT
{
    public interface IService: IRepositoryBase<Model.Por_FileMauThanhPhanHStrongQT>
    {
        public Task<List<Model.Por_FileMauThanhPhanHStrongQT>> DsFileMauThanhPhanHSTheoQuyTrinh(Guid Id);
        public Task XoaFile(List<Model.Por_FileMauThanhPhanHStrongQT> itemsEdit, List<Model.Por_FileMauThanhPhanHStrongQT> itemsSaveNew);
    }
}
