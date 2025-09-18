using Gis.Core.Interfaces;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_LinhVuc
{
    public interface IService: IRepositoryBase<Model.Por_LinhVuc>
    {
        Task<List<ViewModel.Por_LinhVuc.LinhVucTree>> GetTreeAsync();
        Task<List<ViewModel.Por_LinhVuc.LinhVucTree>> GetTreeListAsync();        
        Task<List<Model.Por_LinhVuc>> GetByParentIdAsync(Guid ParentId);
        Task<bool> IsDupicateAttributesAsync(Guid? Id, string Code, Guid ParentId);
        Task DeleteById(Guid Id);
    }
}
