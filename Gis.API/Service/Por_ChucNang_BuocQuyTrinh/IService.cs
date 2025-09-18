using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_ChucNang_BuocQuyTrinh
{
    public interface IService: IRepositoryBase<Model.Por_ChucNang_BuocQuyTrinh>
    {
        Task<bool> IsDupicateAttributesAsync(Guid? Id, string Name);
    }
}
