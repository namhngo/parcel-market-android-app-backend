using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Gis.API.Infrastructure.Authorization;
using Gis.API.Service;
using Gis.API.Controllers;
using Gis.API.Model;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gis.API.ViewModel.Por_BuocQuyTrinh;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using Gis.Core.Interfaces;
using Gis.API.Infrastructure;
using Microsoft.Extensions.Configuration;
using static Gis.Core.Constant.Sys_Const;

namespace Gis.API.Controllers
{
    public class Por_FileMauThanhPhanHStrongQTController : ApiControllerBase<Por_FileMauThanhPhanHStrongQT>
    {
        private readonly IUploadFileProvider _fileProvider;
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_FileMauThanhPhanHStrongQTController> _logger;
        private readonly string _savePath;
        public Por_FileMauThanhPhanHStrongQTController(IConfiguration rootConfiguration, IUploadFileProvider fileProvider, IServiceWrapper service, ILogger<Por_FileMauThanhPhanHStrongQTController> logger) :base(service, logger)
        {
            AppSettings appSettings = new AppSettings();
            rootConfiguration.Bind(appSettings);
            _service = service;
            _fileProvider = fileProvider;
            _logger = logger;
            _savePath = appSettings.FileServerConfiguration.SavePath;
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
                    var items = JsonConvert.DeserializeObject<List<Por_FileMauThanhPhanHStrongQT>>(jsonData);
                    byte[] bytes = null;
                    foreach (var item in items)
                    {
                        foreach (var file in Request.Form.Files)
                        {
                            string FileName = file.ContentDisposition.Split("\"")[3];
                            string ContentDispositionName = file.ContentDisposition.Split("\"")[1];
                            if ("file_" + item.Id == ContentDispositionName)
                            {
                                string ContentType = file.ContentType;
                                if (file.Length > 0)
                                {
                                    bytes = new byte[file.Length];
                                    string savePath = Path.Combine(_fileProvider.BuildSavePathYYYYMMDD(Path.Combine(_savePath, "FileMauThanhPhanHStrongQT")), FileName);
                                    using (var stream = new FileStream(savePath, FileMode.Create))
                                    {
                                        file.CopyTo(stream);
                                    }
                                    item.TenFile = FileName;
                                    item.URL = savePath;
                                }
                            }
                        }
                    }
                    Por_FileMauThanhPhanHStrongQT[] itemsSave = null;
                    if (items.Count > 0)
                    {
                        itemsSave = await _service.Por_FileMauThanhPhanHStrongQT.SaveEntitiesAsync(items.ToArray());
                        await _service.Por_FileMauThanhPhanHStrongQT.XoaFile(items, itemsSave.ToList());
                    }
                    return ResponseMessage.Success(itemsSave);
                }
                return ResponseMessage.Error(Message.SERVICE_ERROR);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Save : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("DsFileMauThanhPhanHSTheoQuyTrinh")]
        [AuthorizeFilter]
        public async Task<IActionResult> DsFileMauThanhPhanHSTheoQuyTrinh(Guid Id)
        {
            try
            {
                _logger.LogInformation("Call DsQuyTrinh");
                var result = await _service.Por_FileMauThanhPhanHStrongQT.DsFileMauThanhPhanHSTheoQuyTrinh(Id);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("DsQuyTrinh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
