using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Gis.API.Infrastructure;
using Gis.API.Infrastructure.Authorization;
using Gis.API.Model;
using Gis.API.Service;
using Gis.API.ViewModel.PhanAnh;
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
    public class Por_ThuaDatController : ApiControllerBase<Por_ThuatDat>
    {
        // GET: /<controller>/
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_ThuaDatController> _logger;
        private readonly string _savePath;
        private readonly IUploadFileProvider _fileProvider;
        private readonly AppSettings appSettings;
        private readonly IConfiguration _rootConfiguration;
        public Por_ThuaDatController(IConfiguration rootConfiguration, IUploadFileProvider fileProvider, IServiceWrapper service, ILogger<Por_ThuaDatController> logger) : base(service, logger)
        {
            appSettings = new AppSettings();
            rootConfiguration.Bind(appSettings);
            _rootConfiguration = rootConfiguration;
            _service = service;
            _fileProvider = fileProvider;
            _logger = logger;
            _savePath = appSettings.FileServerConfiguration.SavePath;
        }
        [HttpGet("GetByHoSoId")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> GetByHoSoId(Guid hosoId)
        {
            try
            {
                _logger.LogInformation("Call GetByHoSoId");                
                var item = await _service.Por_ThuaDat.GetByHoSoId(hosoId);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetByHoSoId : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    } 
}

