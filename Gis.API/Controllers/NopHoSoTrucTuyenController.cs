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
using Gis.API.ViewModel.HoSoQuyTrinh;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;
using System.Linq;
using Gis.API.ViewModel.PhanAnh;

namespace Gis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NopHoSoTrucTuyenController : ControllerBase
    {        
        private readonly IUserProvider _userProvider;        
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IUploadFileProvider _fileProvider;
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_TaiKhoanController> _logger;
        private readonly AppSettings appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _savePath;
        public NopHoSoTrucTuyenController(IUploadFileProvider fileProvider, IServiceWrapper service, IUserProvider userService, IJwtAuthManager jwtAuthManager, ILogger<Por_TaiKhoanController> logger, IConfiguration rootConfiguration, IHttpContextAccessor httpContextAccessor)
        {
            appSettings = new AppSettings();
            rootConfiguration.Bind(appSettings);
            _userProvider = userService;            
            _jwtAuthManager = jwtAuthManager;
            _service = service;
            _fileProvider = fileProvider;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _savePath = appSettings.FileServerConfiguration.SavePath;
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
        private static bool ValidateToken(string authToken, string issuer, string audience)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters(issuer, audience);

            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            return true;
        }
        [HttpPost("XemChiTietHoSo")]
        [AllowAnonymous]
        public async Task<IActionResult> XemChiTietHoSo([FromBody] HoSoQuyTrinh chiTietHoSo)
        {
            try
            {
                //var tokenHandler = new JwtSecurityTokenHandler();
                //var jwtToken = tokenHandler.ReadJwtToken(chiTietHoSo.Token);
                //var isValidate = ValidateToken(chiTietHoSo.Token, jwtToken.Issuer, jwtToken.Audiences.ElementAt(0));
                //if (!isValidate)
                //{
                //    return ResponseMessage.Success(null);
                //}
                _logger.LogInformation(string.Format("Call XemChiTietHoSo"));
                var items = await _service.Por_HoSo.XemChiTietHoSo(chiTietHoSo.id);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("XemChiTietHoSo : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("TiepTuc")]
        [AllowAnonymous]
        public virtual async Task<IActionResult> TiepTuc([FromBody] HoSoTiepTuc model)
        {
            try
            {
                _logger.LogInformation("Call TiepTuc");
                var tokenHandler = new JwtSecurityTokenHandler();
                var userName = "";
                if (!string.IsNullOrEmpty(model.token))
                {
                    var jwtToken = tokenHandler.ReadJwtToken(model.token);
                    var isValidate = ValidateToken(model.token, jwtToken.Issuer, jwtToken.Audiences.ElementAt(0));
                    if (isValidate)
                    {
                        userName = jwtToken.Claims.First(o => o.Type == ClaimTypes.Name).Value;
                    }
                }
                var item = await _service.Por_HoSoNguoiNop.TiepTuc(model.Id, userName);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("TiepTuc : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("HoSoDinhKem/XoaFile")]
        [AllowAnonymous]
        public virtual async Task<IActionResult> HoSoDinhKemXoaFile([FromBody] HoSoDinhKemXoaFile model)
        {
            try
            {
                _logger.LogInformation("Call TiepTuc");
                var tokenHandler = new JwtSecurityTokenHandler();
                var userName = "";
                if (!string.IsNullOrEmpty(model.Token))
                {
                    var jwtToken = tokenHandler.ReadJwtToken(model.Token);
                    var isValidate = ValidateToken(model.Token, jwtToken.Issuer, jwtToken.Audiences.ElementAt(0));
                    if (isValidate)
                    {
                        userName = jwtToken.Claims.First(o => o.Type == ClaimTypes.Name).Value;
                    }
                }
                var fileHoSoNguoiNops = new List<Por_FileHoSoNguoiNop>();
                fileHoSoNguoiNops.Add(new Por_FileHoSoNguoiNop() { Id = model.Id });
                var item = _service.Por_FileHoSoNguoiNop.DeleteSave(fileHoSoNguoiNops);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("TiepTuc : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("GetDSHoSo")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDSHoSo([FromBody] DSHoSo dSHoSo)
        {
            try
            {
                _logger.LogInformation(string.Format("Call GetDSHoSo"));
                var tokenHandler = new JwtSecurityTokenHandler();
                var userName = "";
                if (!string.IsNullOrEmpty(dSHoSo.token))
                {
                    var jwtToken = tokenHandler.ReadJwtToken(dSHoSo.token);
                    var isValidate = ValidateToken(dSHoSo.token, jwtToken.Issuer, jwtToken.Audiences.ElementAt(0));
                    if (isValidate)
                    {
                        userName = jwtToken.Claims.First(o => o.Type == ClaimTypes.Name).Value;
                    }
                }
                var items = await _service.Por_HoSo.GetDSHoSo(dSHoSo.search, userName);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetDSHoSo : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("NopHoSo")]
        [AllowAnonymous]
        public async Task<IActionResult> NopHoSo()
        {
            try
            {
                _logger.LogInformation(string.Format("Call NopHoSo"));
                if (Request.Form.TryGetValue("data", out var jsonData))
                {
                    var item = JsonConvert.DeserializeObject<HoSoQuyTrinh>(jsonData);
                    var itemSave = await _service.Por_HoSo.NopHoSo(item);
                    byte[] bytes = null;
                    var FileHoSoNguoiNops = new List<Por_FileHoSoNguoiNop>();
                    foreach (var file in Request.Form.Files)
                    {
                        var FileHoSoNguoiNop = new Por_FileHoSoNguoiNop();
                        string FileName = file.ContentDisposition.Split("\"")[3];
                        string ContentDispositionName = file.ContentDisposition.Split("\"")[1];
                        string ContentType = file.ContentType;
                        if (file.Length > 0)
                        {
                            bytes = new byte[file.Length];
                            string savePath = Path.Combine(_fileProvider.BuildSavePathYYYYMMDD(Path.Combine(_savePath, "FileHoSoNguoiNop")), FileName);
                            using (var stream = new FileStream(savePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            FileHoSoNguoiNop.Id = Guid.NewGuid();
                            FileHoSoNguoiNop.Ten = FileName;
                            FileHoSoNguoiNop.URL = savePath;
                            FileHoSoNguoiNop.IDHoSoNguoiNop = itemSave.thongTinNguoiNop.Id;
                            FileHoSoNguoiNop.IDFileMauThanhPhanHStrongQT = Guid.Parse(ContentDispositionName.Split("_")[0]);
                            FileHoSoNguoiNops.Add(FileHoSoNguoiNop);
                        }
                    }
                    if(FileHoSoNguoiNops.Count > 0)
                    {
                        await _service.Por_FileHoSoNguoiNop.SaveEntitiesAsync(FileHoSoNguoiNops.ToArray());
                    }                    
                    return ResponseMessage.Success(itemSave);
                }            
                return ResponseMessage.Error("Xảy ra lỗi !");
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("NopHoSo : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("SuaHoSo")]
        [AllowAnonymous]
        public async Task<IActionResult> SuaHoSo()
        {
            try
            {
                _logger.LogInformation(string.Format("Call SuaHoSo"));
                if (Request.Form.TryGetValue("data", out var jsonData))
                {
                    var item = JsonConvert.DeserializeObject<SuaHoSoQuyTrinh>(jsonData);
                    var itemSave = await _service.Por_HoSo.SuaHoSo(item);
                    byte[] bytes = null;
                    var FileHoSoNguoiNops = new List<Por_FileHoSoNguoiNop>();
                    foreach (var file in Request.Form.Files)
                    {
                        var FileHoSoNguoiNop = new Por_FileHoSoNguoiNop();
                        string FileName = file.ContentDisposition.Split("\"")[3];
                        string ContentDispositionName = file.ContentDisposition.Split("\"")[1];
                        string ContentType = file.ContentType;
                        if (file.Length > 0)
                        {
                            bytes = new byte[file.Length];
                            string savePath = Path.Combine(_fileProvider.BuildSavePathYYYYMMDD(Path.Combine(_savePath, "FileHoSoNguoiNop")), FileName);
                            using (var stream = new FileStream(savePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            FileHoSoNguoiNop.Id = Guid.NewGuid();
                            FileHoSoNguoiNop.Ten = FileName;
                            FileHoSoNguoiNop.URL = savePath;
                            FileHoSoNguoiNop.IDHoSoNguoiNop = itemSave.thongTinNguoiNop.Id;
                            FileHoSoNguoiNop.IDFileMauThanhPhanHStrongQT = Guid.Parse(ContentDispositionName.Split("_")[0]);
                            FileHoSoNguoiNops.Add(FileHoSoNguoiNop);
                        }
                    }
                    if (FileHoSoNguoiNops.Count > 0)
                    {
                        var items_FileHoSoNguoiNop = await _service.Por_FileHoSoNguoiNop.SaveEntitiesAsync(FileHoSoNguoiNops.ToArray());
                        item.fileHoSoNguoiNops = items_FileHoSoNguoiNop.ToList();
                    }
                    return ResponseMessage.Success(item);
                }
                return ResponseMessage.Error("Xảy ra lỗi !");
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("SuaHoSo : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("LayDsLoaiHoSo")]
        [AllowAnonymous]
        public async Task<IActionResult> LayDsLoaiHoSo()
        {
            try
            {
                _logger.LogInformation(string.Format("Call LayDsLoaiHoSo"));
                var items = await _service.Por_HoSo.LayDsLoaiHoSo();
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("LayDsLoaiHoSo : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("LayDsLoaiPhanAnh")]
        [AllowAnonymous]
        public async Task<IActionResult> LayDsLoaiPhanAnh()
        {
            try
            {
                _logger.LogInformation(string.Format("Call LayDsLoaiPhanAnh"));
                var items = await _service.Por_HoSo.LayDsLoaiPhanAnh();
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("LayDsLoaiPhanAnh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("LayDsMucDichSuDung")]
        [AllowAnonymous]
        public async Task<IActionResult> LayDsMucDichSuDung()
        {
            try
            {
                _logger.LogInformation(string.Format("Call LayDsMucDichSuDung"));
                var items = await _service.Por_HoSo.LayDsMucDichSuDung();
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("LayDsMucDichSuDung : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("LayDsFileMauThanhPhanHS")]
        [AllowAnonymous]
        public async Task<IActionResult> LayDsFileMauThanhPhanHS(Guid IdQuyTrinh)
        {
            try
            {
                _logger.LogInformation(string.Format("Call LayDsFileMauThanhPhanHS"));
                var items = await _service.Por_HoSo.LayDsFileMauThanhPhanHS(IdQuyTrinh);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("LayDsFileMauThanhPhanHS : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("LayDsTieuChiTamDung")]
        [AllowAnonymous]
        public async Task<IActionResult> LayDsTieuChiTamDung()
        {
            try
            {
                _logger.LogInformation(string.Format("Call LayDsTieuChiTamDung"));
                var items = await _service.Por_HoSo.LayDsTieuChiTamDung();
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("LayDsTieuChiTamDung : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("LayDsHinhThucThanhToan")]
        [AllowAnonymous]
        public async Task<IActionResult> LayDsHinhThucThanhToan()
        {
            try
            {
                _logger.LogInformation(string.Format("Call LayDsHinhThucThanhToan"));
                var items = await _service.Por_HoSo.LayDsHinhThucThanhToan();
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("LayDsHinhThucThanhToan : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("LayDsHinhThucNhanKetQua")]
        [AllowAnonymous]
        public async Task<IActionResult> LayDsHinhThucNhanKetQua()
        {
            try
            {
                _logger.LogInformation(string.Format("Call LayDsHinhThucNhanKetQua"));
                var items = await _service.Por_HoSo.LayDsHinhThucNhanKetQua();
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("LayDsHinhThucNhanKetQua : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
