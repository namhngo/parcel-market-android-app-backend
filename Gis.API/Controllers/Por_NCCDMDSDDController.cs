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
using Gis.API.ViewModel.Por_NCCDMDSDD;

namespace Gis.API.Controllers
{
    public class Por_NCCDMDSDDController : ApiControllerBase<Por_NCCDMDSDD>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Por_NCCDMDSDDController> _logger;
        public Por_NCCDMDSDDController(IServiceWrapper service, ILogger<Por_NCCDMDSDDController> logger) :base(service, logger)
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
                var result = await _service.Por_NCCDMDSDD.IsDupicateAttributesAsync(id, soto, sothua);
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
                return ResponseMessage.Success("StaticFiles/Template/Template_NCCDMDSDD.xlsx");
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
                var items = await _service.Por_NCCDMDSDD.Search( model.SoThua, model.SoTo, model.TenPhuongXa);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Search : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("SearchMaPX")]
        [AuthorizeFilter]
        public async Task<IActionResult> SearchMaPX([FromBody] TimKiem model)
        {
            try
            {
                var items = await _service.Por_NCCDMDSDD.SearchMaPX(model.SoThua, model.SoTo, model.MaPX);
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
                var items = new List<Por_NCCDMDSDD>();
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
                                if (j >= 2)
                                {
                                    var item = new Por_NCCDMDSDD();
                                    item.MaHS = Convert.ToString(dataTable.Rows[j]["Column1"]);
                                    item.TenChuSuDung = Convert.ToString(dataTable.Rows[j]["Column2"]);
                                    item.DiaChiThuongTru = Convert.ToString(dataTable.Rows[j]["Column3"]);
                                    item.DiaDiem = Convert.ToString(dataTable.Rows[j]["Column4"]);
                                    item.MaPX = Convert.ToString(dataTable.Rows[j]["Column5"]);
                                    item.DiaChiThuaDat = Convert.ToString(dataTable.Rows[j]["Column6"]);
                                    item.SoTo = Convert.ToString(dataTable.Rows[j]["Column7"]);
                                    item.SoThua = Convert.ToString(dataTable.Rows[j]["Column8"]);
                                    item.DienTich = Convert.ToString(dataTable.Rows[j]["Column9"]);
                                    item.LoaiDatHienTrangTheoGCN = Convert.ToString(dataTable.Rows[j]["Column10"]);
                                    item.NhuCauChuyenMucDich = Convert.ToString(dataTable.Rows[j]["Column11"]);
                                    item.GCN = Convert.ToString(dataTable.Rows[j]["Column12"]);
                                    item.ThongTinQuyHoach = Convert.ToString(dataTable.Rows[j]["Column13"]);
                                    item.GhiChu = Convert.ToString(dataTable.Rows[j]["Column14"]);
                                    item.SoDienThoai = Convert.ToString(dataTable.Rows[j]["Column15"]);
                                    item.TenPhuongXa = Convert.ToString(dataTable.Rows[j]["Column16"]);
                                    items.Add(item);
                                }
                            }
                        }
                    }
                }
                if (items.Count > 0)
                {
                    await _service.Por_NCCDMDSDD.SaveEntitiesAsync(items.ToArray());
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
