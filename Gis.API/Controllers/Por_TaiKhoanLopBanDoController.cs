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
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using static Gis.Core.Constant.Sys_Const;
using Gis.API.ViewModel.Por_GisSoNha;

namespace Gis.API.Controllers
{
    public class Por_TaiKhoanLopBanDoController : ApiControllerBase<Por_TaiKhoanLopBanDo>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_TaiKhoanLopBanDoController> _logger;
        public Por_TaiKhoanLopBanDoController(IServiceWrapper service, ILogger<Por_TaiKhoanLopBanDoController> logger) :base(service, logger)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet("DeleteByIdLopBanDo/{Id}")]
        [AuthorizeFilter]
        public async Task<IActionResult> DeleteByIdLopBanDo(Guid Id)
        {
            try
            {
                _logger.LogInformation(string.Format("Call DeleteByIdLopBanDo params: (id = {0})", Id));
                await _service.Por_TaiKhoanLopBanDo.DeleteByIdLopBanDo(Id);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("DeleteById : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
