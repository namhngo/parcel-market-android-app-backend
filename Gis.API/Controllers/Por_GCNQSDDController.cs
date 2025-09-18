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
using Microsoft.AspNetCore.Http;
using ExcelDataReader;
using static Gis.Core.Constant.Sys_Const;
using Gis.API.ViewModel.Por_GCNQSDD;

namespace Gis.API.Controllers
{
    public class Por_GCNQSDDController : ApiControllerBase<Por_GCNQSDD>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_GCNQSDDController> _logger;
        public Por_GCNQSDDController(IServiceWrapper service, ILogger<Por_GCNQSDDController> logger) :base(service, logger)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet("CheckDuplicateAttributes")]
        [AuthorizeFilter]
        public async Task<IActionResult> CheckDuplicateAttributes(Guid? id, string soto, string sothua)
        {
            try
            {
                _logger.LogInformation(string.Format("Call CheckDuplicateAttributes params: (id = {0}, soto = {1}, sothua = {2})", id, soto, sothua));
                var result = await _service.Por_GCNQSDD.IsDupicateAttributesAsync(id, soto, sothua);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("CheckDuplicateAttributes : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("FileTemplate")]
        [AuthorizeFilter]
        public async Task<IActionResult> FileTemplate()
        {
            try
            {
                return ResponseMessage.Success("StaticFiles/Template/Template_GCNQSDD.xlsx");
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("FileTemplate : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("Search")]
        [AuthorizeFilter]
        public async Task<IActionResult> Search([FromBody] TimKiem model)
        {
            try
            {
                var items = await _service.Por_GCNQSDD.Search(model.SoHieu, model.CCCD, model.SoThua, model.SoTo, model.TenPhuongXa,model.NguoiSuDung);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Search : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("SearchMaPx")]
        [AuthorizeFilter]
        public async Task<IActionResult> SearchMaPx([FromBody] TimKiem model)
        {
            try
            {
                var items = await _service.Por_GCNQSDD.SearchMaPx(model.SoHieu, model.CCCD, model.SoThua, model.SoTo, model.MaPx);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Search : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("Import")]
        [AllowAnonymous]
        public async Task<IActionResult> Import(IFormFile uploadedFile)
        {
            try
            {
                var items = new List<Por_GCNQSDD>();
                foreach (var file in Request.Form.Files)
                {
                    using var fileStream = file.OpenReadStream();
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    using (var reader = ExcelReaderFactory.CreateReader(fileStream))
                    {
                        var dataSet = reader.AsDataSet();
                        for(var i = 0;i < dataSet.Tables.Count;i++)
                        {
                            var dataTable = dataSet.Tables[i];
                            for(var j = 0;j < dataTable.Rows.Count;j++)
                            {
                                if(j >= 2)
                                {                                    
                                    var item = new Por_GCNQSDD();
                                    item.SoHieu = Convert.ToString(dataTable.Rows[j]["Column1"]);
                                    item.NgayCap = Convert.ToString(dataTable.Rows[j]["Column2"]);
                                    item.NguoiSuDung = Convert.ToString(dataTable.Rows[j]["Column3"]);
                                    item.DiaChiThuongTru = Convert.ToString(dataTable.Rows[j]["Column4"]);
                                    item.CCCD = Convert.ToString(dataTable.Rows[j]["Column5"]);
                                    item.NguoiKy = Convert.ToString(dataTable.Rows[j]["Column6"]);
                                    item.SoTo = Convert.ToString(dataTable.Rows[j]["Column7"]);
                                    item.SoThua = Convert.ToString(dataTable.Rows[j]["Column8"]);
                                    item.DiaChiThuaDat = Convert.ToString(dataTable.Rows[j]["Column9"]);
                                    item.MaPX = Convert.ToString(dataTable.Rows[j]["Column10"]);
                                    item.TenPhuongXa = Convert.ToString(dataTable.Rows[j]["Column11"]);
                                    item.DienTich = Convert.ToString(dataTable.Rows[j]["Column12"]);
                                    item.MucDichSuDung = Convert.ToString(dataTable.Rows[j]["Column13"]);
                                    item.ThoiHanSuDung = Convert.ToString(dataTable.Rows[j]["Column14"]);
                                    item.NhaO = Convert.ToString(dataTable.Rows[j]["Column15"]);
                                    item.CongTrinh = Convert.ToString(dataTable.Rows[j]["Column16"]);
                                    item.RungSanXuat = Convert.ToString(dataTable.Rows[j]["Column17"]);
                                    item.CayLauNam = Convert.ToString(dataTable.Rows[j]["Column18"]);
                                    items.Add(item);
                                }    
                            }    
                        }    
                    }
                }
                if(items.Count > 0)
                {
                    await _service.Por_GCNQSDD.SaveEntitiesAsync(items.ToArray());
                    return ResponseMessage.Success();
                }
                return ResponseMessage.Error(Message.SERVICE_ERROR);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Import : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
