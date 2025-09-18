using Gis.API.ViewModel.Portal;
using Gis.Core.Interfaces;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_HoSo
{
    public interface IService
    {
        public Task<ViewModel.HoSoQuyTrinh.InPhieuBienNhan> InPhieuBienNhan(Guid Id, string UserName);
        public Task<ViewModel.HoSoQuyTrinh.InPhieuTiepNhan> InPhieuTiepNhan(Guid Id);
        public Task<List<LoaiHoSo>> LayDsLoaiHoSo();
        public Task<ViewModel.HoSoQuyTrinh.HoSoDashboard> HoSoDashboard();

        public Task<List<ViewModel.BaoCao.HoSoTheoTungLoai>> BaoCaoHoSoTheoTungLoai(ViewModel.BaoCao.SearchHoSoTheoTungLoai search, int? totalLimitItems);
        public Task<List<ViewModel.BaoCao.ThuaDatDuocTimKiem>> BaoCaoThuaDatDuocTimKiem(ViewModel.BaoCao.SearchThuaDatDuocTimKiem search, int? totalLimitItems);
        public Task<List<ViewModel.BaoCao.TiepNhanHoSo>> BaoCaoTiepNhanHoSo(ViewModel.BaoCao.SearchTiepNhanHoSo search, int? totalLimitItems);

        public Task<List<Model.Sys_Category>> LayDsMucDichSuDung();
        public Task<List<Model.Sys_Category>> LayDsLoaiPhanAnh();        
        public Task<List<Model.Por_FileMauThanhPhanHStrongQT>> LayDsFileMauThanhPhanHS(Guid IdQuyTrinh);
        public Task<List<Model.Sys_Category>> LayDsHinhThucNhanKetQua();
        public Task<List<Model.Sys_Category>> LayDsHinhThucThanhToan();
        public Task<List<Model.Sys_Category>> LayDsTieuChiTamDung();
        
        public Task<ViewModel.HoSoQuyTrinh.HoSoQuyTrinh> NopHoSo(ViewModel.HoSoQuyTrinh.HoSoQuyTrinh item);
        public Task<ViewModel.HoSoQuyTrinh.SuaHoSoQuyTrinh> SuaHoSo(ViewModel.HoSoQuyTrinh.SuaHoSoQuyTrinh item);
        
        public Task<ViewModel.HoSoQuyTrinh.HoSoQuyTrinh> SuaHoSo(ViewModel.HoSoQuyTrinh.HoSoQuyTrinh item);
        public Task XoaFileNop(Guid hosoId, string idsFileNop);
        public Task XoaFileDinhKem(Guid hosoId, string idsFileNop);
        public Task<List<ViewModel.HoSoQuyTrinh.HoSo>> GetDSHoSo(string search, string userName);
        public Task<ViewModel.HoSoQuyTrinh.HoSoQuyTrinh> XemChiTietHoSo(Guid Id);
    }
}
