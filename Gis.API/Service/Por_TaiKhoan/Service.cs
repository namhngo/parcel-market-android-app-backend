using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Gis.API.Infrastructure;
using Gis.API.Model;
using Gis.Core.Constant;
using Gis.Core.Core;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s.KubeConfigModels;

namespace Gis.API.Service.Por_TaiKhoan
{
    public class Service:RepositoryBase<Model.Por_TaiKhoan>, Por_TaiKhoan.IService
    {
        private readonly DomainDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;                
        public Service(DomainDbContext dbContext, IDateTimeProvider dateTimeProvider, IUserProvider userService) :base(dbContext, dateTimeProvider, userService)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userService;            
        }
        public async Task<ViewModel.TaiKhoan.ThongTinResponse> SuaThongTin(ViewModel.TaiKhoan.ThongTinResponse taikhoan)
        {
            var item = await _dbContext.Por_TaiKhoans.Where(o => o.Id == taikhoan.Id).FirstAsync();
            item.HoTen = taikhoan.HoTen;
            item.Email = taikhoan.Email;
            item.SoDienThoai = taikhoan.SoDienThoai;
            item.DiaChi = taikhoan.DiaChi;
            await _dbContext.SaveChangesAsync();
            return taikhoan;
        }    
        public async Task<ViewModel.TaiKhoan.ThongTinResponse> LayThongTin(Guid Id)
        {
            return await _dbContext.Por_TaiKhoans.Where(o => o.Id == Id)
                .Select(o => new ViewModel.TaiKhoan.ThongTinResponse() { 
                    Id = o.Id, 
                    TenDangNhap = o.TenDangNhap, 
                    HoTen = o.HoTen,
                    Email = o.Email,
                    SoDienThoai = o.SoDienThoai,
                    DiaChi = o.DiaChi
                }).FirstOrDefaultAsync();
        }
        public async Task UserForgetPassword(string Email)
        {
            var MatKhauMoi = StringHelpers.RandomString(10);
            var oUser = await _dbContext.Por_TaiKhoans.SingleOrDefaultAsync(o => o.Email == Email);
            if (oUser == null)
            {
                throw new Exception(Sys_Const.Message.SERVICE_EMAIL_NOT_EXISTS);
            }
            if (!string.IsNullOrEmpty(MatKhauMoi))
            {
                oUser.MatKhau = Cryption.EncryptByKey(MatKhauMoi, Sys_Const.Security.key);
            }
            await UnitOfWork.SaveAsync();
            var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone);
            _dbContext.Database.ExecuteSqlRaw("INSERT INTO public.por_smssend (mobile, email, content, contentemail, status, statusmail, time_create, time_send, type) " +
                                              "VALUES ('', '" + Email + "', '', 'Mật khẩu mới: "+ MatKhauMoi + ", vui lòng đổi lại mật khẩu của bạn', '0', '0', '"+ DateTimeOffsetNow.ToString("dd/MM/yyyy HH:mm:ss") + "', '', 'ResetPass' )");
        }
        public async Task UserChangePassword(Guid UserId, string PassWord)
        {
            var oUser = await _dbContext.Por_TaiKhoans.SingleOrDefaultAsync(o => o.Id == UserId);
            if (!string.IsNullOrEmpty(PassWord))
            {
                oUser.MatKhau = Cryption.EncryptByKey(PassWord, Sys_Const.Security.key);
            }
            await UnitOfWork.SaveAsync();
        }
        public async Task<LoginResult> CheckUserLogin(string UserName, string Password)
        {            
            var obj = await (from a in _dbContext.Por_TaiKhoans
                             where a.TenDangNhap == UserName || a.Email == UserName
                             select new
                             {
                                 UserId = a.Id,
                                 UserName = a.TenDangNhap,
                                 Password = a.MatKhau,
                                 IsActive = a.TrangThai                                 
                             }).FirstOrDefaultAsync();
            if (obj == null)
            {
                throw new(Sys_Const.Message.SERVICE_USERNAME_UNEXISTS);
            }
            else
            {
                if (Cryption.DecryptByKey(obj.Password, Sys_Const.Security.key) != Password)
                {
                    throw new(Sys_Const.Message.SERVICE_PASS_INCORRECT);
                }
                if (!obj.IsActive)
                {
                    throw new(Sys_Const.Message.SERVICE_USERNAME_UNACTIVE);
                }
            }
            List<PermFeature> permFeatures = new List<PermFeature>();
            var ArrPermFeatures = await _dbContext.Por_TaiKhoanLopBanDos.Select(o => o.IdLopBanDo).ToListAsync();
            var dmLopBanDo = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.LopBanDo).ToListAsync();
            for (int i = 0; i < dmLopBanDo.Count; i++)
            {
                for (int j = 0; j < ArrPermFeatures.Count; j++)
                {
                    if (dmLopBanDo[i].Id == ArrPermFeatures[j])
                    {
                        permFeatures.Add(new PermFeature()
                        {
                            title = dmLopBanDo[i].Name,
                            featureType = dmLopBanDo[i].Code,
                        });
                        break;
                    }
                }
            }
            return new LoginResult() { UserId = obj.UserId, PermFeatures = permFeatures, UserName = obj.UserName };
        }
        public async Task<bool> CheckUserNameExists(string UserName)
        {
            var usernameExists = await _dbContext.Por_TaiKhoans.Where(o => o.TenDangNhap == UserName).AnyAsync();
            return usernameExists;
        }
        public async Task<bool> CheckEmailExists(string Email)
        {
            var emailExists = await _dbContext.Por_TaiKhoans.Where(o => o.Email == Email).AnyAsync();
            return emailExists;
        }
        public async Task<bool> IsDupicateAttributesAsync(Guid? Id, string UserName, string Email)
        {
            bool result = false;
            if (GuidHelpers.IsNullOrEmpty(Id))
            {
                result = await _dbContext.Por_TaiKhoans.Where(o => o.TenDangNhap == UserName || o.Email == Email).AnyAsync();
            }
            else
            {
                var count = await _dbContext.Por_TaiKhoans.Where(o => o.Id == Id && (o.TenDangNhap == UserName || o.Email == Email)).CountAsync();
                if (count <= 1)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            return await Task.FromResult(result);
        }
    }
}
