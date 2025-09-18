using Gis.API.ViewModel.HoSoPaQuyTrinh;
using Gis.API.ViewModel.HoSoQuyTrinh;
using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_HoSoNguoiNopPA
{
    public interface IService: IRepositoryBase<Model.Por_GopYPhanAnh>
    {
        Task<LuuHoSoPAQuyTrinh> LuuHoSoQuyTrinh(LuuHoSoPAQuyTrinh model);
        Task<ChiTietHoSoPAQuyTrinh> LayChiTietHoSoQuyTrinh(Guid id);
        Task<List<TraCuuHoSoPA>> TraCuuHoSo(string SoHoSo, string UserName);
        Task TiepNhanHoSo(Guid Id, string UserName);
        Task<Guid> TraKetQua(Guid Id, string NoiDung, string UserName);
        Task<Guid> ChuyenXuLy(Guid Id, string NoiDung, string UserName);
        Task<bool> KiemTraBuocCuoiCung(Guid Id);
        Task<Guid> TraNguocLai(Guid Id, string NoiDung, string UserName);        
        Task HuyQuyTrinh(Guid Id, string UserName, string NoiDungHuy);
        Task<List<DsBuocQuyTrinhHoSo>> DsBuocQuyTrinhHoSo(Guid Id);
        Task XoaHoSo(Guid Id);
        Task<List<HoSoPAHuy>> HoSoHuy(string SoHoSo, string UserName);
        Task<List<HoSoPAHoanThanh>> HoSoHoanThanh(string SoHoSo);        
        Task<List<HoSoPAXuLy>> HoSoXuLy(Guid UserId, string UserName, string SoHoSo);
        Task<List<HoSoPATamDung>> HoSoTamDung(string SoHoSo);
        Task<List<HoSoPATraKetQua>> HoSoTraKetQua(string SoHoSo, string UserName);
    }
}
