using Gis.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service
{
    public interface IServiceWrapper
    {
        Por_TinhThanhPho.IService Por_TinhThanhPho { get; }
        Por_QuanHuyen.IService Por_QuanHuyen { get; }
        Por_PhuongXaThiTran.IService Por_PhuongXaThiTran { get; }
        Sys_AuthToken.IService Sys_AuthToken { get; }
        Sys_File.IService Sys_File { get; }
        Sys_User.IService Sys_User { get; }
        Sys_Category.IService Sys_Category { get; }
        Sys_Organization.IService Sys_Organization { get; }
        Sys_Role.IService Sys_Role { get; }
        Sys_Config.IService Sys_Config { get; }
        Sys_Permission.IService Sys_Permission { get; }
        Sys_Resource.IService Sys_Resource { get; }
        Sys_Notification.IService Sys_Notification { get; }
        Por_TaiKhoan.IService Por_TaiKhoan { get; }
        Por_TaiKhoanToken.IService Por_TaiKhoanToken { get; }
        Por_QuyTrinh.IService Por_QuyTrinh { get; }
        Por_LinhVuc.IService Por_LinhVuc { get; }
        Por_HoSo.IService Por_HoSo { get; }
        Por_BuocQuyTrinh.IService Por_BuocQuyTrinh { get; }
        Por_FileMauThanhPhanHStrongQT.IService Por_FileMauThanhPhanHStrongQT { get; }
        Por_FileHoSoNguoiNop.IService Por_FileHoSoNguoiNop { get; }
        Por_ChucNang_BuocQuyTrinh.IService Por_ChucNang_BuocQuyTrinh { get; }
        Por_VanBan.IService Por_VanBan { get; }
        Por_PhanAnh.IService Por_PhanAnh { get; }
        Por_HoSoNguoiNop.IService Por_HoSoNguoiNop { get; }
        Por_HoSoNguoiNopPA.IService Por_HoSoNguoiNopPA { get; }
        Por_HoSo_QuyTrinh.IService Por_HoSo_QuyTrinh { get; }
        Por_CauHoiThuongGap.IService Por_CauHoiThuongGap { get; }
        Por_LoaiVanBan.IService Por_LoaiVanBan { get; }
        Por_ThuaDat.IService Por_ThuaDat { get; }
        Por_FileBuocQuyTrinh.IService Por_FileBuocQuyTrinh { get; }
        Por_BinhLuanGopYPhanAnh.IService Por_BinhLuanGopYPhanAnh { get;}
        Por_BinhLuanThich.IService Por_BinhLuanThich { get; }
        Por_BinhLuanKhongThich.IService Por_BinhLuanKhongThich { get; }
        Por_FileHoSo.IService Por_FileHoSo { get; }
        Por_NCCDMDSDD.IService Por_NCCDMDSDD { get; }
        Por_GPXD.IService Por_GPXD { get; }
        Por_GisSoNha.IService Por_GisSoNha { get; }
        Por_GCNQSDD.IService Por_GCNQSDD { get; }
        Por_TemplateSms.IService Por_TemplateSms { get; }
        Por_TemplateEmail.IService Por_TemplateEmail { get; }
        Por_TemplatePhieuBienNhan.IService Por_TemplatePhieuBienNhan { get; }
        Por_LogSearch.IService Por_LogSearch { get; }
        Por_TaiKhoanLopBanDo.IService Por_TaiKhoanLopBanDo { get; }
        Sys_EmailSms.IService Sys_EmailSms { get; }
    }
}
