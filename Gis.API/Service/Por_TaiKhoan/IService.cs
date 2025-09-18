using Gis.Core.Interfaces;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_TaiKhoan
{
    public interface IService: IRepositoryBase<Model.Por_TaiKhoan>
    {
        Task UserForgetPassword(string Email);
        Task UserChangePassword(Guid UserId, string PassWord);
        Task<LoginResult> CheckUserLogin(string UserName, string Password);
        Task<ViewModel.TaiKhoan.ThongTinResponse> LayThongTin(Guid Id);
        Task<ViewModel.TaiKhoan.ThongTinResponse> SuaThongTin(ViewModel.TaiKhoan.ThongTinResponse taiKhoan);
        Task<bool> CheckUserNameExists(string UserName);
        Task<bool> CheckEmailExists(string Email);
        Task<bool> IsDupicateAttributesAsync(Guid? Id, string UserName, string Email);
    }
}
