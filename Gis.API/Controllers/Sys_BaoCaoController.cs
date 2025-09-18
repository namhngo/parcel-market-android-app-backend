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
using System.IO;
using OfficeOpenXml.Table;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Gis.API.Controllers
{
    [ApiController]
    [AuthorizeFilter]
    [Route("api/[controller]")]
    public class Sys_BaoCaoController
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Sys_BaoCaoController> _logger;
        public Sys_BaoCaoController(IServiceWrapper service, ILogger<Sys_BaoCaoController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost("BaoCaoHoSoTheoTungLoai/{totalLimitItems}")]
        [AuthorizeFilter]
        public async Task<IActionResult> BaoCaoHoSoTheoTungLoai([FromBody] ViewModel.BaoCao.SearchHoSoTheoTungLoai search, int? totalLimitItems)
        {
            try
            {
                _logger.LogInformation(string.Format("Call BaoCaoHoSoTheoTungLoai"));
                var items = await _service.Por_HoSo.BaoCaoHoSoTheoTungLoai(search, totalLimitItems);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("BaoCaoHoSoTheoTungLoai : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        private ExcelPackage createExcelPackageHoSoTheoTungLoai( List<ViewModel.BaoCao.HoSoTheoTungLoai> items, string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            var package = new ExcelPackage(fileInfo);
            var worksheet = package.Workbook.Worksheets[1];

            var index = 2;
            foreach(var item in items)
            {
                worksheet.Cells[index, 1].Value = item.MaHoSo;
                worksheet.Cells[index, 2].Value = item.LoaiDichVu;
                worksheet.Cells[index, 3].Value = item.NguoiDan;
                worksheet.Cells[index, 4].Value = item.SoDienThoai;
                worksheet.Cells[index, 5].Value = item.TrangThaiHoSo;
                worksheet.Cells[index, 6].Value = item.NgayNop;
                //
                #region style
                worksheet.Cells[index, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[index, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[index, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[index, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[index, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[index, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                #endregion
                index++;
            }

            return package;
        }
        [HttpPost("XuatBaoCaoHoSoTheoTungLoai/{totalLimitItems}")]
        [AuthorizeFilter]
        public async Task<IActionResult> XuatBaoCaoHoSoTheoTungLoai([FromBody] ViewModel.BaoCao.SearchHoSoTheoTungLoai search, int? totalLimitItems)
        {
            try
            {
                string path = "StaticFiles/TemplateOutput/Template_BaoCaoHoSoTheoTungLoai" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";
                var fileInfo = new FileInfo(path);
                if (!fileInfo.Exists)
                {
                    var items = await _service.Por_HoSo.BaoCaoHoSoTheoTungLoai(search, totalLimitItems);
                    using (var package = createExcelPackageHoSoTheoTungLoai(items, "StaticFiles/Template/Template_BaoCaoHoSoTheoTungLoai.xlsx"))
                    {
                        package.SaveAs(fileInfo);
                    }
                }
                return ResponseMessage.Success(path);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("XuatBaoCaoHoSoTheoTungLoai : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPost("BaoCaoThuaDatDuocTimKiem/{totalLimitItems}")]
        [AuthorizeFilter]
        public async Task<IActionResult> BaoCaoThuaDatDuocTimKiem([FromBody] ViewModel.BaoCao.SearchThuaDatDuocTimKiem search, int? totalLimitItems)
        {
            try
            {
                _logger.LogInformation(string.Format("Call BaoCaoThuaDatDuocTimKiem"));
                var items = await _service.Por_HoSo.BaoCaoThuaDatDuocTimKiem(search, totalLimitItems);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("BaoCaoThuaDatDuocTimKiem : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        private ExcelPackage createExcelPackageThuaDatDuocTimKiem(List<ViewModel.BaoCao.ThuaDatDuocTimKiem> items, string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            var package = new ExcelPackage(fileInfo);
            var worksheet = package.Workbook.Worksheets[1];

            var index = 2;
            foreach (var item in items)
            {
                worksheet.Cells[index, 1].Value = item.PhuongXa;
                worksheet.Cells[index, 2].Value = item.SoTo;
                worksheet.Cells[index, 3].Value = item.SoThua;
                worksheet.Cells[index, 4].Value = item.LuotTimKiem;
                //
                #region style
                worksheet.Cells[index, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[index, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[index, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[index, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                #endregion
                index++;
            }

            return package;
        }
        [HttpPost("XuatBaoCaoThuaDatDuocTimKiem/{totalLimitItems}")]
        [AuthorizeFilter]
        public async Task<IActionResult> XuatBaoCaoThuaDatDuocTimKiem([FromBody] ViewModel.BaoCao.SearchThuaDatDuocTimKiem search, int? totalLimitItems)
        {
            try
            {
                string path = "StaticFiles/TemplateOutput/Template_BaoCaoThuaDatDuocTimKiem" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";
                var fileInfo = new FileInfo(path);
                if (!fileInfo.Exists)
                {
                    var items = await _service.Por_HoSo.BaoCaoThuaDatDuocTimKiem(search, totalLimitItems);
                    using (var package = createExcelPackageThuaDatDuocTimKiem(items, "StaticFiles/Template/Template_BaoCaoThuaDatDuocTimKiem.xlsx"))
                    {
                        package.SaveAs(fileInfo);
                    }
                }
                return ResponseMessage.Success(path);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("XuatBaoCaoThuaDatDuocTimKiem : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPost("BaoCaoTiepNhanHoSo/{totalLimitItems}")]
        [AuthorizeFilter]
        public async Task<IActionResult> BaoCaoTiepNhanHoSo([FromBody] ViewModel.BaoCao.SearchTiepNhanHoSo search, int? totalLimitItems)
        {
            try
            {
                _logger.LogInformation(string.Format("Call BaoCaoTiepNhanHoSo"));
                var items = await _service.Por_HoSo.BaoCaoTiepNhanHoSo(search, totalLimitItems);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("BaoCaoTiepNhanHoSo : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        private ExcelPackage createExcelPackageTiepNhanHoSo(List<ViewModel.BaoCao.TiepNhanHoSo> items, string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            var package = new ExcelPackage(fileInfo);
            var worksheet = package.Workbook.Worksheets[1];

            var index = 2;
            foreach (var item in items)
            {
                worksheet.Cells[index, 1].Value = item.MaHoSo;
                worksheet.Cells[index, 2].Value = item.LoaiDichVu;
                worksheet.Cells[index, 3].Value = item.NguoiDan;
                worksheet.Cells[index, 4].Value = item.SoDienThoai;
                worksheet.Cells[index, 5].Value = item.TrangThaiHoSo;
                worksheet.Cells[index, 6].Value = item.NgayNop;
                worksheet.Cells[index, 7].Value = item.NgayTiepNhan;
                worksheet.Cells[index, 8].Value = item.NgayTraKetQua;
                //
                #region style
                worksheet.Cells[index, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[index, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[index, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[index, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[index, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[index, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[index, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[index, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[index, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                #endregion
                index++;
            }

            return package;
        }
        [HttpPost("XuatBaoCaoTiepNhanHoSo/{totalLimitItems}")]
        [AuthorizeFilter]
        public async Task<IActionResult> XuatBaoCaoTiepNhanHoSo([FromBody] ViewModel.BaoCao.SearchTiepNhanHoSo search, int? totalLimitItems)
        {
            try
            {
                string path = "StaticFiles/TemplateOutput/Template_BaoCaoTiepNhanHoSo" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";
                var fileInfo = new FileInfo(path);
                if (!fileInfo.Exists)
                {
                    var items = await _service.Por_HoSo.BaoCaoTiepNhanHoSo(search, totalLimitItems);
                    using (var package = createExcelPackageTiepNhanHoSo(items, "StaticFiles/Template/Template_BaoCaoTiepNhanHoSo.xlsx"))
                    {
                        package.SaveAs(fileInfo);
                    }
                }
                return ResponseMessage.Success(path);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("XuatBaoCaoTiepNhanHoSo : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
