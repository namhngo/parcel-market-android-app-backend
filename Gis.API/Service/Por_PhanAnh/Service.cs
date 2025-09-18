using Gis.API.Infrastructure;
using Gis.Core.Interfaces;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Gis.API.Model;
using Microsoft.EntityFrameworkCore;
using Gis.Core.Constant;
using Gis.API.ViewModel.PhanAnh;
using Gis.Core.Helpers;
using static Gis.API.Infrastructure.Enums;
using Gis.API.ViewModel.SendMessage;

namespace Gis.API.Service.Por_PhanAnh
{
    public class Service : RepositoryBase<Por_GopYPhanAnh>,IService
    {
        private readonly DomainDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public Service(DomainDbContext dbContext, IDateTimeProvider dateTimeProvider, IUserProvider userService) : base(dbContext, dateTimeProvider, userService)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userService;
        }
        public async Task<Por_GopYPhanAnh> GuiPhanAnh(Por_GopYPhanAnh model)
        {
            var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone);
            var quyTrinh = await _dbContext.Por_QuyTrinhs.Where(o => o.IDLoaiPhanAnh == model.IDLinhVuc).FirstOrDefaultAsync();
            if(quyTrinh == null)
            {
                throw new Exception("Loại phản ánh không có quy trình xử lý !");
            }
            var BuocQuyTrinhFirst = await (from x in _dbContext.Por_QuyTrinhs
                                           join y in _dbContext.Por_BuocQuyTrinhs on x.Id equals y.IDQuyTrinh
                                           where x.Id == quyTrinh.Id
                                           select new { IDQuyTrinh = x.Id, ThoiGianThucHien = x.ThoiGianThucHien, IDBuocQuyTrinh = y.Id, ThuTuBuoc = y.ThuTuBuoc })
                      .OrderBy(o => o.ThuTuBuoc)
                      .FirstAsync();
            var TrangThaiChoXuLy = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo && o.Code == TrangThaiQuyTrinh.choxuly.ToString()).FirstAsync();
            var LoaiPhanAnh = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.LoaiPhanAnh && o.Id == model.IDLinhVuc).FirstAsync();
            model.Id = Guid.NewGuid();            
            model.MaPhanAnh = LoaiPhanAnh.Code.ToUpper() + StringHelpers.RandomString(5);
            model.IDTrangThaiPA = TrangThaiChoXuLy.Id;
            model.ThoiGianXuLy = quyTrinh.ThoiGianThucHien;
            model.CreatedDateTime = DateTimeOffsetNow;
            await _dbContext.Por_GopYPhanAnhs.AddAsync(model);
            Model.Por_HoSoPA_QuyTrinh por_HoSo_QuyTrinh = new Model.Por_HoSoPA_QuyTrinh();
            por_HoSo_QuyTrinh.Id = Guid.NewGuid();            
            por_HoSo_QuyTrinh.IDHoSo = model.Id;
            por_HoSo_QuyTrinh.IDQuyTrinh = BuocQuyTrinhFirst.IDQuyTrinh;
            por_HoSo_QuyTrinh.IDBuocQuyTrinh = BuocQuyTrinhFirst.IDBuocQuyTrinh;
            por_HoSo_QuyTrinh.ThoiGianThucHien = BuocQuyTrinhFirst.ThoiGianThucHien;
            por_HoSo_QuyTrinh.NgayNop = DateTimeOffsetNow;
            por_HoSo_QuyTrinh.NgayTiepNhan = null;
            por_HoSo_QuyTrinh.NgayNhanKQ = null;
            por_HoSo_QuyTrinh.NgayDuKienNhanKQ = DateTimeOffsetNow.AddDays(BuocQuyTrinhFirst.ThoiGianThucHien);
            await _dbContext.Por_HoSoPA_QuyTrinhs.AddAsync(por_HoSo_QuyTrinh);
            await UnitOfWork.SaveAsync();
            await SendMessageHoSoPhanAnh(model.Id, SmsConfig.Mau_Sms_HSPA_NopHoSo.ToString(), EmailConfig.Mau_Email_HSPA_NopHoSo.ToString());
            return model;
        }
        private async Task SendMessageHoSoPhanAnh(Guid IDHoSo, string TypeSms, string TypeEmail)
        {
            try
            {
                string template_send_message_insert = "INSERT INTO public.por_smssend (mobile, email, content, contentemail, status, statusmail, time_create, time_send, type) VALUES";
                string template_send_message_value = "('{0}', '{1}', '{2}', '{3}', {4}, '{5}', '{6}', '{7}', '{8}')";
                Guid valueSms, valueEmail;
                var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone);
                var configSendMessage = await _dbContext.Sys_Configs.Where(o => o.Type == Core.Enums.ConfigType.Email || o.Type == Core.Enums.ConfigType.SMS).ToListAsync();
                var configTiepNhanHoSo_Sms = configSendMessage.Where(o => o.Code == TypeSms).FirstOrDefault();
                var configTiepNhanHoSo_Email = configSendMessage.Where(o => o.Code == TypeEmail).FirstOrDefault();
                if (configTiepNhanHoSo_Sms == null && configTiepNhanHoSo_Email == null)
                    return;
                if (Guid.TryParse(configTiepNhanHoSo_Sms.Value, out valueSms)) { }
                if (Guid.TryParse(configTiepNhanHoSo_Email.Value, out valueEmail)) { }
                if (valueSms == Guid.Empty && valueEmail == Guid.Empty)
                    return;
                #region Query
                var item = await (from x in _dbContext.Por_GopYPhanAnhs
                                  join y in _dbContext.Por_HoSoPA_QuyTrinhs on x.Id equals y.IDHoSo
                                  join z in _dbContext.Por_QuyTrinhs on y.IDQuyTrinh equals z.Id
                                  join a in _dbContext.Por_LinhVucs on z.IDLinhVuc equals a.Id
                                       into sub_Por_LinhVucs
                                  from aa in sub_Por_LinhVucs.DefaultIfEmpty()
                                  select new SendMessage()
                                  {
                                      Id = x.Id,
                                      MaHoSo = x.MaPhanAnh,
                                      HoVaTen = x.TenNguoiGui,
                                      Email = x.Email,
                                      SĐT = x.SoDienThoai,
                                      SoNha = x.SoNha,
                                      TenDuong = x.TenDuong,
                                      PhuongXa = x.PhuongXa,
                                      QuanHuyen = x.QuanHuyen,
                                      TinhThanhPho = x.TinhThanhPho,
                                      LinhVuc = aa != null ? aa.Ten : "",
                                      LoaiPhanAnh = "",
                                  }).FirstOrDefaultAsync(o => o.Id == IDHoSo);
                if (item == null)
                    return;
                string phone = "", content_phone = "", template_phone = "", column_sql = "";
                string email = "", content_email = "", template_email = "";
                #endregion
                var PhuongXa = await _dbContext.Por_PhuongXaThiTrans.Where(o => o.Id == item.PhuongXa).FirstOrDefaultAsync();
                var QuanHuyen = await _dbContext.Por_QuanHuyens.Where(o => o.Id == item.QuanHuyen).FirstOrDefaultAsync();
                var TinhThanhPho = await _dbContext.Por_TinhThanhPhos.Where(o => o.Id == item.TinhThanhPho).FirstOrDefaultAsync();
                #region Sms
                var templateSms = await _dbContext.Por_TemplateSmss.Where(o => o.Id == valueSms).FirstOrDefaultAsync();
                if (templateSms != null)
                {
                    phone = item.SĐT;
                    template_phone = templateSms.NoiDung;
                    column_sql = templateSms.CotSql;
                    string DiaChi = item.SoNha + ", " + item.TenDuong + ", ";

                    DiaChi += (PhuongXa != null ? PhuongXa.Ten : "") + ", ";
                    DiaChi += (QuanHuyen != null ? QuanHuyen.Ten : "") + ", ";
                    DiaChi += (TinhThanhPho != null ? TinhThanhPho.Ten : "");
                    item.DiaChi = DiaChi;
                    string[] arr_column_sql = column_sql.Split(",");
                    List<string> arr_value = new List<string>();
                    for (var i = 0; i < arr_column_sql.Length; i++)
                    {
                        if (arr_column_sql[i] == "MaHoSo")
                        {
                            arr_value.Add(item.MaHoSo);
                        }
                        else if (arr_column_sql[i] == "HoVaTen")
                        {
                            arr_value.Add(item.HoVaTen);
                        }
                        else if (arr_column_sql[i] == "Email")
                        {
                            arr_value.Add(item.Email);
                        }
                        else if (arr_column_sql[i] == "SĐT")
                        {
                            arr_value.Add(item.SĐT);
                        }
                        else if (arr_column_sql[i] == "DiaChi")
                        {
                            arr_value.Add(item.DiaChi);
                        }
                        else if (arr_column_sql[i] == "LinhVuc")
                        {
                            arr_value.Add(item.LinhVuc);
                        }
                        else if (arr_column_sql[i] == "LoaiPhanAnh")
                        {
                            arr_value.Add(item.LoaiPhanAnh);
                        }
                        else if (arr_column_sql[i] == "NgayHienTai")
                        {
                            arr_value.Add(DateTime.Now.ToString("dd/MM/yyyy"));
                        }
                    }
                    content_phone = string.Format(template_phone, arr_value.ToArray());
                }
                #endregion
                #region Email
                var templateEmail = await _dbContext.Por_TemplateEmails.Where(o => o.Id == valueEmail).FirstOrDefaultAsync();
                if (templateEmail != null)
                {
                    email = item.Email;
                    template_email = templateEmail.NoiDung;
                    column_sql = templateEmail.CotSql;
                    string DiaChi = item.SoNha + ", " + item.TenDuong + ", ";
                    DiaChi += (PhuongXa != null ? PhuongXa.Ten : "") + ", ";
                    DiaChi += (QuanHuyen != null ? QuanHuyen.Ten : "") + ", ";
                    DiaChi += (TinhThanhPho != null ? TinhThanhPho.Ten : "");
                    item.DiaChi = DiaChi;
                    string[] arr_column_sql = column_sql.Split(",");
                    List<string> arr_value = new List<string>();
                    for (var i = 0; i < arr_column_sql.Length; i++)
                    {
                        if (arr_column_sql[i] == "MaHoSo")
                        {
                            arr_value.Add(item.MaHoSo);
                        }
                        else if (arr_column_sql[i] == "HoVaTen")
                        {
                            arr_value.Add(item.HoVaTen);
                        }
                        else if (arr_column_sql[i] == "Email")
                        {
                            arr_value.Add(item.Email);
                        }
                        else if (arr_column_sql[i] == "SĐT")
                        {
                            arr_value.Add(item.SĐT);
                        }
                        else if (arr_column_sql[i] == "DiaChi")
                        {
                            arr_value.Add(item.DiaChi);
                        }
                        else if (arr_column_sql[i] == "LinhVuc")
                        {
                            arr_value.Add(item.LinhVuc);
                        }
                        else if (arr_column_sql[i] == "LoaiPhanAnh")
                        {
                            arr_value.Add(item.LoaiPhanAnh);
                        }
                        else if (arr_column_sql[i] == "NgayHienTai")
                        {
                            arr_value.Add(DateTime.Now.ToString("dd/MM/yyyy"));
                        }
                    }
                    content_email = string.Format(template_email, arr_value.ToArray());
                }
                #endregion
                _dbContext.Database.ExecuteSqlRaw(template_send_message_insert
                + String.Format(template_send_message_value, phone, email, content_phone, content_email, 0, 0,
                DateTimeOffsetNow.ToString("dd/MM/yyyy HH:mm:ss"), "", "HSPA"));
            }
            catch (Exception ex) { }
        }
        public async Task<Paged<ListPhanAnh>> GetPagedCustomAsync(int page, int pageSize, int totalLimitItems, string search)
        {
            var query = (from x in _dbContext.Por_GopYPhanAnhs
                        join y in _dbContext.Sys_Categories on x.IDLinhVuc equals y.Id
                        join z in _dbContext.Sys_Categories on x.IDTrangThaiPA equals z.Id
                        join o in _dbContext.Por_HoSoPA_QuyTrinhs on x.Id equals o.IDHoSo
                        select new ListPhanAnh() {         
                            Id= x.Id,
                            MaPhanAnh = x.MaPhanAnh,
                            LinhVuc = y.Name,
                            TenNguoiGui = x.TenNguoiGui,
                            TieuDe = x.TieuDe,
                            NgayGui = o.NgayNop.ToString("dd/MM/yyyy"),
                            NgayGuiDate = o.NgayNop,
                            TrangThai = z.Name,
                            SoDienThoai = x.SoDienThoai,
                            ThoiGianXuLy = o.ThoiGianThucHien,

                        });
            var result = new Paged<ListPhanAnh>(query, page, pageSize, totalLimitItems);
            result.Items = await query.OrderByDescending(o => o.NgayGuiDate).Paged(page, pageSize, totalLimitItems).ToListAsync();
            return result;
        }    
        public async Task<List<GopYPhanAnh>> GetDSPhanAnh(string search, string userName)
        {
            var query = from x in _dbContext.Por_GopYPhanAnhs
                        join y in _dbContext.Por_HoSoPA_QuyTrinhs on x.Id equals y.IDHoSo
                        join z in _dbContext.Por_QuyTrinhs on y.IDQuyTrinh equals z.Id
                        join a in _dbContext.Por_BuocQuyTrinhs on y.IDBuocQuyTrinh equals a.Id
                            into sub_Por_BuocQuyTrinhs
                        from aa in sub_Por_BuocQuyTrinhs.DefaultIfEmpty()
                        where x.CongKhai == true || x.TaiKhoanNguoiGui == userName
                        select new { x.Id, x.IDTrangThaiPA, x.SoDienThoai, x.MaPhanAnh, 
                            x.TieuDe, x.TenNguoiGui, x.TaiKhoanNguoiGui, x.CreatedDateTime, y.NgayDuKienNhanKQ, y.NgayNhanKQ, x.GhiChu,
                            IdChucNang = aa != null ? aa.IDChucNangBuocQuyTrinh : Guid.Empty
                        };
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(o => o.MaPhanAnh.ToLower().Contains(search.ToLower()) || o.SoDienThoai.ToLower().Contains(search.ToLower()));
            }
            var ChucNangs = await _dbContext.Por_ChucNang_BuocQuyTrinhs.ToListAsync();
            var TrangThais = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo).ToListAsync();
            var items = await query.Select(o => new GopYPhanAnh() { IdChucNang = o.IdChucNang, IDTrangThaiPA = o.IDTrangThaiPA, Id = o.Id, SoDienThoai = o.SoDienThoai, MaPhanAnh = o.MaPhanAnh, TieuDe = o.TieuDe, TenNguoiGui = o.TenNguoiGui, TaiKhoanNguoiGui = o.TaiKhoanNguoiGui, CreatedDateTime = o.CreatedDateTime, 
                ThoiGianXuLy = o.NgayNhanKQ.HasValue ? o.NgayNhanKQ.Value.ToString("dd/MM/yyyy"): "",
                ThoiGianXuLyDuKien = o.NgayDuKienNhanKQ.ToString("dd/MM/yyyy"), GhiChu = o.GhiChu }).ToListAsync();
            foreach(var item in items)
            {
                var trangthai = TrangThais.First(e => e.Id == item.IDTrangThaiPA);
                if (trangthai.Code == TrangThaiQuyTrinh.traketqua.ToString())
                {
                    item.TrangThai = trangthai.Name;
                }
                else if (trangthai.Code == TrangThaiQuyTrinh.huy.ToString())
                {
                    item.TrangThai = trangthai.Name;
                }
                else
                {
                    var chucnung = ChucNangs.First(e => e.Id == item.IdChucNang);
                    item.TrangThai = trangthai.Name + " " + chucnung.Ten;
                }
            }    
            return items;
        }
        public async Task<NoiDungPhanAnh> XemNoiDungPhanAnh(Guid Id)
        {
            var item = await _dbContext.Por_GopYPhanAnhs.Where(o => o.Id == Id)
                                                    .Select(o => new NoiDungPhanAnh() { 
                                                        Id = o.Id, TieuDe =  o.TieuDe, NoiDung = o.NoiDung 
                                                    }).FirstOrDefaultAsync();
            if(item != null)
            {
                var item_TraKetQua = await (from x in _dbContext.Por_HoSoPA_QuyTrinhs
                                      join y in _dbContext.Por_HoSoPA_Buoc_QuyTrinhs on x.NgayNhanKQ equals y.NgayKetThuc
                                      where x.IDHoSo == Id && y.IDHoSo == Id
                                      select new { y.Id, y.NoiDungXuLy }).FirstOrDefaultAsync();
                if(item_TraKetQua != null)
                {
                    item.NoiDungTraKetQua = item_TraKetQua.NoiDungXuLy;
                    item.FileTraKetQua = await _dbContext.Por_FileBuocQuyTrinhs.Where(o => o.IDBuocQuyTrinh == item_TraKetQua.Id).ToListAsync();
                }
            }    
            return item;
        }
        public async Task<ChiTietPhanAnh> XemChiTietPhanAnh(Guid Id)
        {
            var item = await (from o in _dbContext.Por_GopYPhanAnhs
                        join i in _dbContext.Sys_Categories on o.IDLinhVuc equals i.Id
                        join a in _dbContext.Por_TinhThanhPhos on o.TinhThanhPho equals a.Id
                        join b in _dbContext.Por_QuanHuyens on o.QuanHuyen equals b.Id
                        join c in _dbContext.Por_PhuongXaThiTrans on o.PhuongXa equals c.Id
                        where o.Id == Id
                        select new ChiTietPhanAnh()
                        {
                            Id = o.Id,
                            CongKhai = o.CongKhai,
                            MaPhanAnh = o.MaPhanAnh,
                            TenNguoiGui = o.TenNguoiGui,
                            SoDienThoai = o.SoDienThoai,
                            Email = o.Email,
                            TinhThanhPho = a.Ten,
                            QuanHuyen = b.Ten,
                            PhuongXa = c.Ten,
                            SoNha = o.SoNha,
                            TenDuong = o.TenDuong,
                            IDLinhVuc = o.IDLinhVuc,
                            LinhVuc = i.Name,
                            TieuDe = o.TieuDe,
                            NoiDung = o.NoiDung,
                            FileName = o.TenFile,
                            FileUrl = o.URL
                        }).FirstOrDefaultAsync();
            if (item != null)
            {
                var item_TraKetQua = await (from x in _dbContext.Por_HoSoPA_QuyTrinhs
                                            join y in _dbContext.Por_HoSoPA_Buoc_QuyTrinhs on x.NgayNhanKQ equals y.NgayKetThuc
                                            where x.IDHoSo == Id && y.IDHoSo == Id
                                            select new { y.Id, y.NoiDungXuLy }).FirstOrDefaultAsync();
                if (item_TraKetQua != null)
                {
                    item.NoiDungTraKetQua = item_TraKetQua.NoiDungXuLy;
                    item.FileTraKetQua = await _dbContext.Por_FileBuocQuyTrinhs.Where(o => o.IDBuocQuyTrinh == item_TraKetQua.Id).ToListAsync();
                }
            }
            return item;
        }
        public async Task CongKhaiPA(Guid id, bool congKhai)
        {
            var phanAnh = await _dbContext.Por_GopYPhanAnhs.Where(o => o.Id == id).FirstOrDefaultAsync();
            phanAnh.CongKhai = congKhai;
            await _dbContext.SaveChangesAsync();
        }    
        public async Task BinhLuanPhanAnh(BinhLuanPhanAnh binhLuanPhanAnh)
        {
            var dsThich = await _dbContext.Por_BinhLuanThichs.Where(o => o.IDGopYPhanAnh == binhLuanPhanAnh.Id && o.CreatedBy == binhLuanPhanAnh.TaiKhoan).ToListAsync();
            var dsKhongThich = await _dbContext.Por_BinhLuanKhongThichs.Where(o => o.IDGopYPhanAnh == binhLuanPhanAnh.Id && o.CreatedBy == binhLuanPhanAnh.TaiKhoan).ToListAsync();
            if(binhLuanPhanAnh.Thich)
            {
                if(dsKhongThich.Count > 0)
                {
                    _dbContext.Por_BinhLuanKhongThichs.RemoveRange(dsKhongThich);
                    await _dbContext.SaveChangesAsync();
                }
                if(dsThich.Count == 0)
                {
                    var binhLuanThich = new Model.Por_BinhLuanThich();
                    binhLuanThich.IDGopYPhanAnh = binhLuanPhanAnh.Id;
                    binhLuanThich.CreatedBy = binhLuanPhanAnh.TaiKhoan;
                    await _dbContext.Por_BinhLuanThichs.AddAsync(binhLuanThich);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                if (dsThich.Count > 0)
                {
                    _dbContext.Por_BinhLuanThichs.RemoveRange(dsThich);
                    await _dbContext.SaveChangesAsync();
                }
                if (dsKhongThich.Count == 0)
                {
                    var binhLuanKhongThich = new Model.Por_BinhLuanKhongThich();
                    binhLuanKhongThich.IDGopYPhanAnh = binhLuanPhanAnh.Id;
                    binhLuanKhongThich.CreatedBy = binhLuanPhanAnh.TaiKhoan;
                    await _dbContext.Por_BinhLuanKhongThichs.AddAsync(binhLuanKhongThich);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
        public async Task<BinhLuanPhanAnh> LayDSBinhLuanPhanAnh(Guid IDGopYPhanAnh, string TaiKhoan)
        {
            var item = new BinhLuanPhanAnh();
            if(!string.IsNullOrEmpty(TaiKhoan))
            {
                item.CoThich = await _dbContext.Por_BinhLuanThichs.Where(o => o.IDGopYPhanAnh == IDGopYPhanAnh && o.CreatedBy == TaiKhoan).AnyAsync();
                item.KhongThich = await _dbContext.Por_BinhLuanKhongThichs.Where(o => o.IDGopYPhanAnh == IDGopYPhanAnh && o.CreatedBy == TaiKhoan).AnyAsync();
            }
            item.SLThich = await _dbContext.Por_BinhLuanThichs.Where(o => o.IDGopYPhanAnh == IDGopYPhanAnh).CountAsync();
            item.SLKhongThich = await _dbContext.Por_BinhLuanKhongThichs.Where(o => o.IDGopYPhanAnh == IDGopYPhanAnh).CountAsync();
            return item;
        }
        public async Task GhiChuPhanAnh(GhiChuPhanAnh ghiChuPhanAnh)
        {
            var item = await _dbContext.Por_GopYPhanAnhs.Where(o => o.Id == ghiChuPhanAnh.Id).FirstAsync();
            item.IDTrangThaiPA = ghiChuPhanAnh.IDTrangThaiPA;
            item.ThoiGianXuLy = ghiChuPhanAnh.ThoiGianXuLy;
            item.GhiChu = ghiChuPhanAnh.GhiChu;
            await _dbContext.SaveChangesAsync();
        }
    }
}

