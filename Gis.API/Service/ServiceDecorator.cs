using Gis.Core.Interfaces;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gis.API.Service
{
    class ServiceDecorator<TEntity>
    {
        private IRepositoryBase<TEntity> _serviceBase;
        public ServiceDecorator(IServiceWrapper service)
        {
            #region add service
            if (typeof(TEntity) == typeof(Model.Sys_Category))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_Category;
            }
            else if(typeof(TEntity) == typeof(Model.Sys_User))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_User;
            }
            else if (typeof(TEntity) == typeof(Model.Sys_File))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_File;
            }
            else if (typeof(TEntity) == typeof(Model.Sys_Organization))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_Organization;
            }
            else if (typeof(TEntity) == typeof(Model.Sys_Role))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_Role;
            }
            else if (typeof(TEntity) == typeof(Model.Sys_Config))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_Config;
            }
            else if (typeof(TEntity) == typeof(Model.Sys_Permission))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_Permission;
            }
            else if (typeof(TEntity) == typeof(Model.Sys_Resource))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_Resource;
            }
            else if (typeof(TEntity) == typeof(Model.Sys_Notification))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_Notification;
            }
            else if (typeof(TEntity) == typeof(Model.Por_TaiKhoan))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_TaiKhoan;
            }
            else if (typeof(TEntity) == typeof(Model.Por_QuyTrinh))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_QuyTrinh;
            }
            else if (typeof(TEntity) == typeof(Model.Por_LinhVuc))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_LinhVuc;
            }
            else if (typeof(TEntity) == typeof(Model.Por_ChucNang_BuocQuyTrinh))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_ChucNang_BuocQuyTrinh;
            }
            else if (typeof(TEntity) == typeof(Model.Por_BuocQuyTrinh))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_BuocQuyTrinh;
            }
            else if (typeof(TEntity) == typeof(Model.Por_FileMauThanhPhanHStrongQT))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_FileMauThanhPhanHStrongQT;
            }
            else if (typeof(TEntity) == typeof(Model.Por_VanBanPhapQuy))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_VanBan;
            }
            else if (typeof(TEntity) == typeof(Model.Por_LoaiVanBanPhapQuy))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_LoaiVanBan;
            }
            else if (typeof(TEntity) == typeof(Model.Por_GopYPhanAnh))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_PhanAnh;
            }
            else if (typeof(TEntity) == typeof(Model.Por_HoSoNguoiNop))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_HoSoNguoiNop;
            }
            else if (typeof(TEntity) == typeof(Model.Por_CauHoiThuongGap))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_CauHoiThuongGap;
            }
            else if (typeof(TEntity) == typeof(Model.Por_ThuatDat))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_ThuaDat;
            }
            else if (typeof(TEntity) == typeof(Model.Por_FileBuocQuyTrinh))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_FileBuocQuyTrinh;
            }
            else if (typeof(TEntity) == typeof(Model.Por_NCCDMDSDD))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_NCCDMDSDD;
            }
            else if (typeof(TEntity) == typeof(Model.Por_GPXD))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_GPXD;
            }
            else if (typeof(TEntity) == typeof(Model.Por_GCNQSDD))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_GCNQSDD;
            }
            else if (typeof(TEntity) == typeof(Model.Por_TemplateSms))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_TemplateSms;
            }
            else if (typeof(TEntity) == typeof(Model.Por_TemplateEmail))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_TemplateEmail;
            }
            else if (typeof(TEntity) == typeof(Model.Por_TemplatePhieuBienNhan))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_TemplatePhieuBienNhan;
            }
            else if (typeof(TEntity) == typeof(Model.Por_TinhThanhPho))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_TinhThanhPho;
            }
            else if (typeof(TEntity) == typeof(Model.Por_QuanHuyen))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_QuanHuyen;
            }
            else if (typeof(TEntity) == typeof(Model.Por_PhuongXaThiTran))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_PhuongXaThiTran;
            }   
            else if (typeof(TEntity) == typeof(Model.Por_LogSearchGis))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_LogSearch;
            }
            else if (typeof(TEntity) == typeof(Model.Por_GisSoNha))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_GisSoNha;
            }
            else if (typeof(TEntity) == typeof(Model.Sys_EmailSms))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_EmailSms;
            }
            else if (typeof(TEntity) == typeof(Model.Por_TaiKhoanLopBanDo))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_TaiKhoanLopBanDo;
            }
            else if (typeof(TEntity) == typeof(Model.Por_FileHoSo))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Por_FileHoSo;
            }

            #endregion
        }
        public async Task<TEntity> SaveEntityAsync(TEntity entity)
        {
            return await _serviceBase.SaveEntityAsync(entity);
        }
        public async Task<TEntity[]> SaveEntitiesAsync(TEntity[] entities)
        {
            return await _serviceBase.SaveEntitiesAsync(entities);
        }        
        public async Task<Paged<TEntity>> GetPagedAsync(int page, int pageSize, int totalLimitItems, string search)
        {
            return await _serviceBase.GetPagedAsync(page, pageSize, totalLimitItems, search);
        }
        public List<TEntity> GetCategories()
        {
            return _serviceBase.GetCategories();
        }
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _serviceBase.GetByIdAsync(id);
        }
        public async Task Delete(List<TEntity> entity)
        {
            await _serviceBase.DeleteSave(entity);
        }
    }
}
