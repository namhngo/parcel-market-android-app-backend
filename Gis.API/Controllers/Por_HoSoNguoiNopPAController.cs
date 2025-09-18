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
using Gis.API.ViewModel.HoSoQuyTrinh;
using Gis.Core.Interfaces;
using Newtonsoft.Json;
using System.IO;
using Gis.API.Infrastructure;
using Microsoft.Extensions.Configuration;
using Gis.Core.Enumeration;

namespace Gis.API.Controllers
{
    public class Por_HoSoNguoiNopPAController : ApiControllerBase<Por_GopYPhanAnh>
    {
        private readonly IServiceWrapper _service;
        private readonly IUserProvider _userProvider;
        private readonly ILogger<Por_HoSoNguoiNopPAController> _logger;
        private readonly IUploadFileProvider _fileProvider;
        private readonly string _savePath;
        private readonly AppSettings appSettings;
        public Por_HoSoNguoiNopPAController(IUploadFileProvider fileProvider, IUserProvider userProvider, IConfiguration rootConfiguration, IServiceWrapper service, ILogger<Por_HoSoNguoiNopPAController> logger) :base(service, logger)
        {
            appSettings = new AppSettings();
            rootConfiguration.Bind(appSettings);
            _fileProvider = fileProvider;
            _userProvider = userProvider;
            _service = service;
            _logger = logger;
            _savePath = appSettings.FileServerConfiguration.SavePath;
        }
        [HttpGet("InPhieuTiepNhan")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> InPhieuTiepNhan(Guid Id)
        {
            try
            {
                _logger.LogInformation("Call InPhieuTiepNhan");
                var item = await _service.Por_HoSo.InPhieuTiepNhan(Id);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("InPhieuTiepNhan : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("LuuHoSoQuyTrinh")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> LuuHoSoQuyTrinh()
        {
            try
            {
                _logger.LogInformation("Call LuuHoSoQuyTrinh");                
                if (Request.Form.TryGetValue("data", out var jsonData))
                {
                    var item = JsonConvert.DeserializeObject<HoSoQuyTrinh>(jsonData);
                    var itemSave = await _service.Por_HoSo.NopHoSo(item);
                    byte[] bytes = null;
                    var FileHoSoNguoiNops = new List<Por_FileHoSoNguoiNop>();
                    foreach (var file in Request.Form.Files)
                    {
                        var FileHoSoNguoiNop = new Por_FileHoSoNguoiNop();
                        string FileName = file.ContentDisposition.Split("\"")[3];
                        string ContentDispositionName = file.ContentDisposition.Split("\"")[1];
                        string ContentType = file.ContentType;
                        if (file.Length > 0)
                        {
                            bytes = new byte[file.Length];
                            string savePath = Path.Combine(_fileProvider.BuildSavePathYYYYMMDD(Path.Combine(_savePath, "FileHoSoNguoiNop")), FileName);
                            using (var stream = new FileStream(savePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            FileHoSoNguoiNop.Id = Guid.NewGuid();
                            FileHoSoNguoiNop.Ten = FileName;
                            FileHoSoNguoiNop.URL = savePath;
                            FileHoSoNguoiNop.IDHoSoNguoiNop = itemSave.thongTinNguoiNop.Id;
                            FileHoSoNguoiNop.IDFileMauThanhPhanHStrongQT = Guid.Parse(ContentDispositionName.Split("_")[0]);
                            FileHoSoNguoiNops.Add(FileHoSoNguoiNop);
                        }
                    }
                    if (FileHoSoNguoiNops.Count > 0)
                    {
                        await _service.Por_FileHoSoNguoiNop.SaveEntitiesAsync(FileHoSoNguoiNops.ToArray());
                    }
                    return ResponseMessage.Success(itemSave);
                }
                return ResponseMessage.Error("Xảy ra lỗi !");
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("LuuHoSoQuyTrinh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("SuaHoSoQuyTrinh")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> SuaHoSoQuyTrinh()
        {
            try
            {
                _logger.LogInformation("Call LuuHoSoQuyTrinh");
                if (Request.Form.TryGetValue("data", out var jsonData))
                {

                    var item = JsonConvert.DeserializeObject<HoSoQuyTrinh>(jsonData);
                    var itemSave = await _service.Por_HoSo.SuaHoSo(item);
                    byte[] bytes = null;
                    var FileHoSoNguoiNops = new List<Por_FileHoSoNguoiNop>();
                    string idsFileNopConcat = string.Empty;
                    foreach (var file in Request.Form.Files)
                    {
                        var FileHoSoNguoiNop = new Por_FileHoSoNguoiNop();
                        string FileName = file.ContentDisposition.Split("\"")[3];
                        string ContentDispositionName = file.ContentDisposition.Split("\"")[1];
                        string ContentType = file.ContentType;
                        if (file.Length > 0)
                        {
                            bytes = new byte[file.Length];
                            string savePath = Path.Combine(_fileProvider.BuildSavePathYYYYMMDD(Path.Combine(_savePath, "FileHoSoNguoiNop")), FileName);
                            using (var stream = new FileStream(savePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            FileHoSoNguoiNop.Id = Guid.NewGuid();                            
                            FileHoSoNguoiNop.Ten = FileName;
                            FileHoSoNguoiNop.URL = savePath;
                            FileHoSoNguoiNop.IDHoSoNguoiNop = itemSave.thongTinNguoiNop.Id;
                            FileHoSoNguoiNop.IDFileMauThanhPhanHStrongQT = Guid.Parse(ContentDispositionName.Split("_")[0]);
                            FileHoSoNguoiNops.Add(FileHoSoNguoiNop);
                        }
                    }
                    if (FileHoSoNguoiNops.Count > 0)
                    {
                        await _service.Por_FileHoSoNguoiNop.SaveEntitiesAsync(FileHoSoNguoiNops.ToArray());
                        foreach(var itm in FileHoSoNguoiNops)
                        {
                            idsFileNopConcat += itm.Id + "_";
                        }
                    }
                    if(Request.Form.TryGetValue("idsFileNop", out var idsFileNop))
                    {
                        await _service.Por_HoSo.XoaFileNop(item.thongTinNguoiNop.Id, idsFileNop + idsFileNopConcat);
                    }
                    return ResponseMessage.Success(itemSave);
                }
                return ResponseMessage.Error("Xảy ra lỗi !");
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("LuuHoSoQuyTrinh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }        
        [HttpGet("LayChiTietHoSoQuyTrinh")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> LayChiTietHoSoQuyTrinh(Guid id)
        {
            try
            {
                _logger.LogInformation("Call LayChiTietHoSoQuyTrinh");
                var item = await _service.Por_HoSoNguoiNopPA.LayChiTietHoSoQuyTrinh(id);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("LayChiTietHoSoQuyTrinh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("DsBuocQuyTrinhHoSo")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> DsBuocQuyTrinhHoSo(Guid Id)
        {
            try
            {
                _logger.LogInformation("Call DsBuocQuyTrinhHoSo");
                var item = await _service.Por_HoSoNguoiNopPA.DsBuocQuyTrinhHoSo(Id);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("DsBuocQuyTrinhHoSo : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }        
        [HttpPost("HuyQuyTrinh")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> HuyQuyTrinh([FromBody] HoSoHuy model)
        {
            try
            {
                _logger.LogInformation("Call HuyQuyTrinh");
                var UserName = _userProvider.UserName;
                await _service.Por_HoSoNguoiNopPA.HuyQuyTrinh(model.Id, UserName, model.NoiDungHuy);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("HuyQuyTrinh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }        
        [HttpPost("ChuyenXuLy")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> ChuyenXuLy([FromBody] ChuyenXuLy model)
        {
            try
            {
                _logger.LogInformation("Call ChuyenXuLy");
                var KiemTraBuocCuoiCung = await _service.Por_HoSoNguoiNopPA.KiemTraBuocCuoiCung(model.Id);
                if (KiemTraBuocCuoiCung)
                {
                    return ResponseMessage.Success(null, "Hồ sơ đang ở quy trình cuối cùng, yêu cầu trả kết quả hồ sơ !", (int)Sys_Enum.StatusCode.Warning);
                }
                var UserName = _userProvider.UserName;
                var item = await _service.Por_HoSoNguoiNopPA.ChuyenXuLy(model.Id, model.NoiDungXuLy, UserName);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("ChuyenXuLy : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("TraNguocLai")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> TraNguocLai([FromBody] TraNguocLai model)
        {
            try
            {
                _logger.LogInformation("Call TraNguocLai");
                var UserName = _userProvider.UserName;
                var item = await _service.Por_HoSoNguoiNopPA.TraNguocLai(model.Id, model.NoiDungXuLy, UserName);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("TraNguocLai : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("TiepNhanHoSo")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> TiepNhanHoSo(Guid Id)
        {
            try
            {
                _logger.LogInformation("Call TiepNhanHoSo");
                var UserName = _userProvider.UserName;
                await _service.Por_HoSoNguoiNopPA.TiepNhanHoSo(Id, UserName);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("TiepNhanHoSo : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("TraKetQua")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> TraKetQua([FromBody] TraKetQua model)
        {
            try
            {
                _logger.LogInformation("Call TraKetQua");
                var UserName = _userProvider.UserName;
                var KiemTraBuocCuoiCung = await _service.Por_HoSoNguoiNopPA.KiemTraBuocCuoiCung(model.Id);
                if(!KiemTraBuocCuoiCung)
                {
                    return ResponseMessage.Success(null, "Hồ sơ đang trong quá trình xử lý !", (int)Sys_Enum.StatusCode.Warning);
                }    
                var item = await _service.Por_HoSoNguoiNopPA.ChuyenXuLy(model.Id, model.NoiDungXuLy, UserName);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("TraKetQua : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }        
        [HttpGet("TraCuuHoSo")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> TraCuuHoSo(string SoHoSo)
        {
            try
            {
                _logger.LogInformation("Call TraCuuHoSo");
                var UserName = _userProvider.UserName;
                var item = await _service.Por_HoSoNguoiNopPA.TraCuuHoSo(SoHoSo, UserName);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("TraCuuHoSo : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("UploadFileBuocQuyTrinh")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> UploadFileBuocQuyTrinh()
        {
            try
            {
                _logger.LogInformation("Call UploadFileBuocQuyTrinh");
                var items = new List<Por_FileBuocQuyTrinh>();
                foreach (var file in Request.Form.Files)
                {
                    string FileName = file.ContentDisposition.Split("\"")[3];
                    string ContentDispositionName = file.ContentDisposition.Split("\"")[1];
                    string ContentType = file.ContentType;
                    if (file.Length > 0)
                    {                        
                        string savePath = Path.Combine(_fileProvider.BuildSavePathYYYYMMDD(Path.Combine(_savePath, "FileBuocQuyTrinh")), FileName);
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        var item = new Por_FileBuocQuyTrinh();
                        item.IDBuocQuyTrinh = Guid.Parse(ContentDispositionName);
                        item.Ten = FileName;
                        item.URL = savePath;
                        items.Add(item);
                    }
                }
                if(items.Count > 0)
                {
                    await _service.Por_FileBuocQuyTrinh.SaveEntitiesAsync(items.ToArray());
                }    
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("UploadFileBuocQuyTrinh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        //

        [HttpGet("HoSoHuy")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> HoSoHuy(string SoHoSo)
        {
            try
            {
                _logger.LogInformation("Call HoSoHuy");
                var UserName = _userProvider.UserName;
                var item = await _service.Por_HoSoNguoiNopPA.HoSoHuy(SoHoSo, UserName);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("HoSoHuy : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("HoSoHoanThanh")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> HoSoHoanThanh(string SoHoSo)
        {
            try
            {
                _logger.LogInformation("Call HoSoHoanThanh");
                var item = await _service.Por_HoSoNguoiNopPA.HoSoHoanThanh(SoHoSo);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("HoSoHoanThanh : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("HoSoXuLy")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> HoSoXuLy(string SoHoSo)
        {
            try
            {
                _logger.LogInformation("Call HoSoXuLy");
                var UserId = _userProvider.Id;
                var UserName = _userProvider.UserName;
                var item = await _service.Por_HoSoNguoiNopPA.HoSoXuLy(UserId, UserName, SoHoSo);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("HoSoDangXuLy : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("HoSoTamNgung")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> HoSoTamDung(string SoHoSo)
        {
            try
            {
                _logger.LogInformation("Call HoSoTamDung");
                var UserId = _userProvider.Id;
                var item = await _service.Por_HoSoNguoiNopPA.HoSoTamDung(SoHoSo);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("HoSoTamDung : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("HoSoTraKetQua")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> HoSoTraKetQua(string SoHoSo)
        {
            try
            {
                _logger.LogInformation("Call HoSoTraKetQua");
                var UserName = _userProvider.UserName;
                var item = await _service.Por_HoSoNguoiNopPA.HoSoTraKetQua(SoHoSo, UserName);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("HoSoTraKetQua : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpDelete("XoaHoSo/{Id}")]
        [AuthorizeFilter]
        public virtual async Task<IActionResult> XoaHoSo(Guid Id)
        {
            try
            {
                _logger.LogInformation("Call XoaHoSo");
                var UserId = _userProvider.Id;
                await _service.Por_HoSoNguoiNopPA.XoaHoSo(Id);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("XoaHoSo : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
