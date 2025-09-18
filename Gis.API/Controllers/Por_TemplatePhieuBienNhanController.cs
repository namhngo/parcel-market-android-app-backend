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
    public class Por_TemplatePhieuBienNhanController : ApiControllerBase<Por_TemplatePhieuBienNhan>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_TemplatePhieuBienNhanController> _logger;
        public Por_TemplatePhieuBienNhanController(IServiceWrapper service, ILogger<Por_TemplatePhieuBienNhanController> logger) :base(service, logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("CheckDuplicateAttributes")]
        [AuthorizeFilter]
        public async Task<IActionResult> CheckDuplicateAttributes(Guid? id, string ma)
        {
            try
            {
                _logger.LogInformation(string.Format("Call CheckDuplicateAttributes params: (id = {0}, ma = {1})", id, ma));
                var result = await _service.Por_TemplateSms.IsDupicateAttributesAsync(id, ma);
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
