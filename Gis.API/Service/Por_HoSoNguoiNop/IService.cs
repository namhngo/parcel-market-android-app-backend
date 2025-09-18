using Gis.API.ViewModel.HoSoQuyTrinh;
using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_HoSoNguoiNop
{
    public interface IService: IRepositoryBase<Model.Por_HoSoNguoiNop>
    {
        Task<LuuHoSoQuyTrinh> LuuHoSoQuyTrinh(LuuHoSoQuyTrinh model);
        Task<ChiTietHoSoQuyTrinh> LayChiTietHoSoQuyTrinh(Guid id);
        Task<List<TraCuuHoSo>> TraCuuHoSo(string SoHoSo, string UserName);
        Task TiepNhanHoSo(Guid Id, string UserName);
        Task GuiHoSo(Guid Id, string UserName);
        Task ThanhToanHoSo(Guid Id, string UserName);
        Task<Guid> TraKetQua(Guid Id, string NoiDung, string UserName);
        Task<Guid> ChuyenXuLy(Guid Id, string NoiDung, string UserName);
        Task<bool> KiemTraBuocCuoiCung(Guid Id);
        Task<Guid> TamDung(Guid Id, string NoiDung, Guid? TieuChi, string UserName);
        Task<Guid> TiepTuc(Guid Id, string UserName);
        Task<Guid> TraNguocLai(Guid Id, string NoiDung, string UserName);
        Task HuyQuyTrinh(Guid Id, string UserName, string NoiDungHuy);
        Task<List<DsBuocQuyTrinhHoSo>> DsBuocQuyTrinhHoSo(Guid Id);
        Task XoaHoSo(Guid Id);
        Task<List<HoSoHuy>> HoSoHuy(string SoHoSo, string UserName);
        Task<List<HoSoHoanThanh>> HoSoHoanThanh(string SoHoSo);        
        Task<List<HoSoXuLy>> HoSoXuLy(Guid UserId, string UserName, string SoHoSo);
        Task<List<HoSoTamDung>> HoSoTamDung(string SoHoSo);
        Task<List<HoSoTraKetQua>> HoSoTraKetQua(string SoHoSo, string UserName);
    }
}
