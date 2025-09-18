using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_TemplatePhieuBienNhan
{
    public interface IService: IRepositoryBase<Model.Por_TemplatePhieuBienNhan>
    {
        Task<bool> IsDupicateAttributesAsync(Guid? Id, string Ma);
    }
}
