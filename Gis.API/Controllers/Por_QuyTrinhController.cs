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
    public class Por_QuyTrinhController : ApiControllerBase<Por_QuyTrinh>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_QuyTrinhController> _logger;
        public Por_QuyTrinhController(IServiceWrapper service, ILogger<Por_QuyTrinhController> logger) :base(service, logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("CheckDuplicateAttributes")]
        [AuthorizeFilter]
        public async Task<IActionResult> CheckDuplicateAttributes(Guid? Id, string TenThuTuc)
        {
            try
            {
                _logger.LogInformation(string.Format("Call CheckDuplicateAttributes params: (id = {0}, name = {1})", Id, TenThuTuc));
                var result = await _service.Por_QuyTrinh.IsDupicateAttributesAsync(Id, TenThuTuc);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("CheckDuplicateAttributes : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("DsQuyTrinh")]
        [AuthorizeFilter]
        public async Task<IActionResult> DsQuyTrinh()
        {
            try
            {
                _logger.LogInformation("Call DsQuyTrinh");
                var result = await _service.Por_QuyTrinh.DsQuyTrinh();
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("DsQuyTrinh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("DsQuyTrinhHoatDong")]
        [AuthorizeFilter]
        public async Task<IActionResult> DsQuyTrinhHoatDong()
        {
            try
            {
                _logger.LogInformation("Call DsQuyTrinhHoatDong");
                var result = await _service.Por_QuyTrinh.DsQuyTrinhHoatDong();
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("DsQuyTrinhHoatDong : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("DsQuyTrinhTheoLinhVuc")]
        [AuthorizeFilter]
        public async Task<IActionResult> DsQuyTrinhTheoLinhVuc(Guid Id)
        {
            try
            {
                _logger.LogInformation("Call DsQuyTrinhTheoLinhVuc");
                var result = await _service.Por_QuyTrinh.DsQuyTrinhTheoLinhVuc(Id);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("DsQuyTrinhTheoLinhVuc : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpDelete("XoaQuyTrinh")]
        [AuthorizeFilter]
        public async Task<IActionResult> XoaQuyTrinh(Guid Id)
        {
            try
            {
                _logger.LogInformation("Call XoaQuyTrinh");
                await _service.Por_QuyTrinh.XoaQuyTrinh(Id);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("XoaQuyTrinh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
