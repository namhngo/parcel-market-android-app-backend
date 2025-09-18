using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Gis.API.Infrastructure;
using Gis.API.Infrastructure.Authorization;
using Gis.API.Model;
using Gis.API.Service;
using Gis.API.ViewModel.PhanAnh;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using Gis.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using static Gis.API.Infrastructure.Enums;
using static Gis.Core.Constant.Sys_Const;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gis.API.Controllers
{
    public class Por_PhanAnhController : ApiControllerBase<Por_GopYPhanAnh>
    {
        // GET: /<controller>/
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_PhanAnhController> _logger;
        private readonly string _savePath;
        private readonly IUploadFileProvider _fileProvider;
        private readonly AppSettings appSettings;
        private readonly IConfiguration _rootConfiguration;
        public Por_PhanAnhController(IConfiguration rootConfiguration, IUploadFileProvider fileProvider, IServiceWrapper service, ILogger<Por_PhanAnhController> logger) : base(service, logger)
        {
            appSettings = new AppSettings();
            rootConfiguration.Bind(appSettings);
            _rootConfiguration = rootConfiguration;
            _service = service;
            _fileProvider = fileProvider;
            _logger = logger;
            _savePath = appSettings.FileServerConfiguration.SavePath;
        }
        [HttpGet("{page}/{pageSize}/{totalLimitItems}")]
        [AuthorizeFilter]
        public override async Task<IActionResult> GetListPaged(int page = 1, int pageSize = 10, int totalLimitItems = 500)
        {
            try
            {
                _logger.LogInformation(string.Format("Call GetListPaged params: (page = {0}, pageSize = {1}, totalLimitItems = {2})", page, pageSize, totalLimitItems));
                var items = await _service.Por_PhanAnh.GetPagedCustomAsync(page, pageSize, totalLimitItems, "");
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetListPaged : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("CheckDuplicateAttributes")]
        [AuthorizeFilter]
        public async Task<IActionResult> CheckDuplicateAttributes(Guid? Id, string TenVanBan)
        {
            try
            {
                _logger.LogInformation(string.Format("Call CheckDuplicateAttributes params: (id = {0}, name = {1})", Id, TenVanBan));
                var result = await _service.Por_QuyTrinh.IsDupicateAttributesAsync(Id, TenVanBan);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("CheckDuplicateAttributes : {0}", ex.Message));
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
        [HttpPost("GhiChuPhanAnh")]
        [AuthorizeFilter]
        public async Task<IActionResult> GhiChuPhanAnh([FromBody] GhiChuPhanAnh ghiChuPhanAnh)
        {
            try
            {
                await _service.Por_PhanAnh.GhiChuPhanAnh(ghiChuPhanAnh);
                return ResponseMessage.Success(ghiChuPhanAnh);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("XemNoiDungPhanAnh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("LayDSBinhLuanPhanAnh")]
        [AllowAnonymous]
        public async Task<IActionResult> LayDSBinhLuanPhanAnh(Guid IDGopYPhanAnh, string TaiKhoan)
        {
            try
            {
                var items = await _service.Por_PhanAnh.LayDSBinhLuanPhanAnh(IDGopYPhanAnh, TaiKhoan);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("LayDSBinhLuanPhanAnh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("BinhLuanPhanAnh")]
        [AllowAnonymous]
        public async Task<IActionResult> BinhLuanPhanAnh([FromBody] BinhLuanPhanAnh binhLuanPhanAnh)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(binhLuanPhanAnh.Token);
                var isValidate = ValidateToken(binhLuanPhanAnh.Token, jwtToken.Issuer, jwtToken.Audiences.ElementAt(0));
                if (!isValidate)
                {
                    return ResponseMessage.Success(null);
                }
                _logger.LogInformation(string.Format("Call BinhLuanPhanAnh"));
                await _service.Por_PhanAnh.BinhLuanPhanAnh(binhLuanPhanAnh);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("BinhLuanPhanAnh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("CongKhai")]
        [AllowAnonymous]
        public async Task<IActionResult> CongKhaiPA([FromBody] CongKhaiPA congkhai)
        {
            try
            {
                _logger.LogInformation(string.Format("Call CongKhai"));
                await _service.Por_PhanAnh.CongKhaiPA(congkhai.Id, congkhai.CongKhai);
                return ResponseMessage.Success(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("CongKhai : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("XemChiTietPhanAnh")]
        [AllowAnonymous]
        public async Task<IActionResult> XemChiTietPhanAnh([FromBody] ChiTietPhanAnh chiTietPhanAnh)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(chiTietPhanAnh.Token);                
                var isValidate = ValidateToken(chiTietPhanAnh.Token, jwtToken.Issuer, jwtToken.Audiences.ElementAt(0));
                if(!isValidate)
                {
                    return ResponseMessage.Success(null);
                }    
                _logger.LogInformation(string.Format("Call XemChiTietPhanAnh"));
                var items = await _service.Por_PhanAnh.XemChiTietPhanAnh(chiTietPhanAnh.Id);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("XemChiTietPhanAnh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("XemNoiDungPhanAnh")]
        [AllowAnonymous]
        public async Task<IActionResult> XemNoiDungPhanAnh(Guid Id)
        {
            try
            {
                _logger.LogInformation(string.Format("Call GetDSPhanAnh"));
                var items = await _service.Por_PhanAnh.XemNoiDungPhanAnh(Id);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("XemNoiDungPhanAnh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("GetDSPhanAnh")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDSPhanAnh([FromBody] DSPhanAnh dSPhanAnh)
        {
            try
            {
                _logger.LogInformation(string.Format("Call GetDSPhanAnh"));
                var tokenHandler = new JwtSecurityTokenHandler();
                var userName = "";
                if (!string.IsNullOrEmpty(dSPhanAnh.token))
                {
                    var jwtToken = tokenHandler.ReadJwtToken(dSPhanAnh.token);
                    var isValidate = ValidateToken(dSPhanAnh.token, jwtToken.Issuer, jwtToken.Audiences.ElementAt(0));
                    if (isValidate)
                    {
                        userName = jwtToken.Claims.First(o => o.Type == ClaimTypes.Name).Value;
                    }
                }
                var items = await _service.Por_PhanAnh.GetDSPhanAnh(dSPhanAnh.search, userName);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetDSPhanAnh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("GuiPhanAnh")]
        [AllowAnonymous]
        public async Task<IActionResult> GuiPhanAnh(Por_GopYPhanAnh model)
        {
            try
            {
                _logger.LogInformation(string.Format("Call GuiPhanAnh"));                                
                var item = await _service.Por_PhanAnh.GuiPhanAnh(model);                
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GuiPhanAnh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("SaveMultipleAndUploadFile")]
        [AllowAnonymous]
        public async Task<IActionResult> SaveMultipleAndUploadFile()
        {
            try
            {
                _logger.LogInformation("Call Save multiple");
                if (Request.Form.TryGetValue("data", out var jsonData))
                {
                    var item = JsonConvert.DeserializeObject<Por_GopYPhanAnh>(jsonData);
                    byte[] bytes = null;
                    foreach (var file in Request.Form.Files)
                    {
                        string FileName = file.ContentDisposition.Split("\"")[3];
                        string ContentDispositionName = file.ContentDisposition.Split("\"")[1];
                        string ContentType = file.ContentType;
                        if (file.Length > 0)
                        {
                            bytes = new byte[file.Length];                            
                            string savePath = _fileProvider.BuildSavePathYYYYMMDD(Path.Combine(_savePath, "PhanAnhGopY"));
                            if (!System.IO.File.Exists(savePath))
                            {
                                Directory.CreateDirectory(savePath);
                            }
                            using (var stream = new FileStream(Path.Combine(savePath, FileName), FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            item.TenFile = FileName;
                            item.URL = Path.Combine(savePath, FileName);
                        }
                    }                    
                    //var itemCategory = await _service.Sys_Category.GetItemByCode(Core.Enums.CategoryType.TrangThaiHoSo, TrangThaiQuyTrinh.choxuly.ToString());
                    //item.IDTrangThaiPA = itemCategory.Id;
                    //var linhVuc = await _service.Por_LinhVuc.GetByIdAsync(item.IDLinhVuc);
                    //item.MaPhanAnh = linhVuc.Ma.ToUpper() + StringHelpers.RandomString(5);
                    var itemsSave = await _service.Por_PhanAnh.GuiPhanAnh(item);
                    return ResponseMessage.Success(item.MaPhanAnh);
                }
                return ResponseMessage.Error(Message.SERVICE_ERROR);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Save : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("LayDsLinhVuc")]
        [AllowAnonymous]
        public async Task<IActionResult> LayDsLinhVuc()
        {
            try
            {
                _logger.LogInformation(string.Format("Call LayDsLinhVuc"));
                var treeOrgan = await _service.Por_LinhVuc.GetTreeListAsync();
                return ResponseMessage.Success(treeOrgan);                
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("LayDsLinhVuc : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    } 
}

