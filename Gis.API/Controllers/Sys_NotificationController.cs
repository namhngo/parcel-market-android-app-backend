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
    public class Sys_NotificationController : ApiControllerBase<Sys_Notification>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Sys_NotificationController> _logger;
        public Sys_NotificationController(IServiceWrapper service, ILogger<Sys_NotificationController> logger) :base(service, logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}
