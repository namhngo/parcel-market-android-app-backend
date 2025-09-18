using Microsoft.Extensions.Configuration;
using Gis.API.Infrastructure;
using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly DomainDbContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        private Sys_AuthToken.IService _sys_authtoken;
        private Sys_Category.IService _sys_category;
        private Sys_User.IService _sys_user;
        private Sys_Role.IService _sys_role;
        private Sys_Config.IService _sys_config;
        private Sys_Resource.IService _sys_resource;
        private Sys_Organization.IService _sys_organization;
        private Sys_Permission.IService _sys_permission;
        private Sys_File.IService _sys_file;
        private Sys_Notification.IService _sys_notification;
        //        
        private Por_HoSoNguoiNopPA.IService _por_hosonguoinoppa;
        private Por_HoSoNguoiNop.IService _por_hosonguoinop;
        private Por_QuyTrinh.IService _por_quytrinh;
        private Por_HoSo.IService _por_hoso;
        private Por_TaiKhoan.IService _por_taikhoan;
        private Por_TaiKhoanToken.IService _por_taikhoantoken;
        private Por_LinhVuc.IService _por_linhvuc;
        private Por_ThuaDat.IService _por_thuadat;
        private Por_BuocQuyTrinh.IService _por_buocquytrinh;
        private Por_FileMauThanhPhanHStrongQT.IService _por_filemauthanhphanhstrongqt;
        private Por_FileHoSoNguoiNop.IService _por_filehosonguoinop;
        private Por_ChucNang_BuocQuyTrinh.IService _por_chucnang_buocquytrinh;
        private Por_VanBan.IService _por_vanban;
        private Por_PhanAnh.IService _pro_phananh;
        private Por_HoSo_QuyTrinh.IService _por_hoso_quytrinh;
        private Por_CauHoiThuongGap.IService _por_cauhoithuonggap;
        private Por_LoaiVanBan.IService _por_loaivanban;
        private Por_FileBuocQuyTrinh.IService _por_filebuocquytrinh;

        private Por_BinhLuanGopYPhanAnh.IService _por_binhluangopyphananh;
        private Por_BinhLuanThich.IService _por_binhluanthich;
        private Por_BinhLuanKhongThich.IService _por_binhluankhongthich;

        private Por_NCCDMDSDD.IService _por_nccdmdsdd;
        private Por_GPXD.IService _por_gpxd;
        private Por_GisSoNha.IService _por_gissonha;
        private Por_GCNQSDD.IService _por_gcnqsdd;
        private Por_TemplateSms.IService _por_templatesms;
        private Por_TemplateEmail.IService _por_templateemail;
        private Por_TemplatePhieuBienNhan.IService _por_templatephieubiennhan;

        private Por_TinhThanhPho.IService _por_tinhthanhpho;
        private Por_QuanHuyen.IService _por_quanhuyen;
        private Por_PhuongXaThiTran.IService _por_phuongxathitran;
        private Por_LogSearch.IService _por_logsearch;
        private Sys_EmailSms.IService _sys_emailsms;
        private Por_TaiKhoanLopBanDo.IService _por_taikhoanlopbando;
        private Por_FileHoSo.IService _por_filehoso;
        
        public ServiceWrapper(DomainDbContext context, IDateTimeProvider dateTimeProvider, IUserProvider userService, IConfiguration configuration)
        {
            _context = context;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userService;
        }
        public Por_FileHoSo.IService Por_FileHoSo
        {
            get
            {
                if (_por_filehoso == null)
                {
                    _por_filehoso = new Por_FileHoSo.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_filehoso;
            }
        }
        public Por_TaiKhoanLopBanDo.IService Por_TaiKhoanLopBanDo
        {
            get
            {
                if (_por_taikhoanlopbando == null)
                {
                    _por_taikhoanlopbando = new Por_TaiKhoanLopBanDo.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_taikhoanlopbando;
            }
        }
        public Por_TemplatePhieuBienNhan.IService Por_TemplatePhieuBienNhan
        {
            get
            {
                if (_por_templatephieubiennhan == null)
                {
                    _por_templatephieubiennhan = new Por_TemplatePhieuBienNhan.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_templatephieubiennhan;
            }
        }
        public Por_PhuongXaThiTran.IService Por_PhuongXaThiTran
        {
            get
            {
                if (_por_phuongxathitran == null)
                {
                    _por_phuongxathitran = new Por_PhuongXaThiTran.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_phuongxathitran;
            }
        }
        public Por_QuanHuyen.IService Por_QuanHuyen
        {
            get
            {
                if (_por_quanhuyen == null)
                {
                    _por_quanhuyen = new Por_QuanHuyen.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_quanhuyen;
            }
        }
        public Por_TinhThanhPho.IService Por_TinhThanhPho
        {
            get
            {
                if (_por_tinhthanhpho == null)
                {
                    _por_tinhthanhpho = new Por_TinhThanhPho.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_tinhthanhpho;
            }
        }
        public Por_TemplateEmail.IService Por_TemplateEmail
        {
            get
            {
                if (_por_templateemail == null)
                {
                    _por_templateemail = new Por_TemplateEmail.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_templateemail;
            }
        }
        public Por_TemplateSms.IService Por_TemplateSms
        {
            get
            {
                if (_por_templatesms == null)
                {
                    _por_templatesms = new Por_TemplateSms.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_templatesms;
            }
        }


        public Por_NCCDMDSDD.IService Por_NCCDMDSDD
        {
            get
            {
                if (_por_nccdmdsdd == null)
                {
                    _por_nccdmdsdd = new Por_NCCDMDSDD.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_nccdmdsdd;
            }
        }

        public Por_GPXD.IService Por_GPXD
        {
            get
            {
                if (_por_gpxd == null)
                {
                    _por_gpxd = new Por_GPXD.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_gpxd;
            }
        }
        public Por_GisSoNha.IService Por_GisSoNha
        {
            get
            {
                if (_por_gissonha == null)
                {
                    _por_gissonha = new Por_GisSoNha.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_gissonha;
            }
        }

        public Por_GCNQSDD.IService Por_GCNQSDD
        {
            get
            {
                if (_por_gcnqsdd == null)
                {
                    _por_gcnqsdd = new Por_GCNQSDD.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_gcnqsdd;
            }
        }

        public Por_BinhLuanKhongThich.IService Por_BinhLuanKhongThich
        {
            get
            {
                if (_por_binhluankhongthich == null)
                {
                    _por_binhluankhongthich = new Por_BinhLuanKhongThich.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_binhluankhongthich;
            }
        }
        public Por_BinhLuanThich.IService Por_BinhLuanThich
        {
            get
            {
                if (_por_binhluanthich == null)
                {
                    _por_binhluanthich = new Por_BinhLuanThich.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_binhluanthich;
            }
        }
        public Por_BinhLuanGopYPhanAnh.IService Por_BinhLuanGopYPhanAnh
        {
            get
            {
                if (_por_binhluangopyphananh == null)
                {
                    _por_binhluangopyphananh = new Por_BinhLuanGopYPhanAnh.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_binhluangopyphananh;
            }
        }
        public Por_FileBuocQuyTrinh.IService Por_FileBuocQuyTrinh
        {
            get
            {
                if (_por_filebuocquytrinh == null)
                {
                    _por_filebuocquytrinh = new Por_FileBuocQuyTrinh.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_filebuocquytrinh;
            }
        }
        public Por_ThuaDat.IService Por_ThuaDat
        {
            get
            {
                if (_por_thuadat == null)
                {
                    _por_thuadat = new Por_ThuaDat.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_thuadat;
            }
        }
        public Por_FileHoSoNguoiNop.IService Por_FileHoSoNguoiNop
        {
            get
            {
                if (_por_filehosonguoinop == null)
                {
                    _por_filehosonguoinop = new Por_FileHoSoNguoiNop.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_filehosonguoinop;
            }
        }
        public Por_LoaiVanBan.IService Por_LoaiVanBan
        {
            get
            {
                if (_por_loaivanban == null)
                {
                    _por_loaivanban = new Por_LoaiVanBan.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_loaivanban;
            }
        }
        public Por_CauHoiThuongGap.IService Por_CauHoiThuongGap
        {
            get
            {
                if (_por_cauhoithuonggap == null)
                {
                    _por_cauhoithuonggap = new Por_CauHoiThuongGap.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_cauhoithuonggap;
            }
        }
        public Por_HoSo_QuyTrinh.IService Por_HoSo_QuyTrinh
        {
            get
            {
                if (_por_hoso_quytrinh == null)
                {
                    _por_hoso_quytrinh = new Por_HoSo_QuyTrinh.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_hoso_quytrinh;
            }
        }
        public Por_HoSoNguoiNopPA.IService Por_HoSoNguoiNopPA
        {
            get
            {
                if (_por_hosonguoinoppa == null)
                {
                    _por_hosonguoinoppa = new Por_HoSoNguoiNopPA.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_hosonguoinoppa;
            }
        }
        public Por_HoSoNguoiNop.IService Por_HoSoNguoiNop
        {
            get
            {
                if (_por_hosonguoinop == null)
                {
                    _por_hosonguoinop = new Por_HoSoNguoiNop.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_hosonguoinop;
            }
        }
        public Por_PhanAnh.IService Por_PhanAnh
        {
            get
            {
                if (_pro_phananh == null)
                {
                    _pro_phananh = new Por_PhanAnh.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _pro_phananh;
            }
        }
        public Por_VanBan.IService Por_VanBan
        {
            get
            {
                if (_por_vanban == null)
                {
                    _por_vanban = new Por_VanBan.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_vanban;
            }
        }
        public Por_BuocQuyTrinh.IService Por_BuocQuyTrinh
        {
            get
            {
                if (_por_buocquytrinh == null)
                {
                    _por_buocquytrinh = new Por_BuocQuyTrinh.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_buocquytrinh;
            }
        }
        public Por_FileMauThanhPhanHStrongQT.IService Por_FileMauThanhPhanHStrongQT
        {
            get
            {
                if (_por_filemauthanhphanhstrongqt == null)
                {
                    _por_filemauthanhphanhstrongqt = new Por_FileMauThanhPhanHStrongQT.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_filemauthanhphanhstrongqt;
            }
        }
        public Por_ChucNang_BuocQuyTrinh.IService Por_ChucNang_BuocQuyTrinh
        {
            get
            {
                if (_por_chucnang_buocquytrinh == null)
                {
                    _por_chucnang_buocquytrinh = new Por_ChucNang_BuocQuyTrinh.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_chucnang_buocquytrinh;
            }
        }
        public Por_LinhVuc.IService Por_LinhVuc
        {
            get
            {
                if (_por_linhvuc == null)
                {
                    _por_linhvuc = new Por_LinhVuc.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_linhvuc;
            }
        }
        public Por_QuyTrinh.IService Por_QuyTrinh
        {
            get
            {
                if (_por_quytrinh == null)
                {
                    _por_quytrinh = new Por_QuyTrinh.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_quytrinh;
            }
        }
        public Por_HoSo.IService Por_HoSo
        {
            get
            {
                if (_por_hoso == null)
                {
                    _por_hoso = new Por_HoSo.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_hoso;
            }
        }
        public Por_TaiKhoan.IService Por_TaiKhoan
        {
            get
            {
                if (_por_taikhoan == null)
                {
                    _por_taikhoan = new Por_TaiKhoan.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_taikhoan;
            }
        }
        public Por_TaiKhoanToken.IService Por_TaiKhoanToken
        {
            get
            {
                if (_por_taikhoantoken == null)
                {
                    _por_taikhoantoken = new Por_TaiKhoanToken.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_taikhoantoken;
            }
        }
        //
        public Sys_Notification.IService Sys_Notification
        {
            get
            {
                if (_sys_notification == null)
                {
                    _sys_notification = new Sys_Notification.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _sys_notification;
            }
        }
        public Sys_AuthToken.IService Sys_AuthToken
        {
            get
            {
                if (_sys_authtoken == null)
                {
                    _sys_authtoken = new Sys_AuthToken.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _sys_authtoken;
            }
        }
        public Sys_File.IService Sys_File
        {
            get
            {
                if (_sys_file == null)
                {
                    _sys_file = new Sys_File.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _sys_file;
            }
        }
        public Sys_Category.IService Sys_Category
        {
            get
            {
                if (_sys_category == null)
                {
                    _sys_category = new Sys_Category.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _sys_category;
            }
        }
        public Sys_User.IService Sys_User
        {
            get
            {
                if (_sys_user == null)
                {
                    _sys_user = new Sys_User.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _sys_user;
            }
        }
        public Sys_Organization.IService Sys_Organization
        {
            get
            {
                if (_sys_organization == null)
                {
                    _sys_organization = new Sys_Organization.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _sys_organization;
            }
        }
        public Sys_Role.IService Sys_Role
        {
            get
            {
                if (_sys_role == null)
                {
                    _sys_role = new Sys_Role.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _sys_role;
            }
        }
        public Sys_Config.IService Sys_Config
        {
            get
            {
                if (_sys_config == null)
                {
                    _sys_config = new Sys_Config.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _sys_config;
            }
        }
        public Sys_Resource.IService Sys_Resource
        {
            get
            {
                if (_sys_resource == null)
                {
                    _sys_resource = new Sys_Resource.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _sys_resource;
            }
        }
        public Sys_Permission.IService Sys_Permission
        {
            get
            {
                if (_sys_permission == null)
                {
                    _sys_permission = new Sys_Permission.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _sys_permission;
            }
        }
        public Por_LogSearch.IService Por_LogSearch
        {
            get
            {
                if (_por_logsearch == null)
                {
                    _por_logsearch = new Por_LogSearch.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _por_logsearch;
            }
        }
        public Sys_EmailSms.IService Sys_EmailSms
        {
            get
            {
                if (_sys_emailsms == null)
                {
                    _sys_emailsms = new Sys_EmailSms.Service(_context, _dateTimeProvider, _userProvider);
                }

                return _sys_emailsms;
            }
        }
    }
}
