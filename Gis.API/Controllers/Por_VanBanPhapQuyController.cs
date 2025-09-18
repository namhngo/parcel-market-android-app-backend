using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Gis.API.Infrastructure;
using Gis.API.Infrastructure.Authorization;
using Gis.API.Model;
using Gis.API.Service;
using Gis.Core.Interfaces;
using Gis.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static Gis.Core.Constant.Sys_Const;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gis.API.Controllers
{
    [Route("api/[controller]")]
    public class Por_VanBanPhapQuyController : ApiControllerBase<Por_VanBanPhapQuy>
    {
        private readonly IUploadFileProvider _fileProvider;
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_VanBanPhapQuyController> _logger;
        private readonly string _savePath;
        public Por_VanBanPhapQuyController(IConfiguration rootConfiguration, IUploadFileProvider fileProvider, IServiceWrapper service, ILogger<Por_VanBanPhapQuyController> logger) : base(service, logger)
        {
            AppSettings appSettings = new AppSettings();
            rootConfiguration.Bind(appSettings);
            _service = service;
            _fileProvider = fileProvider;
            _logger = logger;
            _savePath = appSettings.FileServerConfiguration.SavePath;
        }
        [HttpGet("GetItemsByLoaiVanBanId/{page}/{pageSize}/{totalLimitItems}/{loaiVanBanId}")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> GetItemsByLoaiVanBanId(int page = 1, int pageSize = 10, int totalLimitItems = 500, string loaiVanBanId = "")
        {
            try
            {
                _logger.LogInformation(string.Format("Call GetListPaged params: (page = {0}, pageSize = {1}, totalLimitItems = {2})", page, pageSize, totalLimitItems));
                var items = await _service.Por_VanBan.GetItemsByLoaiVanBanId(page, pageSize, totalLimitItems, loaiVanBanId);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetListPaged : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPost("SaveMultipleAndUploadFile")]
        [AuthorizeFilter]
        public async Task<IActionResult> SaveMultipleAndUploadFile()
        {
            try
            {
                _logger.LogInformation("Call Save multiple");
                if (Request.Form.TryGetValue("data", out var jsonData))
                {
                    var item = JsonConvert.DeserializeObject<Por_VanBanPhapQuy>(jsonData);
                    byte[] bytes = null;
                    foreach (var file in Request.Form.Files)
                    {
                        string FileName = file.ContentDisposition.Split("\"")[3];
                        string ContentDispositionName = file.ContentDisposition.Split("\"")[1];
                        string ContentType = file.ContentType;
                        if (file.Length > 0)
                        {
                            bytes = new byte[file.Length];
                            string savePath = Path.Combine(_fileProvider.BuildSavePathYYYYMMDD(Path.Combine(_savePath, "VanBanPhapQuy")), FileName);
                            using (var stream = new FileStream(savePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            item.TenFile = FileName;
                            item.URL = savePath;
                        }
                    }
                    var itemSave = await _service.Por_VanBan.SaveEntityAsync(item);
                    return ResponseMessage.Success(itemSave);
                }
                return ResponseMessage.Error(Message.SERVICE_ERROR);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Save : {0}", ex.Message));
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
        [HttpGet("GetDSVanBan")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDSVanBan(Guid idLoaiVanBan, string search)
        {
            try
            {
                _logger.LogInformation(string.Format("Call GetDSVanBan"));
                var items = await _service.Por_VanBan.GetDSVanBan(idLoaiVanBan, search);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetDSPhanAnh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpGet("GetChiTietVanBan")]
        [AllowAnonymous]
        public async Task<IActionResult> GetChiTietVanBan(Guid Id)
        {
            try
            {
                _logger.LogInformation(string.Format("Call GetChiTietVanBan"));
                var items = await _service.Por_VanBan.GetChiTietVanBan(Id);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetChiTietVanBan : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("GetInformationVanBanPortal")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVanBanPortal()
        {
            try
            {
                _logger.LogInformation(string.Format("Call GetInformationVanBanPortal"));
                var items = await _service.Por_VanBan.GetVanBanPortal();
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetInformationVanBanPortal : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("GetInformationVanBanAdmin")]
        [AuthorizeFilter]
        public async Task<IActionResult> GetVanBanAdmin()
        {
            try
            {
                _logger.LogInformation(string.Format("Call GetInformationVanBanAdmin"));
                var items = await _service.Por_VanBan.GetVanBanAdmin();
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetInformationVanBanAdmin : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpGet("GetDsLoaiVanBan")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLoaiVanBanAdmin()
        {
            try
            {
                _logger.LogInformation(string.Format("Call GetDsLoaiVanBan"));
                var items = await _service.Por_VanBan.GetDsLoaiVanBan();
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetLoaiVanBanAdmin : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPost("AddLoaiVanBan")]
        [AuthorizeFilter]
        public async Task<IActionResult> AddLoaiVanBan(Por_LoaiVanBanPhapQuy lvb)
        {

            try
            {
                _logger.LogInformation(string.Format("Call GetInformationVanBanAdmin"));
                await _service.Por_VanBan.AddLoaiVanBanAdmin(lvb);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetInformationVanBanAdmin : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("AddVanBan")]
        [AuthorizeFilter]
        public async Task<IActionResult> AddVanBan(Por_VanBanPhapQuy vb)
        {

            try
            {
                _logger.LogInformation(string.Format("Call GetInformationVanBanAdmin"));
                await _service.Por_VanBan.AddVanBanAdmin(vb);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetInformationVanBanAdmin : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpDelete("DeleteVB/{Id}")]
        [AuthorizeFilter]
        public async Task<IActionResult> DeleteVanBanById(Guid Id)
        {
            try
            {
                _logger.LogInformation(string.Format("Call DeleteVanBanById"));
                await _service.Por_VanBan.DeleteVanBanById(Id);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("DeleteVanBanById : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpDelete("DeleteLoaiVB/{Id}")]
        [AuthorizeFilter]
        public async Task<IActionResult> DeleteLoaiVanBanById(Guid Id)
        {
            try
            {
                _logger.LogInformation(string.Format("Call DeleteLoaiVanBanById"));
                await _service.Por_VanBan.DeleteLoaiVanBanById(Id);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("DeleteLoaiVanBanById : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}

