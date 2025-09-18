using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Gis.API.Infrastructure.Authentication;
using Gis.Core.Interfaces;
using Gis.API.Service;
using Gis.Core.Models;
using Gis.Core.Constant;
using System.Security.Claims;
using Gis.Core.Helpers;
using Gis.Core.Core;
using Microsoft.Extensions.Configuration;
using Gis.API.Infrastructure.Authorization;
using Gis.API.Model;
using Newtonsoft.Json;

namespace Gis.API.Controllers
{
    [ApiController]
    [AuthorizeFilter]
    [Route("api/[controller]")]
    public class Sys_DashboardController : ControllerBase
    {                           
        private readonly IServiceWrapper _service;
        private readonly ILogger<Sys_DashboardController> _logger;
        public Sys_DashboardController(IServiceWrapper service, ILogger<Sys_DashboardController> logger)
        {                                            
            _service = service;
            _logger = logger;
        }
        [HttpGet("HoSoDashboard")]
        [AllowAnonymous]
        public async Task<IActionResult> HoSoDashboard()
        {
            try
            {
                _logger.LogInformation(string.Format("Call HoSoDashboard"));
                var items = await _service.Por_HoSo.HoSoDashboard();
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("HoSoDashboard : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
