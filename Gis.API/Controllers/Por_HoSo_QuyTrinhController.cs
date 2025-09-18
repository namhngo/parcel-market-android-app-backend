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
    public class Por_HoSo_QuyTrinhController : ApiControllerBase<Por_HoSo_QuyTrinh>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_HoSo_QuyTrinhController> _logger;
        public Por_HoSo_QuyTrinhController(IServiceWrapper service, ILogger<Por_HoSo_QuyTrinhController> logger) :base(service, logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}
