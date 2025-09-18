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

namespace Gis.API.Controllers
{
    public class Por_BuocQuyTrinhController : ApiControllerBase<Por_BuocQuyTrinh>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_BuocQuyTrinhController> _logger;
        public Por_BuocQuyTrinhController(IServiceWrapper service, ILogger<Por_BuocQuyTrinhController> logger) :base(service, logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("DsBuocQuyTrinhTheoQuyTrinh")]
        [AuthorizeFilter]
        public async Task<IActionResult> DsBuocQuyTrinhTheoQuyTrinh(Guid Id)
        {
            try
            {
                _logger.LogInformation("Call DsQuyTrinh");
                var result = await _service.Por_BuocQuyTrinh.DsBuocQuyTrinhTheoQuyTrinh(Id);
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
