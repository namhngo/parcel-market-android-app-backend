using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gis.API.Infrastructure.Authorization;
using Gis.API.Model;
using Gis.API.Service;
using Gis.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gis.API.Controllers
{
    [Route("api/[controller]")]
    public class Por_LoaiVanBanPhapQuyController : ApiControllerBase<Por_LoaiVanBanPhapQuy>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_LoaiVanBanPhapQuyController> _logger;
        public Por_LoaiVanBanPhapQuyController(IServiceWrapper service, ILogger<Por_LoaiVanBanPhapQuyController> logger) : base(service, logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}

