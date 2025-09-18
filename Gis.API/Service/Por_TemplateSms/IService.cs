using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_TemplateSms
{
    public interface IService: IRepositoryBase<Model.Por_TemplateSms>
    {
        Task<bool> IsDupicateAttributesAsync(Guid? Id, string Ma);
    }
}
