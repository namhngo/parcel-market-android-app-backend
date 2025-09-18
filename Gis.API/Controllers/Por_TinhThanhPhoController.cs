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
    public class Por_TinhThanhPhoController : ApiControllerBase<Por_TinhThanhPho>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_TinhThanhPhoController> _logger;
        public Por_TinhThanhPhoController(IServiceWrapper service, ILogger<Por_TinhThanhPhoController> logger) :base(service, logger)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet("GetTinhThanhPhoByMaTP")]
        [AllowAnonymous]
        public virtual IActionResult GetTinhThanhPhoByMaTP(string MaTP)
        {
            try
            {
                _logger.LogInformation("Call GetTinhThanhPhoByMaTP");
                var items = _service.Por_TinhThanhPho.GetTinhThanhPhoByMaTP(MaTP);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetTinhThanhPhoByMaTP : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("GetTinhThanhPho")]
        [AllowAnonymous]
        public virtual IActionResult GetTinhThanhPho()
        {
            try
            {
                _logger.LogInformation("Call GetTinhThanhPho");
                var items = _service.Por_TinhThanhPho.GetTinhThanhPho();
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetTinhThanhPho : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("Import")]
        [AllowAnonymous]
        public async Task<IActionResult> Import(IFormFile uploadedFile)
        {
            try
            {
                var items = new List<Por_TinhThanhPho>();
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
                                    var item = new Por_TinhThanhPho();
                                    item.Ma = Convert.ToString(dataTable.Rows[j]["Column0"]);
                                    item.Ten = Convert.ToString(dataTable.Rows[j]["Column1"]);
                                    item.TenTiengAnh = Convert.ToString(dataTable.Rows[j]["Column2"]);
                                    item.Cap = Convert.ToString(dataTable.Rows[j]["Column3"]);
                                    items.Add(item);
                                }
                            }
                        }
                    }
                }
                if (items.Count > 0)
                {
                    await _service.Por_TinhThanhPho.SaveEntitiesAsync(items.ToArray());
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
