using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Gis.API.Infrastructure;
using Gis.API.Infrastructure.Authorization;
using Gis.API.Model;
using Gis.API.Service;
using Gis.API.ViewModel.Por_LogGis;
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
    public class Por_LogEmailSmsController
    {
        private readonly IUploadFileProvider _fileProvider;
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_LogEmailSmsController> _logger;
        private readonly string _savePath;
        public Por_LogEmailSmsController(IConfiguration rootConfiguration, IUploadFileProvider fileProvider, IServiceWrapper service, ILogger<Por_LogEmailSmsController> logger)
        {
            AppSettings appSettings = new AppSettings();
            rootConfiguration.Bind(appSettings);
            _service = service;
            _fileProvider = fileProvider;
            _logger = logger;
            _savePath = appSettings.FileServerConfiguration.SavePath;
        }
        [HttpPost("SearchDate")]
        [AuthorizeFilter]
        public async Task<IActionResult> SearchDate([FromBody] TimkiemLogGis gis)
        {
            try
            {
                var items = await _service.Por_LogSearch.SearchDateEmailSms(gis.toDate, gis.endDate);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("SearchDate : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}

