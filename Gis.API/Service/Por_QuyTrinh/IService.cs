using Gis.API.ViewModel.PhanAnh;
using Gis.API.ViewModel.QuyTrinh;
using Gis.Core.Interfaces;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_QuyTrinh
{
    public interface IService: IRepositoryBase<Model.Por_QuyTrinh>
    {                
        Task<bool> IsDupicateAttributesAsync(Guid? Id, string TenThuTuc);
        Task<List<DsQuyTrinh>> DsQuyTrinh();
        Task<List<DsQuyTrinh>> DsQuyTrinhHoatDong();       
        Task<List<DsQuyTrinh>> DsQuyTrinhTheoLinhVuc(Guid Id);
        Task XoaQuyTrinh(Guid Id);
    }
}
