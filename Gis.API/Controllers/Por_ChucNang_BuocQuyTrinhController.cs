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
    public class Por_ChucNang_BuocQuyTrinhController : ApiControllerBase<Por_ChucNang_BuocQuyTrinh>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_QuyTrinhController> _logger;
        public Por_ChucNang_BuocQuyTrinhController(IServiceWrapper service, ILogger<Por_QuyTrinhController> logger) :base(service, logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("CheckDuplicateAttributes")]
        [AuthorizeFilter]
        public async Task<IActionResult> CheckDuplicateAttributes(Guid? id, string name)
        {
            try
            {
                _logger.LogInformation(string.Format("Call CheckDuplicateAttributes params: (id = {0}, name = {1})", id, name));
                var result = await _service.Por_ChucNang_BuocQuyTrinh.IsDupicateAttributesAsync(id, name);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("CheckDuplicateAttributes : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
