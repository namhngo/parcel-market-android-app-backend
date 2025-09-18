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

namespace Gis.API.Controllers
{
    public class Por_CauHoiThuongGapController : ApiControllerBase<Por_CauHoiThuongGap>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_CauHoiThuongGapController> _logger;
        public Por_CauHoiThuongGapController(IServiceWrapper service, ILogger<Por_CauHoiThuongGapController> logger) :base(service, logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("LayDSCauHoiThuongGap")]
        [AllowAnonymous]
        public async Task<IActionResult> LayDSCauHoiThuongGap()
        {
            try
            {
                _logger.LogInformation(string.Format("Call LayDSCauHoiThuongGap"));
                var items = await _service.Por_CauHoiThuongGap.LayDSCauHoiThuongGap();
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("LayDSCauHoiThuongGap : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
