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
    public class Por_PhuongXaThiTranController : ApiControllerBase<Por_PhuongXaThiTran>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_PhuongXaThiTranController> _logger;
        public Por_PhuongXaThiTranController(IServiceWrapper service, ILogger<Por_PhuongXaThiTranController> logger) :base(service, logger)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet("GetPhuongXaThiTranByMaPX")]
        [AllowAnonymous]
        public virtual IActionResult GetPhuongXaThiTranByMaPX(string MaPX)
        {
            try
            {
                _logger.LogInformation("Call GetPhuongXaThiTranByMaPX");
                var items = _service.Por_PhuongXaThiTran.GetPhuongXaThiTranByMaPX(MaPX);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetPhuongXaThiTranByMaPX : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("GetPhuongXaThiTranByMaQH")]
        [AllowAnonymous]
        public virtual IActionResult GetPhuongXaThiTranByMaQH(string MaQH)
        {
            try
            {
                _logger.LogInformation("Call GetPhuongXaThiTranByMaQH");
                var items = _service.Por_PhuongXaThiTran.GetPhuongXaThiTran(MaQH);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetPhuongXaThiTranByMaQH : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("GetPhuongXaThiTranByIDQH")]
        [AllowAnonymous]
        public virtual IActionResult GetPhuongXaThiTranByIDQH(Guid IDQH)
        {
            try
            {
                _logger.LogInformation("Call GetPhuongXaThiTranByIDQH");
                var items = _service.Por_PhuongXaThiTran.GetPhuongXaThiTran(IDQH);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetPhuongXaThiTranByIDQH : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("Import")]
        [AllowAnonymous]
        public async Task<IActionResult> Import(IFormFile uploadedFile)
        {
            try
            {
                var items = new List<Por_PhuongXaThiTran>();
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
                                    var item = new Por_PhuongXaThiTran();
                                    item.Ma = Convert.ToString(dataTable.Rows[j]["Column0"]);
                                    item.Ten = Convert.ToString(dataTable.Rows[j]["Column1"]);
                                    item.TenTiengAnh = Convert.ToString(dataTable.Rows[j]["Column2"]);
                                    item.Cap = Convert.ToString(dataTable.Rows[j]["Column3"]);
                                    item.MaQH = Convert.ToString(dataTable.Rows[j]["Column4"]);
                                    item.QuanHuyen = Convert.ToString(dataTable.Rows[j]["Column5"]);
                                    item.MaTP = Convert.ToString(dataTable.Rows[j]["Column6"]);
                                    item.TinhThanhPho = Convert.ToString(dataTable.Rows[j]["Column7"]);
                                    items.Add(item);
                                }
                            }
                        }
                    }
                }
                if (items.Count > 0)
                {
                    await _service.Por_PhuongXaThiTran.SaveEntitiesAsync(items.ToArray());
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
