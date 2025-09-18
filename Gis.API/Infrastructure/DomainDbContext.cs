using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Gis.Core.Interfaces;
using Gis.Core.Models;
using Gis.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Gis.API.Infrastructure
{
    public class DomainDbContext : DbContext, IUnitOfWork
    {
        private IDbContextTransaction _dbContextTransaction;        
        public DomainDbContext(DbContextOptions<DomainDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);            
        }
        #region Dataset
        //Hệ thống
        public DbSet<Sys_AuthToken> Sys_AuthTokens { get; set; }
        public DbSet<Sys_Category> Sys_Categories { get; set; }
        public DbSet<Sys_File> Sys_Files { get; set; }
        public DbSet<Sys_Config> Sys_Configs { get; set; }
        public DbSet<Sys_Organization> Sys_Organizations { get; set; }
        public DbSet<Sys_Permission> Sys_Permissions { get; set; }        
        public DbSet<Sys_Resource> Sys_Resources { get; set; }
        public DbSet<Sys_Role> Sys_Roles { get; set; }        
        public DbSet<Sys_User> Sys_Users { get; set; }
        public DbSet<Sys_User_Role> Sys_Users_Roles { get; set; }
        public DbSet<Sys_Notification> Sys_Notifications { get; set; }
        //Cổng thông tin
        public DbSet<Por_TaiKhoanToken> Por_TaiKhoanTokens { get; set; }
        public DbSet<Por_TaiKhoan> Por_TaiKhoans { get; set; }
        public DbSet<Por_TinhThanhPho> Por_TinhThanhPhos { get; set; }
        public DbSet<Por_QuanHuyen> Por_QuanHuyens { get; set; }
        public DbSet<Por_PhuongXaThiTran> Por_PhuongXaThiTrans { get; set; }
        public DbSet<Por_LoaiVanBanPhapQuy> Por_LoaiVanBanPhapQuys { get; set; }        
        public DbSet<Por_VanBanPhapQuy> Por_VanBanPhapQuys { get; set; }        
        public DbSet<Por_CauHoiThuongGap> Por_CauHoiThuongGaps { get; set; }
        public DbSet<Por_HuongDanSuDung> Por_HuongDanSuDungs { get; set; }
        public DbSet<Por_FileGopYPhanAnh> Por_FileGopYPhanAnhs { get; set; }
        public DbSet<Por_GopYPhanAnh> Por_GopYPhanAnhs { get; set; }
        //
        public DbSet<Por_TemplateEmail> Por_TemplateEmails { get; set; }
        public DbSet<Por_TemplateSms> Por_TemplateSmss { get; set; }
        //
        public DbSet<Por_GCNQSDD> Por_GCNQSDDs { get; set; }
        public DbSet<Por_NCCDMDSDD> Por_NCCDMDSDDs { get; set; }
        public DbSet<Por_GPXD> Por_GPXDs { get; set; }
        public DbSet<Por_GisSoNha> Por_GisSoNhas { get; set; }
        //Quy trình
        public DbSet<Por_ThuatDat> Por_ThuatDats { get; set; }
        //public DbSet<Por_MucDichSuDung> Por_MucDichSuDungs { get; set; }
        public DbSet<Por_FileHoSoNguoiNop> Por_FileHoSoNguoiNops { get; set; }
        public DbSet<Por_HoSoNguoiNop> Por_HoSoNguoiNops { get; set; }
        public DbSet<Por_FileBuocQuyTrinh> Por_FileBuocQuyTrinhs { get; set; }
        //public DbSet<Por_TrangThai> Por_TrangThais { get; set; }
        //
        public DbSet<Por_HoSo_QuyTrinh> Por_HoSo_QuyTrinhs { get; set; }
        public DbSet<Por_HoSo_Buoc_QuyTrinh> Por_HoSo_Buoc_QuyTrinhs { get; set; }
        //
        public DbSet<Por_HoSoPA_QuyTrinh> Por_HoSoPA_QuyTrinhs { get; set; }
        public DbSet<Por_HoSoPA_Buoc_QuyTrinh> Por_HoSoPA_Buoc_QuyTrinhs { get; set; }
        //
        public DbSet<Por_FileMauThanhPhanHStrongQT> Por_FileMauThanhPhanHStrongQTs { get; set; }
        public DbSet<Por_FileMauBienNhanHS> Por_FileMauBienNhanHSs { get; set; }
        public DbSet<Por_LinhVuc> Por_LinhVucs { get; set; }       
        public DbSet<Por_QuyTrinh> Por_QuyTrinhs { get; set; }
        public DbSet<Por_BuocQuyTrinh> Por_BuocQuyTrinhs { get; set; }
        public DbSet<Por_ChucNang_BuocQuyTrinh> Por_ChucNang_BuocQuyTrinhs { get; set; }
        //
        public DbSet<Por_BinhLuanGopYPhanAnh> Por_BinhLuanGopYPhanAnhs { get; set; }
        public DbSet<Por_BinhLuanThich> Por_BinhLuanThichs { get; set; }
        public DbSet<Por_BinhLuanKhongThich> Por_BinhLuanKhongThichs { get; set; }
        public DbSet<Por_LogSearchGis> Por_LogSearchGiss { get; set; }
        public DbSet<Sys_EmailSms> Sys_EmailSmss { get; set; }
        //
        public DbSet<Por_TemplatePhieuBienNhan> Por_TemplatePhieuBienNhans { get; set; }
        //
        public DbSet<Por_TaiKhoanLopBanDo> Por_TaiKhoanLopBanDos { get; set; }
        //
        public DbSet<Por_FileHoSo> Por_FileHoSos { get; set; }
        #endregion

        #region IUnitOfWork
        public void CreateTransaction()
        {
            _dbContextTransaction = Database.BeginTransaction();            
        }
        public void Commit()
        {
            _dbContextTransaction.Commit();
        }
        public void Roolback()
        {
            _dbContextTransaction.Rollback();
            _dbContextTransaction.Dispose();
        }
        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
        private void OnBeforeSaveChanges()
        {
            //var rs = LoggingExtensions.TrackingAuditLogs(Guid.Empty, "", ChangeTracker);
        }
        #endregion
    }
}
