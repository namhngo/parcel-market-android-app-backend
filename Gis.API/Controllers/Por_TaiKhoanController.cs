using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Gis.API.Infrastructure.Authentication;
using Gis.Core.Interfaces;
using Gis.API.Service;
using Gis.Core.Models;
using Gis.Core.Constant;
using System.Security.Claims;
using Gis.Core.Helpers;
using Gis.Core.Core;
using Microsoft.Extensions.Configuration;
using Gis.API.Infrastructure.Authorization;
using Gis.API.Model;
using Newtonsoft.Json;
using Google.Apis.Auth;
using System.Collections.Generic;
using Gis.API.Infrastructure;
using FacebookCore;
using Gis.InfraCore.Email;
using Microsoft.AspNetCore.SignalR.Client;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;
using System.Linq;
using static Gis.Core.Constant.Sys_Const;
using k8s.KubeConfigModels;

namespace Gis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Por_TaiKhoanController : ApiControllerBase<Por_TaiKhoan>
    {        
        private readonly IUserProvider _userProvider;        
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_TaiKhoanController> _logger;
        private readonly AppSettings appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly HubConnection connection;
        public Por_TaiKhoanController(IServiceWrapper service, IUserProvider userService, IJwtAuthManager jwtAuthManager, ILogger<Por_TaiKhoanController> logger, IConfiguration rootConfiguration, IHttpContextAccessor httpContextAccessor)
            :base(service, logger)
        {
            appSettings = new AppSettings();
            rootConfiguration.Bind(appSettings);
            _userProvider = userService;            
            _jwtAuthManager = jwtAuthManager;
            _service = service;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("CheckDuplicateAttributes")]
        [AuthorizeFilter]
        public async Task<IActionResult> KiemTraTrungThuocTinhTaiKhoan(Guid? id, string userName, string email)
        {
            try
            {
                _logger.LogInformation(string.Format("Call KiemTraTrungThuocTinhTaiKhoan params: (id = {0}, UserName = {1}, Email = {2})", id, userName, email));
                var result = await _service.Por_TaiKhoan.IsDupicateAttributesAsync(id, userName, email);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("KiemTraTrungThuocTinhTaiKhoan : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet]
        [AuthorizeFilter]
        public async Task<IActionResult> TimKiemDSTaiKhoanTheoTenTaiKhoanKVaTrangThai(int page = 1, int pageSize = 10, int totalLimitItems = 500, string trangthai = "", string tentaikhoan = "")
        {
            try
            {
                _logger.LogInformation(string.Format("Call TimKiemDSTaiKhoanTheoTenTaiKhoanKVaTrangThai params: (page = {0}, pageSize = {1}, totalLimitItems = {2}, loginName = {3}, isActive = {4})", page, pageSize, totalLimitItems, tentaikhoan, trangthai));
                string search = "";
                if (!string.IsNullOrEmpty(tentaikhoan))
                {
                    search += $" TenDangNhap.Contains(\"{tentaikhoan}\") ";
                    search += $" && ";
                }
                if (!string.IsNullOrEmpty(trangthai))
                {
                    search += $" TrangThai == {trangthai} ";
                }
                var items = await _service.Por_TaiKhoan.GetPagedAsync(page, pageSize, totalLimitItems, search);
                foreach(var item in items.Items)
                {
                    item.MatKhau = "";
                }    
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("TimKiemDSTaiKhoanTheoTenTaiKhoanKVaTrangThai : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        private static bool ValidateToken(string authToken, string issuer, string audience)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters(issuer, audience);

            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            return true;
        }
        private static TokenValidationParameters GetValidationParameters(string issuer, string audience)
        {

            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("abcb.,/#@$&^&*()()(*)(*(*&#@$#Ơ}Ơ}b675/_cbasdasljdlkj7687546")),
                ValidAudience = audience,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }
        [HttpPost("QuenMatKhau")]
        [AllowAnonymous]
        public async Task<IActionResult> QuenMatKhau([FromBody] ForgetPasswordRequest request)
        {
            try
            {
                _logger.LogInformation(string.Format("Call ForgetPasswordRequest body: ({0})", JsonConvert.SerializeObject(request)));
                await _service.Por_TaiKhoan.UserForgetPassword(request.Email);
                return ResponseMessage.Success(Sys_Const.Message.SERVICE_FORGETPASSWORD_SUCCESS);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("ForgetPasswordRequest : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("DoiMaiKhau")]
        [AuthorizeFilter]
        public async Task<IActionResult> DoiMaiKhau([FromBody] ChangePasswordRequest request)
        {
            try
            {
                _logger.LogInformation(string.Format("Call ChangePassword body: ({0})", JsonConvert.SerializeObject(request)));
                if (!UserHelpers.IsValidUserChangePassword(_userProvider.UserName, request.PasswordOld, request.PasswordNew, out string message))
                {
                    return ResponseMessage.Error(message);
                }
                var user = await _service.Por_TaiKhoan.CheckUserLogin(_userProvider.UserName, request.PasswordOld);
                await _service.Por_TaiKhoan.UserChangePassword(user.UserId, request.PasswordNew);
                return ResponseMessage.Success(Sys_Const.Message.SERVICE_CHANGEPASSWORD_SUCCESS);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("ChangePassword : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("LayThongTin")]
        [AuthorizeFilter]
        public async Task<IActionResult> LayThongTin([FromBody] ViewModel.TaiKhoan.ThongTinRequest thongTinRequest)
        {
            try
            {
                _logger.LogInformation("Call LayThongTin");
                //var tokenHandler = new JwtSecurityTokenHandler();
                //var jwtToken = tokenHandler.ReadJwtToken(thongTinRequest.Token);
                //var isValidate = ValidateToken(thongTinRequest.Token, jwtToken.Issuer, jwtToken.Audiences.ElementAt(0));
                //if (!isValidate)
                //{
                //    return ResponseMessage.Error(Message.SERVICE_ERROR);
                //}
                var item = await _service.Por_TaiKhoan.LayThongTin(thongTinRequest.Id);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("LayThongTin : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("SuaThongTin")]
        [AuthorizeFilter]
        public async Task<IActionResult> SuaThongTin([FromBody] ViewModel.TaiKhoan.ThongTinResponse thongTinRequest)
        {
            try
            {
                _logger.LogInformation("Call SuaThongTin");
                var item = await _service.Por_TaiKhoan.SuaThongTin(thongTinRequest);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("SuaThongTin : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("DangNhap")]
        [AllowAnonymous]
        public async Task<IActionResult> DangNhap([FromBody] LoginRequest request)
        {
            try
            {
                _logger.LogInformation(string.Format("Call Login body: ({0})", JsonConvert.SerializeObject(request)));
                if (!UserHelpers.IsValidUserLogin(request.UserName, request.Password, out string message))
                {
                    throw new Exception(message);                    
                }
                var result = await _service.Por_TaiKhoan.CheckUserLogin(request.UserName, request.Password);
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()),
                    new Claim(ClaimTypes.Name, result.UserName),                    
                };

                var jwtResult = _jwtAuthManager.GenerateTokens(result.UserName, claims, DateTime.Now);                
                result.AccessToken = jwtResult.AccessToken;
                Por_TaiKhoanToken authToken = new Por_TaiKhoanToken();                
                authToken.TokenTruyCap = jwtResult.AccessToken;
                authToken.TokenLamMoi = jwtResult.RefreshToken.TokenString;
                await _service.Por_TaiKhoanToken.SaveByUserNameAsync(result.UserName, authToken);                
                return ResponseMessage.Success(result, Sys_Const.Message.SERVICE_LOGIN_SUCCESS);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Login : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPost("DangKy")]
        [AllowAnonymous]
        public async Task<IActionResult> DangKy([FromBody] Por_TaiKhoan request)
        {
            try
            {
                _logger.LogInformation(string.Format("Call Signup body: ({0})", JsonConvert.SerializeObject(request)));
                if (!UserHelpers.IsValidUserLogin(request.TenDangNhap, request.MatKhau, out string message))
                {
                    throw new Exception(message);
                }
                var isUserNameExisted = await _service.Por_TaiKhoan.CheckUserNameExists(request.TenDangNhap);
                if (isUserNameExisted)
                {
                    throw new Exception(Sys_Const.Message.SERVICE_USERNAME_EXISTS);
                }
                var isEmailExisted = await _service.Por_TaiKhoan.CheckEmailExists(request.Email);
                if (isEmailExisted)
                {
                    throw new Exception(Sys_Const.Message.SERVICE_EMAIL_EXISTS);
                }
                request.TenDangNhap = request.TenDangNhap.RemoveAllWhitespace();
                request.Email = request.Email.RemoveAllWhitespace();
                request.MatKhau = Cryption.EncryptByKey(request.MatKhau, Sys_Const.Security.key);
                request.TrangThai = true;
                request = await _service.Por_TaiKhoan.SaveEntityAsync(request);
                request.MatKhau = string.Empty;
                return ResponseMessage.Success(request, Sys_Const.Message.SERVICE_SIGNUP_SUCCESS);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Signup : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPost]
        [AuthorizeFilter]
        public override async Task<IActionResult> Create([FromBody] Por_TaiKhoan model)
        {
            try
            {
                _logger.LogInformation(string.Format("Call Create body: ({0})", JsonConvert.SerializeObject(model)));
                var item = await _service.Por_TaiKhoan.SaveEntityAsync(model);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Create : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPut]
        [AuthorizeFilter]
        public override async Task<IActionResult> Update([FromBody] Por_TaiKhoan model)
        {
            try
            {
                _logger.LogInformation(string.Format("Call Update body: ({0})", JsonConvert.SerializeObject(model)));
                if(string.IsNullOrEmpty(model.MatKhau))
                {
                    var taiKhoan = await _service.Por_TaiKhoan.GetByIdAsync(model.Id);
                    model.MatKhau = taiKhoan.MatKhau;
                }   
                else
                {
                    model.MatKhau = Cryption.EncryptByKey(model.MatKhau, Sys_Const.Security.key); 
                }    
                var item = await _service.Por_TaiKhoan.SaveEntityAsync(model);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Update : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [AuthorizeFilter]
        public override async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                _logger.LogInformation(string.Format("Call GetById params: (id = {0})", id));
                var item = await _service.Por_TaiKhoan.GetByIdAsync(id);
                item.MatKhau = "";
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetById : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
