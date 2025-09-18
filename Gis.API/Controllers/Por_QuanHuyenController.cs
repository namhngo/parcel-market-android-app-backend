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
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using static Gis.Core.Constant.Sys_Const;

namespace Gis.API.Controllers
{
    public class Por_QuanHuyenController : ApiControllerBase<Por_QuanHuyen>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_QuanHuyenController> _logger;
        public Por_QuanHuyenController(IServiceWrapper service, ILogger<Por_QuanHuyenController> logger) :base(service, logger)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet("GetQuanHuyenByMaQH")]
        [AllowAnonymous]
        public virtual IActionResult GetQuanHuyenByMaQH(string MaQH)
        {
            try
            {
                _logger.LogInformation("Call GetQuanHuyenByMaQH");
                var items = _service.Por_QuanHuyen.GetQuanHuyenByMaQH(MaQH);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetQuanHuyenByMaQH : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("GetQuanHuyenByMaTP")]
        [AllowAnonymous]
        public virtual IActionResult GetQuanHuyenByMaTP(string MaTP)
        {
            try
            {
                _logger.LogInformation("Call GetQuanHuyenByMaTP");
                var items = _service.Por_QuanHuyen.GetQuanHuyen(MaTP);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetQuanHuyenByMaTP : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("GetQuanHuyenByIDTP")]
        [AllowAnonymous]
        public virtual IActionResult GetQuanHuyenByIDTP(Guid IDTP)
        {
            try
            {
                _logger.LogInformation("Call GetQuanHuyenByIDTP");
                var items = _service.Por_QuanHuyen.GetQuanHuyen(IDTP);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetQuanHuyenByIDTP : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("Import")]
        [AllowAnonymous]
        public async Task<IActionResult> Import(IFormFile uploadedFile)
        {
            try
            {
                var items = new List<Por_QuanHuyen>();
                foreach (var file in Request.Form.Files)
                {
                    using var fileStream = file.OpenReadStream();
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    using (var reader = ExcelReaderFactory.CreateReader(fileStream))
                    {
                        var dataSet = reader.AsDataSet();
                        for (var i = 0; i < dataSet.Tables.Count; i++)
                        {
                            var dataTable = dataSet.Tables[i];
                            for (var j = 0; j < dataTable.Rows.Count; j++)
                            {
                                if (j >= 1)
                                {
                                    var item = new Por_QuanHuyen();
                                    item.Ma = Convert.ToString(dataTable.Rows[j]["Column0"]);
                                    item.Ten = Convert.ToString(dataTable.Rows[j]["Column1"]);
                                    item.TenTiengAnh = Convert.ToString(dataTable.Rows[j]["Column2"]);
                                    item.Cap = Convert.ToString(dataTable.Rows[j]["Column3"]);
                                    item.MaTP = Convert.ToString(dataTable.Rows[j]["Column4"]);
                                    item.TinhThanhPho = Convert.ToString(dataTable.Rows[j]["Column5"]);
                                    items.Add(item);
                                }
                            }
                        }
                    }
                }
                if (items.Count > 0)
                {
                    await _service.Por_QuanHuyen.SaveEntitiesAsync(items.ToArray());
                    return ResponseMessage.Success();
                }
                return ResponseMessage.Error(Message.SERVICE_ERROR);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetByProps : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
