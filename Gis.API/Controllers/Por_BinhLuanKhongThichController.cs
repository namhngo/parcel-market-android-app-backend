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
    public class Por_BinhLuanKhongThichController : ApiControllerBase<Por_BinhLuanKhongThich>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_BinhLuanKhongThichController> _logger;
        public Por_BinhLuanKhongThichController(IServiceWrapper service, ILogger<Por_BinhLuanKhongThichController> logger) :base(service, logger)
        {
            _service = service;
            _logger = logger;
        }

    }
}
