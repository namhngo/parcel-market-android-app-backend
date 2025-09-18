using Microsoft.EntityFrameworkCore;
using Gis.API.Infrastructure;
using Gis.Core.Constant;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gis.API.ViewModel.HoSoQuyTrinh;
using static Gis.API.Infrastructure.Enums;
using Gis.API.ViewModel.SendMessage;
using Microsoft.Extensions.Hosting;
using Gis.API.ViewModel.QuyTrinh;

namespace Gis.API.Service.Por_HoSoNguoiNop
{
    public class Service:RepositoryBase<Model.Por_HoSoNguoiNop>, Por_HoSoNguoiNop.IService
    {
        private readonly DomainDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public Service(DomainDbContext dbContext, IDateTimeProvider dateTimeProvider, IUserProvider userService):base(dbContext, dateTimeProvider, userService)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userService;
        }
        private async Task<List<Guid>> PermWorkflowIDs(string UserName)
        {
            try
            {
                List<Guid> items = new List<Guid>();
                var organ = await (from x in _dbContext.Sys_Users_Roles
                                   join y in _dbContext.Sys_Organizations on x.OrganId equals y.Id
                                   join z in _dbContext.Sys_Users on x.UserId equals z.Id
                                   where z.UserName == UserName
                                   select y).FirstOrDefaultAsync();
                if (organ != null && !string.IsNullOrEmpty(organ.PermWorkFlow))
                {
                    var PermWorkFlowIds = organ.PermWorkFlow.Split(",");
                    foreach(var id in PermWorkFlowIds)
                    {
                        items.Add(Guid.Parse(id));
                    }    
                }    
                return items;
            }
            catch(Exception ex) { return null; }
        }
        public async Task<List<TraCuuHoSo>> TraCuuHoSo(string SoHoSo, string UserName)
        {
            var query = from x in _dbContext.Por_HoSoNguoiNops                        
                        join y in _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo) on x.IDTrangThaiHS equals y.Id
                        join z in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals z.IDHoSo
                        join o in _dbContext.Por_QuyTrinhs on z.IDQuyTrinh equals o.Id
                        select new { x.Id, IdThuTuc = o.Id, o.TenThuTuc, x.ThanhToan, x.SoHoSo, TrangThai = y.Name, z.NguoiTiepNhan, z.NgayTiepNhan, x.HoTen, z.NgayNop};
            if (!string.IsNullOrEmpty(SoHoSo))
            {
                query = query.Where(o => o.SoHoSo.ToLower().Contains(SoHoSo.ToLower()));
            }
            var permWorkflowIDs = await PermWorkflowIDs(UserName);
            if (permWorkflowIDs != null && permWorkflowIDs.Count > 0)
            {
                query = query.Where(o => permWorkflowIDs.Contains(o.IdThuTuc));
            }    
            else
            {
                query = query.Where(o => o.IdThuTuc == Guid.Empty);
            }    
            var items = await query.OrderByDescending(o => o.NgayNop).Select(o => new TraCuuHoSo() { Id = o.Id, TenThuTuc = o.TenThuTuc, ThanhToan = o.ThanhToan, SoHoSo = o.SoHoSo, NguoiTiepNhan = o.NguoiTiepNhan, NgayTiepNhan = (o.NgayTiepNhan.HasValue ? o.NgayTiepNhan.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""), NguoiNop = o.HoTen, NgayNop = o.NgayNop.ToString("dd/MM/yyyy HH:mm:ss"), TrangThai = o.TrangThai }).ToListAsync();
            foreach(var item in items)
            {
                if (string.IsNullOrEmpty(item.NgayTiepNhan) && item.ThanhToan != null)
                {
                    if (!item.ThanhToan.Value)
                    {
                        item.TrangThai = "Chờ thanh toán";
                    }
                }
            }    
            return items;
        }       
        public async Task<bool> KiemTraBuocCuoiCung(Guid Id)
        {
            var hoso_quytrinh = await _dbContext.Por_HoSo_QuyTrinhs.Where(o => o.IDHoSo == Id).FirstAsync();
            var buoc_cuoicung_quytrinh = await _dbContext.Por_BuocQuyTrinhs.Where(o => o.IDQuyTrinh == hoso_quytrinh.IDQuyTrinh).OrderByDescending(o => o.ThuTuBuoc).FirstAsync();
            if(hoso_quytrinh.IDBuocQuyTrinh == buoc_cuoicung_quytrinh.Id)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> KiemTraBuocCuoiCung(Guid idBuocQuyTrinh, Guid idQuyTrinh)
        {
            var buoc_cuoicung_quytrinh = await _dbContext.Por_BuocQuyTrinhs.Where(o => o.IDQuyTrinh == idQuyTrinh).OrderByDescending(o => o.ThuTuBuoc).FirstAsync();
            if (buoc_cuoicung_quytrinh.Id == idBuocQuyTrinh)
            {
                return true;
            }
            return false;
        }
        public async Task<Guid> ChuyenXuLy(Guid Id, string NoiDung,string UserName)
        {
            var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone);
            var DsBuocQuyTrinh = await (from x in _dbContext.Por_BuocQuyTrinhs
                                        join y in _dbContext.Por_QuyTrinhs on x.IDQuyTrinh equals y.Id
                                        join z in _dbContext.Por_HoSo_QuyTrinhs on x.IDQuyTrinh equals z.IDQuyTrinh
                                        where z.IDHoSo == Id && x.ThuocThoiGianThucHien == y.ThoiGianThucHien
                                        select new { x.Id,  x.ThuTuBuoc, x.ThoiGianThucHien, x.IDsNguoiDungThamGia, x.GuiSms, x.IDMauSms, x.GuiEmail, x.IDMauEmail }).OrderBy(o => o.ThuTuBuoc).ToListAsync();
            var hosoquytrinh = await _dbContext.Por_HoSo_QuyTrinhs.Where(o => o.IDHoSo == Id).FirstAsync();
            Guid id_buocquytrinhcurrent = hosoquytrinh.IDBuocQuyTrinh;
            Guid id_buocquytrinhnext = Guid.Empty;
            Guid id_buocquytrinhprev = Guid.Empty;
            bool isBuocQuyTrinhCurrent_Next = false;
            int thoihanxulynext = 0;
            string nguoiDungThamGia = "";
            bool guiSms = false;
            Guid? idMauSms = Guid.Empty;
            bool guiEmail = false;
            Guid? idMauEmail = Guid.Empty;
            for (var i = 0;i < DsBuocQuyTrinh.Count;i++)
            {                 
                if (isBuocQuyTrinhCurrent_Next == true)
                {
                    id_buocquytrinhnext = DsBuocQuyTrinh[i].Id;
                    thoihanxulynext = DsBuocQuyTrinh[i].ThoiGianThucHien;
                    nguoiDungThamGia = DsBuocQuyTrinh[i].IDsNguoiDungThamGia;
                    guiSms = DsBuocQuyTrinh[i].GuiSms;
                    idMauSms = DsBuocQuyTrinh[i].IDMauSms;
                    guiEmail = DsBuocQuyTrinh[i].GuiEmail;
                    idMauEmail = DsBuocQuyTrinh[i].IDMauEmail;
                    break;
                }    
                if(DsBuocQuyTrinh[i].Id == id_buocquytrinhcurrent)
                {
                    isBuocQuyTrinhCurrent_Next = true;
                    if(i > 1)
                    {
                        id_buocquytrinhprev = DsBuocQuyTrinh[i - 1].Id;
                    }                        
                }    
            }            
            var hoso = await _dbContext.Por_HoSoNguoiNops.Where(o => o.Id == Id).FirstAsync();
            var trangthai = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo).ToListAsync();
            var trangthai_choxuly = trangthai.Where(o => o.Code == TrangThaiQuyTrinh.choxuly.ToString()).First();
            var trangthai_dangxuly = trangthai.Where(o => o.Code == TrangThaiQuyTrinh.dangxuly.ToString()).First();
            var trangthai_daxuly = trangthai.Where(o => o.Code == TrangThaiQuyTrinh.daxuly.ToString()).First();
            hoso.IDTrangThaiHS = trangthai_choxuly.Id;
            //
            var HoSoBuocQuyTrinhCurrent = await _dbContext.Por_HoSo_Buoc_QuyTrinhs.Where(o => o.IDHoSo == hosoquytrinh.IDHoSo && o.IDQuyTrinh == hosoquytrinh.IDQuyTrinh && o.IDBuocQuyTrinh == id_buocquytrinhcurrent).FirstAsync();
            HoSoBuocQuyTrinhCurrent.IDTrangThai = trangthai_daxuly.Id;
            HoSoBuocQuyTrinhCurrent.NgayKetThuc = DateTimeOffsetNow;
            HoSoBuocQuyTrinhCurrent.NguoiXuLy = UserName;
            HoSoBuocQuyTrinhCurrent.NoiDungXuLy = NoiDung;            
            //buocquytrinhnext            
            if (id_buocquytrinhnext == Guid.Empty)//
            {
                var trangthai_traketqua = trangthai.Where(o => o.Code == TrangThaiQuyTrinh.traketqua.ToString()).First();
                hosoquytrinh.IDBuocQuyTrinh = id_buocquytrinhnext;
                hosoquytrinh.NgayNhanKQ = DateTimeOffsetNow;
                hosoquytrinh.NguoiTraKetQua = UserName;
                hosoquytrinh.NguoiXuLy = UserName;
                hoso.IDTrangThaiHS = trangthai_traketqua.Id;
                await SendMessageHoSoDiaChinh(Id, SmsConfig.Mau_Sms_HSDVC_TraKetQua.ToString(), EmailConfig.Mau_Email_HSDVC_TraKetQua.ToString());
            }    
            else
            {                                
                hosoquytrinh.IDBuocQuyTrinh = id_buocquytrinhnext;
                hosoquytrinh.NguoiXuLy = string.Empty;                
                if (!string.IsNullOrEmpty(HoSoBuocQuyTrinhCurrent.NguoiTraNguocLai) 
                    && hoso.IDTrangThaiHS == trangthai_choxuly.Id)
                {
                    hosoquytrinh.NguoiXuLy = HoSoBuocQuyTrinhCurrent.NguoiTraNguocLai;
                }
                var dsNguoiDungThamGiaSplit = nguoiDungThamGia.Split(",");
                var laBuocCuoiCung = await KiemTraBuocCuoiCung(id_buocquytrinhnext, hosoquytrinh.IDQuyTrinh);
                if (laBuocCuoiCung)
                {
                    await SendMessageHoSoDiaChinh(Id, SmsConfig.Mau_Sms_HSDVC_ToiBuocCuoi.ToString(), EmailConfig.Mau_Email_HSDVC_ToiBuocCuoi.ToString());
                }
                await SendMessageHoSoDiaChinh(Id, dsNguoiDungThamGiaSplit, hosoquytrinh.NguoiXuLy, guiSms, idMauSms, guiEmail, idMauEmail);
            }
            await UnitOfWork.SaveAsync();
            return HoSoBuocQuyTrinhCurrent.Id;
        }
        public async Task<Guid> TiepTuc(Guid Id,string UserName)
        {
            var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone);
            var HoSoQuyTrinh = await _dbContext.Por_HoSo_QuyTrinhs.FirstAsync(o => o.IDHoSo == Id);
            var HoSo = await _dbContext.Por_HoSoNguoiNops.FirstAsync(o => o.Id == Id);

            var HoSoBuocQuyTrinh = await _dbContext.Por_HoSo_Buoc_QuyTrinhs.FirstOrDefaultAsync(o => o.IDHoSo == HoSoQuyTrinh.IDHoSo && o.IDQuyTrinh == HoSoQuyTrinh.IDQuyTrinh && o.IDBuocQuyTrinh == HoSoQuyTrinh.IDBuocQuyTrinh);
            var IDTrangThai_TamDung = HoSoBuocQuyTrinh.IDTrangThai_TamDung;
            HoSoBuocQuyTrinh.NgayKetThucTamDung = DateTimeOffsetNow;
            HoSoBuocQuyTrinh.NguoiTiepTuc = UserName;
            HoSoBuocQuyTrinh.IDTrangThai = IDTrangThai_TamDung;
            HoSoBuocQuyTrinh.IDTrangThai_TamDung = Guid.Empty;
            HoSo.IDTrangThaiHS = IDTrangThai_TamDung;

            await SendMessageHoSoDiaChinhTiepTuc(Id, HoSoBuocQuyTrinh.NguoiXuLy, SmsConfig.Mau_Sms_HSDVC_TiepTuc.ToString(), EmailConfig.Mau_Email_HSDVC_TiepTuc.ToString());
            await _dbContext.SaveChangesAsync();
            return Id;
        }
        public async Task<Guid> TamDung(Guid Id, string NoiDung, Guid? TieuChi, string UserName)
        {
            var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone);
            var HoSoQuyTrinh = await _dbContext.Por_HoSo_QuyTrinhs.FirstAsync(o => o.IDHoSo == Id);
            var HoSo = await _dbContext.Por_HoSoNguoiNops.FirstAsync(o => o.Id == Id);
            var trangthai_tamdung = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo && o.Code == TrangThaiQuyTrinh.tamdung.ToString()).FirstAsync();
            HoSo.IDTrangThaiHS = trangthai_tamdung.Id;
            var HoSoBuocQuyTrinh = await _dbContext.Por_HoSo_Buoc_QuyTrinhs.FirstOrDefaultAsync(o => o.IDHoSo == HoSoQuyTrinh.IDHoSo && o.IDQuyTrinh == HoSoQuyTrinh.IDQuyTrinh && o.IDBuocQuyTrinh == HoSoQuyTrinh.IDBuocQuyTrinh);
            
            HoSoBuocQuyTrinh.NoiDungTamDung = NoiDung;
            if(TieuChi.HasValue)
            {               
                HoSoBuocQuyTrinh.TieuChiTamDung = TieuChi.Value;
            }    
            HoSoBuocQuyTrinh.NgayBatDauTamDung = DateTimeOffsetNow;
            HoSoBuocQuyTrinh.NgayKetThucTamDung = null;
            HoSoBuocQuyTrinh.NguoiTamDung = UserName;
            HoSoBuocQuyTrinh.IDTrangThai_TamDung = HoSoBuocQuyTrinh.IDTrangThai;
            HoSoBuocQuyTrinh.IDTrangThai = trangthai_tamdung.Id;

            await SendMessageHoSoDiaChinh(Id, SmsConfig.Mau_Sms_HSDVC_TamDung.ToString(), EmailConfig.Mau_Email_HSDVC_TamDung.ToString());
            await _dbContext.SaveChangesAsync();
            return Id;
        }
        public async Task<Guid> TraNguocLai(Guid Id, string NoiDung, string UserName)
        {
            var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone);
            var DsBuocQuyTrinh = await (from x in _dbContext.Por_BuocQuyTrinhs
                                        join y in _dbContext.Por_QuyTrinhs on x.IDQuyTrinh equals y.Id
                                        join z in _dbContext.Por_HoSo_QuyTrinhs on x.IDQuyTrinh equals z.IDQuyTrinh
                                        where z.IDHoSo == Id && x.ThuocThoiGianThucHien == y.ThoiGianThucHien
                                        select new { x.Id, x.ThuTuBuoc }).OrderBy(o => o.ThuTuBuoc).ToListAsync();
            var hosoquytrinh = await _dbContext.Por_HoSo_QuyTrinhs.Where(o => o.IDHoSo == Id).FirstAsync();
            Guid id_buocquytrinhcurrent = hosoquytrinh.IDBuocQuyTrinh;            
            Guid id_buocquytrinhprev = Guid.Empty;            
            for (var i = 0; i < DsBuocQuyTrinh.Count; i++)
            {
                if (DsBuocQuyTrinh[i].Id == id_buocquytrinhcurrent)
                {                    
                    if (i >= 1)
                    {
                        id_buocquytrinhprev = DsBuocQuyTrinh[i - 1].Id;
                    }
                }
            }
            var trangthais = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo).ToListAsync();
            var trangthai_tranguoclai = trangthais.Where(o => o.Code == TrangThaiQuyTrinh.tranguoclai.ToString()).First();
            var trangthai_choxuly = trangthais.Where(o => o.Code == TrangThaiQuyTrinh.choxuly.ToString()).First();
            var hosobuocquytrinh_prev = await _dbContext.Por_HoSo_Buoc_QuyTrinhs.Where(o => o.IDHoSo == Id && o.IDQuyTrinh == hosoquytrinh.IDQuyTrinh && o.IDBuocQuyTrinh == id_buocquytrinhprev).FirstAsync();
            hosoquytrinh.NguoiXuLy = hosobuocquytrinh_prev.NguoiXuLy;
            hosoquytrinh.IDBuocQuyTrinh = id_buocquytrinhprev;
            hosobuocquytrinh_prev.NguoiTraNguocLai = UserName;
            hosobuocquytrinh_prev.NoiDungTraNguocLai = NoiDung;
            hosobuocquytrinh_prev.NgayTraNguocLai = DateTimeOffsetNow;            
            hosobuocquytrinh_prev.IDTrangThai = trangthai_tranguoclai.Id;
            //            
            var hosobuocquytrinh_current = await _dbContext.Por_HoSo_Buoc_QuyTrinhs.Where(o => o.IDHoSo == Id && o.IDQuyTrinh == hosoquytrinh.IDQuyTrinh && o.IDBuocQuyTrinh == id_buocquytrinhcurrent).FirstOrDefaultAsync();
            if(hosobuocquytrinh_current != null)
            {
                hosobuocquytrinh_prev.IDTrangThai = trangthai_choxuly.Id;
            }            
            //
            var hoso = _dbContext.Por_HoSoNguoiNops.Where(o => o.Id == Id).First();
            hoso.IDTrangThaiHS = trangthai_tranguoclai.Id;
            await SendMessageHoSoDiaChinhTraNguocLai(Id, hosoquytrinh.IDQuyTrinh, id_buocquytrinhprev, SmsConfig.Mau_Sms_HSDVC_TraNguocLai.ToString(), EmailConfig.Mau_Email_HSDVC_TraNguocLai.ToString());
            await UnitOfWork.SaveAsync();
            return hosobuocquytrinh_prev.Id;
        }
        public async Task<Guid> TraKetQua(Guid Id, string NoiDung, string UserName)
        {
            var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone);
            var HoSoQuyTrinh = await _dbContext.Por_HoSo_QuyTrinhs.FirstAsync(o => o.IDHoSo == Id);
            HoSoQuyTrinh.NgayNhanKQ = DateTimeOffsetNow;
            var HoSo = await _dbContext.Por_HoSoNguoiNops.FirstAsync(o => o.Id == Id);
            var trangthai_traketqua = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo && o.Code == TrangThaiQuyTrinh.traketqua.ToString()).FirstAsync();
            HoSo.IDTrangThaiHS = trangthai_traketqua.Id;
            var por_HoSo_Buoc_QuyTrinh = await _dbContext.Por_HoSo_Buoc_QuyTrinhs.Where(o => o.IDHoSo == Id && o.IDQuyTrinh == HoSoQuyTrinh.IDQuyTrinh && o.IDBuocQuyTrinh == HoSoQuyTrinh.IDBuocQuyTrinh).FirstAsync();
            por_HoSo_Buoc_QuyTrinh.NgayKetThuc = DateTimeOffsetNow;
            por_HoSo_Buoc_QuyTrinh.NguoiXuLy = UserName;
            por_HoSo_Buoc_QuyTrinh.NoiDungXuLy = NoiDung;
            string content = "Hồ sơ mã: " + HoSo.SoHoSo + " đã có kết quả vui lòng lên 1 cửa để nhận, hoặc HS sẽ được gửi tại nhà nếu đăng ký nhận hồ sơ tại nhà";
            await SendMessageHoSoDiaChinh(Id, SmsConfig.Mau_Sms_HSDVC_TraKetQua.ToString(), EmailConfig.Mau_Email_HSDVC_TraKetQua.ToString());
            await UnitOfWork.SaveAsync();
            return por_HoSo_Buoc_QuyTrinh.Id;
        }
        public async Task HuyQuyTrinh(Guid Id, string UserName, string NoiDungHuy)
        {
            var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone);
            var dsTrangThai = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo).ToListAsync();
            var HoSo = await _dbContext.Por_HoSoNguoiNops.Where(o => o.Id == Id).FirstAsync();
            var trangthai_huy = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo && o.Code == TrangThaiQuyTrinh.huy.ToString()).FirstAsync();
            HoSo.IDTrangThaiHS = trangthai_huy.Id;
            var HoSoQuyTrinh = await _dbContext.Por_HoSo_QuyTrinhs.Where(o => o.IDHoSo == Id).FirstAsync();
            HoSoQuyTrinh.NgayHuy = DateTimeOffsetNow;
            HoSoQuyTrinh.NguoiHuy = UserName;
            HoSoQuyTrinh.NoiDungHuy = NoiDungHuy;
            var HoSoBuocQuyTrinhCurrent = await _dbContext.Por_HoSo_Buoc_QuyTrinhs.Where(o => o.IDHoSo == HoSoQuyTrinh.IDHoSo && o.IDQuyTrinh == HoSoQuyTrinh.IDQuyTrinh && o.IDBuocQuyTrinh == HoSoQuyTrinh.IDBuocQuyTrinh).FirstOrDefaultAsync();
            if (HoSoBuocQuyTrinhCurrent != null)
            {
                HoSoBuocQuyTrinhCurrent.IDTrangThai = trangthai_huy.Id;
                HoSoBuocQuyTrinhCurrent.NguoiXuLy = UserName;
            }
            await SendMessageHoSoDiaChinh(Id, SmsConfig.Mau_Sms_HSDVC_Huy.ToString(), EmailConfig.Mau_Email_HSDVC_Huy.ToString());
            await UnitOfWork.SaveAsync();
        }
        public async Task GuiHoSo(Guid Id, string UserName)
        {
            var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone);
            var HoSoQuyTrinh = await _dbContext.Por_HoSo_QuyTrinhs.FirstAsync(o => o.IDHoSo == Id);
            HoSoQuyTrinh.NgayGuiHoSo = DateTimeOffsetNow;
            HoSoQuyTrinh.NguoiGui = UserName;
            var HoSo = await _dbContext.Por_HoSoNguoiNops.FirstAsync(o => o.Id == Id);
            var trangthai_dangvanchuyen = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo && o.Code == TrangThaiQuyTrinh.dangvanchuyen.ToString()).FirstAsync();
            HoSo.IDTrangThaiHS = trangthai_dangvanchuyen.Id;
            var HoSoBuocQuyTrinh = await _dbContext.Por_HoSo_Buoc_QuyTrinhs.FirstOrDefaultAsync(o => o.IDHoSo == HoSoQuyTrinh.IDHoSo && o.IDQuyTrinh == HoSoQuyTrinh.IDQuyTrinh && o.IDBuocQuyTrinh == HoSoQuyTrinh.IDBuocQuyTrinh);
            HoSoBuocQuyTrinh.IDTrangThai = trangthai_dangvanchuyen.Id;
            await SendMessageHoSoDiaChinh(Id, SmsConfig.Mau_Sms_HSDVC_GuiHoSo.ToString(), EmailConfig.Mau_Email_HSDVC_GuiHoSo.ToString());
            await SendMessageDonViVanChuyen(Id);
            await _dbContext.SaveChangesAsync();
        }
        public async Task TiepNhanHoSo(Guid Id, string UserName)
        {
            var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone);
            var HoSoQuyTrinh = await _dbContext.Por_HoSo_QuyTrinhs.FirstAsync(o => o.IDHoSo == Id);
            HoSoQuyTrinh.NguoiXuLy = UserName;
            if (HoSoQuyTrinh.NgayTiepNhan == null)
            {
                HoSoQuyTrinh.NguoiTiepNhan = UserName;
                HoSoQuyTrinh.NgayTiepNhan = DateTimeOffsetNow;
                HoSoQuyTrinh.NgayDuKienNhanKQ = DateTimeOffsetNow.AddDays(HoSoQuyTrinh.ThoiGianThucHien);
                await SendMessageHoSoDiaChinhTiepNhan(Id, HoSoQuyTrinh.IDBuocQuyTrinh, SmsConfig.Mau_Sms_HSDVC_TiepNhan.ToString(), EmailConfig.Mau_Email_HSDVC_TiepNhan.ToString());
            }
            var HoSo = await _dbContext.Por_HoSoNguoiNops.FirstAsync(o => o.Id == Id);
            var trangthai_dangxuly = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo && o.Code == TrangThaiQuyTrinh.dangxuly.ToString()).FirstAsync();
            HoSo.IDTrangThaiHS = trangthai_dangxuly.Id;
            var HoSoBuocQuyTrinh = await _dbContext.Por_HoSo_Buoc_QuyTrinhs.FirstOrDefaultAsync(o => o.IDHoSo == HoSoQuyTrinh.IDHoSo && o.IDQuyTrinh == HoSoQuyTrinh.IDQuyTrinh && o.IDBuocQuyTrinh == HoSoQuyTrinh.IDBuocQuyTrinh);
            if (HoSoBuocQuyTrinh == null)
            {
                HoSoBuocQuyTrinh = new Model.Por_HoSo_Buoc_QuyTrinh();
                HoSoBuocQuyTrinh.Id = Guid.NewGuid();
                HoSoBuocQuyTrinh.IDHoSo = HoSoQuyTrinh.IDHoSo;
                HoSoBuocQuyTrinh.IDQuyTrinh = HoSoQuyTrinh.IDQuyTrinh;
                HoSoBuocQuyTrinh.IDBuocQuyTrinh = HoSoQuyTrinh.IDBuocQuyTrinh;
                HoSoBuocQuyTrinh.IDTrangThai = trangthai_dangxuly.Id;
                HoSoBuocQuyTrinh.NguoiXuLy = UserName;
                HoSoBuocQuyTrinh.NgayBatDau = DateTimeOffsetNow;
                HoSoBuocQuyTrinh.NgayKetThuc = null;
                HoSoBuocQuyTrinh.CreatedDateTime = DateTimeOffsetNow;
                await _dbContext.Por_HoSo_Buoc_QuyTrinhs.AddAsync(HoSoBuocQuyTrinh);
            }
            else
            {
                HoSoBuocQuyTrinh.IDTrangThai = trangthai_dangxuly.Id;
                HoSoBuocQuyTrinh.NgayBatDau = DateTimeOffsetNow;
                HoSoBuocQuyTrinh.NgayKetThuc = null;
                HoSoBuocQuyTrinh.UpdatedDateTime = DateTimeOffsetNow;
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task ThanhToanHoSo(Guid Id, string UserName)
        {
            var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone);
            var HoSoQuyTrinh = await _dbContext.Por_HoSo_QuyTrinhs.FirstAsync(o => o.IDHoSo == Id);
            HoSoQuyTrinh.NguoiXuLy = UserName;
            if (HoSoQuyTrinh.NgayTiepNhan == null)
            {
                HoSoQuyTrinh.NguoiTiepNhan = UserName;
                HoSoQuyTrinh.NgayTiepNhan = DateTimeOffsetNow;
                HoSoQuyTrinh.NgayDuKienNhanKQ = DateTimeOffsetNow.AddDays(HoSoQuyTrinh.ThoiGianThucHien);
                await SendMessageHoSoDiaChinh(Id, SmsConfig.Mau_Sms_HSDVC_ThanhToan.ToString(), EmailConfig.Mau_Email_HSDVC_ThanhToan.ToString());
            }
            var HoSo = await _dbContext.Por_HoSoNguoiNops.FirstAsync(o => o.Id == Id);
            var trangthai_dangxuly = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo && o.Code == TrangThaiQuyTrinh.dangxuly.ToString()).FirstAsync();
            HoSo.IDTrangThaiHS = trangthai_dangxuly.Id;
            HoSo.ThanhToan = true;
            var HoSoBuocQuyTrinh = await _dbContext.Por_HoSo_Buoc_QuyTrinhs.FirstOrDefaultAsync(o => o.IDHoSo == HoSoQuyTrinh.IDHoSo && o.IDQuyTrinh == HoSoQuyTrinh.IDQuyTrinh && o.IDBuocQuyTrinh == HoSoQuyTrinh.IDBuocQuyTrinh);
            if (HoSoBuocQuyTrinh == null)
            {
                HoSoBuocQuyTrinh = new Model.Por_HoSo_Buoc_QuyTrinh();
                HoSoBuocQuyTrinh.Id = Guid.NewGuid();
                HoSoBuocQuyTrinh.IDHoSo = HoSoQuyTrinh.IDHoSo;
                HoSoBuocQuyTrinh.IDQuyTrinh = HoSoQuyTrinh.IDQuyTrinh;
                HoSoBuocQuyTrinh.IDBuocQuyTrinh = HoSoQuyTrinh.IDBuocQuyTrinh;
                HoSoBuocQuyTrinh.IDTrangThai = trangthai_dangxuly.Id;
                HoSoBuocQuyTrinh.NguoiXuLy = UserName;
                HoSoBuocQuyTrinh.NgayBatDau = DateTimeOffsetNow;
                HoSoBuocQuyTrinh.NgayKetThuc = null;
                HoSoBuocQuyTrinh.CreatedDateTime = DateTimeOffsetNow;
                await _dbContext.Por_HoSo_Buoc_QuyTrinhs.AddAsync(HoSoBuocQuyTrinh);
            }
            else
            {
                HoSoBuocQuyTrinh.IDTrangThai = trangthai_dangxuly.Id;
                HoSoBuocQuyTrinh.NgayBatDau = DateTimeOffsetNow;
                HoSoBuocQuyTrinh.NgayKetThuc = null;
                HoSoBuocQuyTrinh.UpdatedDateTime = DateTimeOffsetNow;
            }
            await _dbContext.SaveChangesAsync();
        }
        private async Task SendMessageDonViVanChuyen(Guid IDHoSo)
        {
            try
            {
                string template_send_message_insert = "INSERT INTO public.por_smssend (mobile, email, content, contentemail, status, statusmail, time_create, time_send, type) VALUES";
                string template_send_message_value = "('{0}', '{1}', '{2}', '{3}', {4}, '{5}', '{6}', '{7}', '{8}')";
                Guid valueSms, valueEmail;
                var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone);
                var configSendMessage = await _dbContext.Sys_Configs.Where(o => o.Type == Core.Enums.ConfigType.DonViVanChuyen).ToListAsync();
                var configTiepNhanHoSo_Sms = configSendMessage.Where(o => o.Code == DonViVanChuyenConfig.MauSms.ToString()).FirstOrDefault();
                var configTiepNhanHoSo_Email = configSendMessage.Where(o => o.Code == DonViVanChuyenConfig.MauEmail.ToString()).FirstOrDefault();
                var configTiepNhanHoSo_GuiSms = configSendMessage.Where(o => o.Code == DonViVanChuyenConfig.SmsGui.ToString()).FirstOrDefault();
                var configTiepNhanHoSo_GuiEmail = configSendMessage.Where(o => o.Code == DonViVanChuyenConfig.EmailGui.ToString()).FirstOrDefault();
                if (configTiepNhanHoSo_Sms == null && configTiepNhanHoSo_Email == null)
                    return;
                if (configTiepNhanHoSo_GuiSms == null && configTiepNhanHoSo_GuiEmail == null)
                    return;
                if (Guid.TryParse(configTiepNhanHoSo_Sms.Value, out valueSms)) { }
                if (Guid.TryParse(configTiepNhanHoSo_Email.Value, out valueEmail)) { }
                if (valueSms == Guid.Empty && valueEmail == Guid.Empty)
                    return;
                #region Query
                var item = await (from x in _dbContext.Por_HoSoNguoiNops
                                  join y in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals y.IDHoSo
                                  join z in _dbContext.Por_QuyTrinhs on y.IDQuyTrinh equals z.Id
                                  join a in _dbContext.Por_LinhVucs on z.IDLinhVuc equals a.Id
                                       into sub_Por_LinhVucs
                                  from aa in sub_Por_LinhVucs.DefaultIfEmpty()
                                  select new SendMessage()
                                  {
                                      Id = x.Id,
                                      MaHoSo = x.SoHoSo,
                                      HoVaTen = x.HoTen,
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
                    phone = configTiepNhanHoSo_GuiSms.Value;
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
                    email = configTiepNhanHoSo_GuiEmail.Value;
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
                DateTimeOffsetNow.ToString("dd/MM/yyyy HH:mm:ss"), "", "GuiVanChuyen"));
            }
            catch (Exception ex) { }
        }
        private async Task SendMessageHoSoDiaChinh(Guid IDHoSo, string TypeSms, string TypeEmail)
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
                var item = await (from x in _dbContext.Por_HoSoNguoiNops
                                  join y in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals y.IDHoSo
                                  join z in _dbContext.Por_QuyTrinhs on y.IDQuyTrinh equals z.Id
                                  join a in _dbContext.Por_LinhVucs on z.IDLinhVuc equals a.Id
                                       into sub_Por_LinhVucs
                                  from aa in sub_Por_LinhVucs.DefaultIfEmpty()
                                  select new SendMessage()
                                  {
                                      Id = x.Id,
                                      MaHoSo = x.SoHoSo,
                                      HoVaTen = x.HoTen,
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
                DateTimeOffsetNow.ToString("dd/MM/yyyy HH:mm:ss"), "", "HSDC"));
            }
            catch (Exception ex) { }
        }
        private async Task SendMessageHoSoDiaChinhTiepTuc(Guid IDHoSo, string NguoiXuLy, string TypeSms, string TypeEmail)
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
                var item = await (from x in _dbContext.Por_HoSoNguoiNops
                                  join y in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals y.IDHoSo
                                  join z in _dbContext.Por_QuyTrinhs on y.IDQuyTrinh equals z.Id
                                  join a in _dbContext.Por_LinhVucs on z.IDLinhVuc equals a.Id
                                       into sub_Por_LinhVucs
                                  from aa in sub_Por_LinhVucs.DefaultIfEmpty()
                                  select new SendMessage()
                                  {
                                      Id = x.Id,
                                      MaHoSo = x.SoHoSo,
                                      HoVaTen = x.HoTen,
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
                DateTimeOffsetNow.ToString("dd/MM/yyyy HH:mm:ss"), "", "HSDC"));
                if(!string.IsNullOrEmpty(NguoiXuLy))
                {
                    var user = await _dbContext.Sys_Users.Where(o => o.UserName == NguoiXuLy).FirstOrDefaultAsync();
                    _dbContext.Database.ExecuteSqlRaw(template_send_message_insert
                    + String.Format(template_send_message_value, user.Phone, user.Email, content_phone, content_email, 0, 0,
                    DateTimeOffsetNow.ToString("dd/MM/yyyy HH:mm:ss"), "", "HSDC"));
                }
            }
            catch (Exception ex) { }
        }
        private async Task SendMessageHoSoDiaChinhTiepNhan(Guid IDHoSo, Guid IDBuocQuyTrinh, string TypeSms, string TypeEmail)
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
                var item = await (from x in _dbContext.Por_HoSoNguoiNops
                                  join y in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals y.IDHoSo
                                  join z in _dbContext.Por_QuyTrinhs on y.IDQuyTrinh equals z.Id
                                  join a in _dbContext.Por_LinhVucs on z.IDLinhVuc equals a.Id
                                       into sub_Por_LinhVucs
                                  from aa in sub_Por_LinhVucs.DefaultIfEmpty()
                                  select new SendMessage()
                                  {
                                      Id = x.Id,
                                      MaHoSo = x.SoHoSo,
                                      HoVaTen = x.HoTen,
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
                DateTimeOffsetNow.ToString("dd/MM/yyyy HH:mm:ss"), "", "HSDC"));
                #region BuocQuyTrinh
                var buocquytrinh = await _dbContext.Por_BuocQuyTrinhs.FirstOrDefaultAsync(o => o.Id == IDBuocQuyTrinh);
                if (buocquytrinh != null)
                {
                    string[] SendUsers = buocquytrinh.IDsNguoiDungThamGia.Split(",");
                    bool guiSms = buocquytrinh.GuiSms;
                    Guid? idMauSms = buocquytrinh.IDMauSms;
                    bool guiEmail = buocquytrinh.GuiEmail;
                    Guid? idMauEmail = buocquytrinh.IDMauEmail;
                    if (!guiSms && !guiEmail)
                        return;
                    string sql_value = string.Empty;
                    #region Sms
                    templateSms = await _dbContext.Por_TemplateSmss.Where(o => o.Id == idMauSms).FirstOrDefaultAsync();
                    if (guiSms && templateSms != null)
                    {
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
                    templateEmail = await _dbContext.Por_TemplateEmails.Where(o => o.Id == idMauEmail).FirstOrDefaultAsync();
                    if (guiEmail && templateEmail != null)
                    {
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
                    for (var i = 0; i < SendUsers.Length; i++)
                    {
                        var user = await _dbContext.Sys_Users.Where(o => o.Id == Guid.Parse(SendUsers[i])).FirstOrDefaultAsync();
                        if (user == null) continue;
                        phone = ""; email = "";
                        if (guiSms) phone = user.Phone;
                        if (guiEmail) email = user.Email;
                        sql_value += String.Format(template_send_message_value, phone, email, content_phone, content_email, 0, 0,
                                        DateTimeOffsetNow.ToString("dd/MM/yyyy HH:mm:ss"), "", "HSDC");
                        if (i != SendUsers.Length - 1)
                            sql_value += ",";
                    }
                    _dbContext.Database.ExecuteSqlRaw(template_send_message_insert + sql_value);
                }
                #endregion
            }
            catch (Exception ex) { }
        }
        private async Task SendMessageHoSoDiaChinhTraNguocLai(Guid IDHoSo, Guid IDQuyTrinh, Guid IDBuocQuyTrinh, string TypeSms, string TypeEmail)
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
                var item = await (from x in _dbContext.Por_HoSoNguoiNops
                                  join y in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals y.IDHoSo
                                  join z in _dbContext.Por_QuyTrinhs on y.IDQuyTrinh equals z.Id
                                  join a in _dbContext.Por_LinhVucs on z.IDLinhVuc equals a.Id
                                       into sub_Por_LinhVucs
                                  from aa in sub_Por_LinhVucs.DefaultIfEmpty()
                                  select new SendMessage()
                                  {
                                      Id = x.Id,
                                      MaHoSo = x.SoHoSo,
                                      HoVaTen = x.HoTen,
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
                #region BuocQuyTrinh
                var buocquytrinhPrev = await _dbContext.Por_HoSo_Buoc_QuyTrinhs.FirstOrDefaultAsync(o => o.IDHoSo == IDHoSo && o.IDQuyTrinh == IDQuyTrinh && o.IDBuocQuyTrinh == IDBuocQuyTrinh);
                if (buocquytrinhPrev != null)
                {
                    string[] SendUsers = new string[1] { buocquytrinhPrev.NguoiXuLy};

                    string sql_value = string.Empty;
                    #region Sms
                    var templateSms = await _dbContext.Por_TemplateSmss.Where(o => o.Id == valueSms).FirstOrDefaultAsync();
                    if (templateSms != null)
                    {
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
                    for (var i = 0; i < SendUsers.Length; i++)
                    {
                        var user = await _dbContext.Sys_Users.Where(o => o.UserName == SendUsers[i]).FirstOrDefaultAsync();
                        if (user == null) continue;
                        phone = ""; email = "";
                        if (templateSms != null) phone = user.Phone;
                        if (templateEmail != null) email = user.Email;
                        sql_value += String.Format(template_send_message_value, phone, email, content_phone, content_email, 0, 0,
                                        DateTimeOffsetNow.ToString("dd/MM/yyyy HH:mm:ss"), "", "HSDC");
                        if (i != SendUsers.Length - 1)
                            sql_value += ",";
                    }
                    _dbContext.Database.ExecuteSqlRaw(template_send_message_insert + sql_value);
                }
                #endregion
            }
            catch (Exception ex) { }
        }
        private async Task SendMessageHoSoDiaChinh(Guid IDHoSo, string[] SendUsers, string NguoiXuLy, bool guiSms, Guid? idMauSms, bool guiEmail, Guid? idMauEmail)
        {
            try
            {
                if (!guiSms && !guiEmail)
                    return;
                var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone);
                string template_send_message_insert = "INSERT INTO public.por_smssend (mobile, email, content, contentemail, status, statusmail, time_create, time_send, type) VALUES";
                string template_send_message_value = "('{0}', '{1}', '{2}', '{3}', {4}, '{5}', '{6}', '{7}', '{8}')";
                #region Query
                var item = await (from x in _dbContext.Por_HoSoNguoiNops
                                  join y in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals y.IDHoSo
                                  join z in _dbContext.Por_QuyTrinhs on y.IDQuyTrinh equals z.Id
                                  join a in _dbContext.Por_LinhVucs on z.IDLinhVuc equals a.Id
                                       into sub_Por_LinhVucs
                                  from aa in sub_Por_LinhVucs.DefaultIfEmpty()
                                  select new SendMessage()
                                  {
                                      Id = x.Id,
                                      MaHoSo = x.SoHoSo,
                                      HoVaTen = x.HoTen,
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
                var templateSms = await _dbContext.Por_TemplateSmss.Where(o => o.Id == idMauSms).FirstOrDefaultAsync();
                if (guiSms && templateSms != null)
                {
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
                var templateEmail = await _dbContext.Por_TemplateEmails.Where(o => o.Id == idMauEmail).FirstOrDefaultAsync();
                if (guiEmail && templateEmail != null)
                {
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
                string sql_value = string.Empty;
                if (!string.IsNullOrEmpty(NguoiXuLy))
                {
                    SendUsers = new string[] { NguoiXuLy };
                }
                for (var i = 0;i < SendUsers.Length;i++)
                {
                    var user = new Model.Sys_User();
                    if (!string.IsNullOrEmpty(NguoiXuLy))
                    {
                        user = await _dbContext.Sys_Users.Where(o => o.UserName == SendUsers[i]).FirstOrDefaultAsync();
                    }
                    else
                    {
                        user = await _dbContext.Sys_Users.Where(o => o.Id == Guid.Parse(SendUsers[i])).FirstOrDefaultAsync();
                    }
                    if (user == null) continue;
                    phone = "";  email = "";
                    if (guiSms) phone = user.Phone;
                    if (guiEmail) email = user.Email;
                    sql_value += String.Format(template_send_message_value, phone, email, content_phone, content_email, 0, 0,
                                    DateTimeOffsetNow.ToString("dd/MM/yyyy HH:mm:ss"), "", "HSDC");
                    if (i != SendUsers.Length - 1)
                        sql_value += ",";
                }
                _dbContext.Database.ExecuteSqlRaw(template_send_message_insert + sql_value);
            }
            catch (Exception ex) { }
        }
        public async Task<List<DsBuocQuyTrinhHoSo>> DsBuocQuyTrinhHoSo(Guid Id)
        {
            var dsbuocquytrinh = await (from x in _dbContext.Por_BuocQuyTrinhs
                                join y in _dbContext.Por_QuyTrinhs on x.IDQuyTrinh equals y.Id
                                join z in _dbContext.Por_HoSo_QuyTrinhs on x.IDQuyTrinh equals z.IDQuyTrinh                                
                                join a in _dbContext.Por_ChucNang_BuocQuyTrinhs on x.IDChucNangBuocQuyTrinh equals a.Id
                                where z.IDHoSo == Id //&& x.ThuocThoiGianThucHien == y.ThoiGianThucHien
                                select new DsBuocQuyTrinhHoSo { 
                                    Id = x.Id, 
                                    IdQuyTrinh = x.IDQuyTrinh,                                          
                                    TenBuoc = x.Ten, 
                                    ThuTuBuoc = x.ThuTuBuoc, 
                                    TenChucNang = a.Ten, 
                                    ThoiGianThucHien = x.ThoiGianThucHien, 
                                    NguoiDungThamGia = x.IDsNguoiDungThamGia,
                                    NguoiGui = z.NguoiGui,
                                    NgayGuiHoSo = (z.NgayGuiHoSo != null ? z.NgayGuiHoSo.Value.ToString("dd/MM/yyyy HH:mm:ss") : "")
                                    //NguoiXuLy = bb != null ? bb.NguoiXuLy : "",
                                    //NoiDungXuLy = bb != null ? bb.NoiDungXuLy : "",
                                    //NguoiTraLai = bb != null ? bb.NguoiTraNguocLai : "",
                                    //NoiDungTraLai = bb != null ? bb.NoiDungTraNguocLai : "",
                                    //NgayTraLai = bb != null ? bb.NgayTraNguocLai.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",
                                    //NgayBatDauXuLy = bb != null ? bb.NgayBatDau.ToString("dd/MM/yyyy HH:mm:ss") : "",
                                    //NgayKetThucXuLy = bb != null ? bb.NgayKetThuc.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",
                                }).OrderBy(o => o.ThuTuBuoc).ToListAsync();
            if(dsbuocquytrinh.Count > 0)
            {
                var trangthais = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo).ToListAsync();
                var hoso_buocquytrinh = await _dbContext.Por_HoSo_Buoc_QuyTrinhs.Where(o => o.IDHoSo == Id && o.IDQuyTrinh == dsbuocquytrinh[0].IdQuyTrinh).ToListAsync();
                for(var i = 0;i < hoso_buocquytrinh.Count;i++)
                {
                    for(var j = 0;j < dsbuocquytrinh.Count;j++)
                    {
                        if (hoso_buocquytrinh[i].IDBuocQuyTrinh == dsbuocquytrinh[j].Id)
                        {
                            foreach(var trangthai in trangthais)
                            {
                                if(trangthai.Id == hoso_buocquytrinh[i].IDTrangThai)
                                {
                                    dsbuocquytrinh[j].TrangThai = trangthai.Name;
                                    break;
                                }    
                            }            
                            dsbuocquytrinh[j].IdHoSoBuocQuyTrinh = hoso_buocquytrinh[i].Id;
                            //tamdung
                            dsbuocquytrinh[j].NguoiTamDung = hoso_buocquytrinh[i].NguoiTamDung;
                            dsbuocquytrinh[j].NoiDungTamDung = hoso_buocquytrinh[i].NoiDungTamDung;
                            dsbuocquytrinh[j].TieuChiTamDung = hoso_buocquytrinh[i].TieuChiTamDung;
                            dsbuocquytrinh[j].NgayBatDauTamDung = (hoso_buocquytrinh[i].NgayBatDauTamDung.HasValue ? hoso_buocquytrinh[i].NgayBatDauTamDung.Value.ToString("dd/MM/yyyy HH:mm:ss") : "");
                            dsbuocquytrinh[j].NgayKetThucTamDung = (hoso_buocquytrinh[i].NgayKetThucTamDung.HasValue ? hoso_buocquytrinh[i].NgayKetThucTamDung.Value.ToString("dd/MM/yyyy HH:mm:ss") : "");
                            //
                            dsbuocquytrinh[j].NguoiXuLy = hoso_buocquytrinh[i].NguoiXuLy;
                            dsbuocquytrinh[j].NoiDungXuLy = hoso_buocquytrinh[i].NoiDungXuLy;
                            dsbuocquytrinh[j].NguoiTraLai = hoso_buocquytrinh[i].NguoiTraNguocLai;
                            dsbuocquytrinh[j].NoiDungTraLai = hoso_buocquytrinh[i].NoiDungTraNguocLai;
                            dsbuocquytrinh[j].NgayTraLai = hoso_buocquytrinh[i].NgayTraNguocLai != null ? hoso_buocquytrinh[i].NgayTraNguocLai.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                            dsbuocquytrinh[j].NgayBatDauXuLy = hoso_buocquytrinh[i].NgayBatDau.ToString("dd/MM/yyyy HH:mm:ss");
                            dsbuocquytrinh[j].NgayKetThucXuLy = hoso_buocquytrinh[i].NgayKetThuc != null ? hoso_buocquytrinh[i].NgayKetThuc.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";                            
                            if (hoso_buocquytrinh[i].NgayKetThuc.HasValue)// tính trễ hạn
                            {
                                var thoigianxuly_subtract = hoso_buocquytrinh[i].NgayKetThuc.Value.Subtract(hoso_buocquytrinh[i].NgayBatDau);
                                var thoigianxuly_quydinh_gio = dsbuocquytrinh[i].ThoiGianThucHien * MockData.QuyDinhGioLamViec.SoGioLamViecHanhChinh;
                                var thoigianxyly_gio = thoigianxuly_subtract.TotalHours - (((int)thoigianxuly_subtract.TotalDays + 1) * MockData.QuyDinhGioLamViec.SoGioLamViecHanhChinh);
                                if(thoigianxyly_gio > thoigianxuly_quydinh_gio)
                                {
                                    var thoigianxyly = TimeSpan.FromHours(thoigianxyly_gio - thoigianxuly_quydinh_gio);
                                    dsbuocquytrinh[j].TreHan += "Trễ hạn xử lý: ";
                                    dsbuocquytrinh[j].TreHan += thoigianxyly.Days != 0 ? thoigianxyly.Days + " ngày " : "";
                                    dsbuocquytrinh[j].TreHan += thoigianxyly.Hours + " giờ ";
                                    dsbuocquytrinh[j].TreHan += thoigianxyly.Minutes + " phút ";
                                }    
                            }
                            break;
                        }
                    }    
                }    
            }    
            var dsnguoidung = await (from x in _dbContext.Sys_Users
                        join y in _dbContext.Sys_Users_Roles on x.Id equals y.UserId
                        join z in _dbContext.Sys_Roles on y.RoleId equals z.Id
                        join a in _dbContext.Sys_Organizations on y.OrganId equals a.Id
                        select new { Id = x.Id, Name = x.UserName, RoleName = z.Name, OrganName = a.Name }).ToListAsync();
            var current_buocquytrinh = await _dbContext.Por_HoSo_QuyTrinhs.FirstAsync(o => o.IDHoSo == Id);
            foreach (var item_dsbuocquytrinh in dsbuocquytrinh)
            {
                var spitNguoiDungThamGia = item_dsbuocquytrinh.NguoiDungThamGia.Split(",");
                var buildNguoiDungThamGia = "";
                foreach (var item_dsnguoidungthamgia in spitNguoiDungThamGia)
                {
                    foreach (var item_dsnguoidung in dsnguoidung)
                    {
                        if(Guid.Parse(item_dsnguoidungthamgia) == item_dsnguoidung.Id)
                        {
                            buildNguoiDungThamGia += item_dsnguoidung.Name + ", " + item_dsnguoidung.OrganName + ". ";
                            break;
                        }    
                    }
                }    
                item_dsbuocquytrinh.NguoiDungThamGia = buildNguoiDungThamGia;
                if (item_dsbuocquytrinh.Id == current_buocquytrinh.IDBuocQuyTrinh)
                {
                    item_dsbuocquytrinh.DangHoatDong = true;
                }
                else
                {
                    item_dsbuocquytrinh.DangHoatDong = false;
                }               
                item_dsbuocquytrinh.Files = await _dbContext.Por_FileBuocQuyTrinhs
                    .Select(o => new ViewModel.Files.FileView() { Id = o.Id, Name = o.Ten, Url = o.URL, IdBuocQuyTrinh = o.IDBuocQuyTrinh })
                    .Where(o => o.IdBuocQuyTrinh == item_dsbuocquytrinh.IdHoSoBuocQuyTrinh)
                    .ToListAsync();
            }    
            return dsbuocquytrinh;
        }        
        public async Task<LuuHoSoQuyTrinh> LuuHoSoQuyTrinh(LuuHoSoQuyTrinh model)
        {
            var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone);
            if (model.Id != Guid.Empty)
            {
                var entry = await _dbContext.Por_HoSoNguoiNops.FirstAsync();
                ObjectHelpers.Mapping<LuuHoSoQuyTrinh, Model.Por_HoSoNguoiNop>(model, entry, new[] { "Id" });                 
                _dbContext.Entry(entry).CurrentValues.SetValues(entry);
                await _dbContext.SaveChangesAsync();
                return model;
            }    
            //
            var BuocQuyTrinhFirst = await (from x in _dbContext.Por_QuyTrinhs
                                  join y in _dbContext.Por_BuocQuyTrinhs on x.Id equals y.IDQuyTrinh
                                  where x.Id == model.IDQuyTrinh
                                  select new { IDQuyTrinh = x.Id, ThoiGianThucHien = x.ThoiGianThucHien, IDBuocQuyTrinh = y.Id, ThuTuBuoc = y.ThuTuBuoc })
                                  .OrderBy(o => o.ThuTuBuoc)
                                  .FirstAsync();
            //
            var TrangThaiChoXuLy = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo && o.Code == TrangThaiQuyTrinh.choxuly.ToString()).FirstAsync();
            //
            Guid IDBuocQuyTrinh = Guid.NewGuid();
            var modelSave = new Model.Por_HoSoNguoiNop();
            ObjectHelpers.Mapping<LuuHoSoQuyTrinh, Model.Por_HoSoNguoiNop>(model, modelSave);
            modelSave.Id = Guid.NewGuid();
            modelSave.SoHoSo = Guid.NewGuid().ToString();
            modelSave.IDTrangThaiHS = TrangThaiChoXuLy.Id;
            model.Id = modelSave.Id;
            await _dbContext.Por_HoSoNguoiNops.AddAsync(modelSave);            
            Model.Por_HoSo_QuyTrinh por_HoSo_QuyTrinh = new Model.Por_HoSo_QuyTrinh();
            por_HoSo_QuyTrinh.Id = Guid.NewGuid();
            model.IDBuocQuyTrinhDauTien = BuocQuyTrinhFirst.IDBuocQuyTrinh;
            por_HoSo_QuyTrinh.IDHoSo = modelSave.Id;
            por_HoSo_QuyTrinh.IDQuyTrinh = BuocQuyTrinhFirst.IDQuyTrinh;
            por_HoSo_QuyTrinh.IDBuocQuyTrinh = BuocQuyTrinhFirst.IDBuocQuyTrinh;
            por_HoSo_QuyTrinh.ThoiGianThucHien = BuocQuyTrinhFirst.ThoiGianThucHien;
            por_HoSo_QuyTrinh.NgayNop = DateTimeOffsetNow;
            por_HoSo_QuyTrinh.NgayTiepNhan = null;
            por_HoSo_QuyTrinh.NgayNhanKQ = null;
            por_HoSo_QuyTrinh.NgayDuKienNhanKQ = DateTimeOffsetNow.AddDays(BuocQuyTrinhFirst.ThoiGianThucHien);
            await _dbContext.Por_HoSo_QuyTrinhs.AddAsync(por_HoSo_QuyTrinh);
            await UnitOfWork.SaveAsync();
            return model;
        }
        public async Task<ChiTietHoSoQuyTrinh> LayChiTietHoSoQuyTrinh(Guid id)
        {
            var hoso = await _dbContext.Por_HoSoNguoiNops.FirstAsync(o => o.Id == id);
            var quytrinh = await _dbContext.Por_HoSo_QuyTrinhs.FirstAsync(o => o.IDHoSo == id);
            ChiTietHoSoQuyTrinh chiTietHoSo = new ChiTietHoSoQuyTrinh();
            ObjectHelpers.Mapping<Model.Por_HoSoNguoiNop, ChiTietHoSoQuyTrinh>(hoso, chiTietHoSo);
            chiTietHoSo.IDQuyTrinh = quytrinh.IDQuyTrinh;
            chiTietHoSo.FileHoSoNguoiNop = await _dbContext.Por_FileHoSoNguoiNops.Where(o => o.IDHoSoNguoiNop == id).ToListAsync();
            chiTietHoSo.Por_FileHoSo = await _dbContext.Por_FileHoSos.Where(o => o.IDHoSoNguoiNop == id).ToListAsync();
            return chiTietHoSo;
    }    
        public async Task<List<HoSoHuy>> HoSoHuy(string SoHoSo, string UserName)
        {
            var trangthai = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo && o.Code == TrangThaiQuyTrinh.huy.ToString()).FirstAsync();
            var query = from x in _dbContext.Por_HoSoNguoiNops
                        join y in _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo) on x.IDTrangThaiHS equals y.Id
                        join z in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals z.IDHoSo
                        join i in _dbContext.Por_QuyTrinhs on z.IDQuyTrinh equals i.Id
                        where x.IDTrangThaiHS == trangthai.Id
                        select new { x.Id, IdThuTuc = i.Id, i.TenThuTuc, x.SoHoSo, TrangThai = y.Name, z.NgayNop, z.NgayHuy, z.NguoiHuy, z.NoiDungHuy };
            if (!string.IsNullOrEmpty(SoHoSo))
            {
                query = query.Where(o => o.SoHoSo.ToLower().Contains(SoHoSo.ToLower()));
            }
            var permWorkflowIDs = await PermWorkflowIDs(UserName);
            if (permWorkflowIDs != null && permWorkflowIDs.Count > 0)
            {
                query = query.Where(o => permWorkflowIDs.Contains(o.IdThuTuc));
            }
            else
            {
                query = query.Where(o => o.IdThuTuc == Guid.Empty);
            }
            return await query.OrderByDescending(o => o.NgayHuy).Select(o => new HoSoHuy()
            {
                Id = o.Id,
                TenThuTuc = o.TenThuTuc,
                SoHoSo = o.SoHoSo,
                NoiDungHuy = o.NoiDungHuy,
                TrangThai = o.TrangThai,
                NgayNop = o.NgayNop.ToString("dd/MM/yyyy HH:mm:ss"),
                NgayHuy = (o.NgayHuy.HasValue ? o.NgayHuy.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""),
                NguoiHuy = o.NguoiHuy
            }).ToListAsync();
        }
        public async Task<List<HoSoHoanThanh>> HoSoHoanThanh(string SoHoSo)
        {
            //var trangthai = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo && o.Code == TrangThaiQuyTrinh.hoanthanh.ToString()).FirstAsync();
            var query = from x in _dbContext.Por_HoSoNguoiNops
                        join y in _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo) on x.IDTrangThaiHS equals y.Id
                        join z in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals z.IDHoSo
                        join i in _dbContext.Por_QuyTrinhs on z.IDQuyTrinh equals i.Id
                        //where x.IDTrangThaiHS == trangthai.Id
                        select new { x.Id, i.TenThuTuc, x.SoHoSo, TrangThai = y.Name, z.NgayNop };
            if (!string.IsNullOrEmpty(SoHoSo))
            {
                query = query.Where(o => o.SoHoSo.ToLower().Contains(SoHoSo.ToLower()));
            }
            return await query.Select(o => new HoSoHoanThanh()
            {
                Id = o.Id,
                TenThuTuc = o.TenThuTuc,
                SoHoSo = o.SoHoSo,
                TrangThai = o.TrangThai,
                NgayNop = o.NgayNop.ToString("dd/MM/yyyy HH:mm:ss"),
                //NgayHoanThanh = (o.NgayHoanThanh.HasValue ? o.NgayHoanThanh.Value.ToString("dd/MM/yyyy HH:mm:ss") : "")
            }).ToListAsync();
        }        
        public async Task<List<HoSoXuLy>> HoSoXuLy(Guid UserId, string UserName, string SoHoSo)
        {
            var trangthai = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo).ToListAsync();
            var trangthai_choxuly = trangthai.Where(o => o.Code == TrangThaiQuyTrinh.choxuly.ToString()).First();
            var trangthai_dangxuly = trangthai.Where(o => o.Code == TrangThaiQuyTrinh.dangxuly.ToString()).First();            
            var trangthai_tranguoclai = trangthai.Where(o => o.Code == TrangThaiQuyTrinh.tranguoclai.ToString()).First();
            var trangthai_dangvanchuyen = trangthai.Where(o => o.Code == TrangThaiQuyTrinh.dangvanchuyen.ToString()).First();
            var trangthai_tamdung = trangthai.Where(o => o.Code == TrangThaiQuyTrinh.tamdung.ToString()).First();
            //var trangthai_hoanthanh = trangthai.Where(o => o.Code == TrangThaiQuyTrinh.hoanthanh.ToString()).First();
            var query = from x in _dbContext.Por_HoSoNguoiNops
                        join y in _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo) on x.IDTrangThaiHS equals y.Id
                        join z in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals z.IDHoSo
                        join i in _dbContext.Por_BuocQuyTrinhs on z.IDBuocQuyTrinh equals i.Id
                        join j in _dbContext.Por_QuyTrinhs on z.IDQuyTrinh equals j.Id
                        where
                        (x.IDTrangThaiHS == trangthai_choxuly.Id
                        || x.IDTrangThaiHS == trangthai_dangxuly.Id
                        || x.IDTrangThaiHS == trangthai_tamdung.Id
                        || x.IDTrangThaiHS == trangthai_dangvanchuyen.Id
                        || x.IDTrangThaiHS == trangthai_tranguoclai.Id) &&
                        i.IDsNguoiDungThamGia.ToLower().Contains(UserId.ToString().ToLower())
                        select new { x.Id, IdThuTuc = j.Id, j.TenThuTuc, x.ThanhToan, x.SoHoSo, TrangThai = y.Name, x.HoTen, x.Email, x.SoDienThoai, z.NgayNop, z.NgayTiepNhan, z.NguoiXuLy, z.IDQuyTrinh, IDBuocQuyTrinh = i.Id };               
            if (!string.IsNullOrEmpty(SoHoSo))
            {
                query = query.Where(o => o.SoHoSo.ToLower().Contains(SoHoSo.ToLower()));
            }
            //var permWorkflowIDs = await PermWorkflowIDs(UserName);
            //if (permWorkflowIDs != null && permWorkflowIDs.Count > 0)
            //{
            //    query = query.Where(o => permWorkflowIDs.Contains(o.IdThuTuc));
            //}
            //else
            //{
            //    query = query.Where(o => o.IdThuTuc == Guid.Empty);
            //}
            var items = await query.OrderByDescending(o => o.NgayNop).Select(o => new HoSoXuLy()
            {
                Id = o.Id,
                TenThuTuc = o.TenThuTuc,
                IDQuyTrinh = o.IDQuyTrinh,
                IDBuocQuyTrinh = o.IDBuocQuyTrinh,
                SoHoSo = o.SoHoSo,
                TrangThai = o.TrangThai,
                ThanhToan = o.ThanhToan,
                NguoiNop = o.HoTen,
                Email = o.Email,
                SDT = o.SoDienThoai,
                NgayNop = o.NgayNop.ToString("dd/MM/yyyy HH:mm:ss"),
                NgayNopInt = long.Parse(o.NgayNop.ToString("yyyyMMddHHmmss")),
                NgayTiepNhan = (o.NgayTiepNhan.HasValue ? o.NgayTiepNhan.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""),
                NguoiDangTiepNhan = o.NguoiXuLy
            }).ToListAsync();
            var items_filter = new List<HoSoXuLy>();            
            for(var i = 0;i < items.Count;i++)
            {
                //if (string.IsNullOrEmpty(items[i].NgayTiepNhan) && items[i].ThanhToan != null)
                //{
                //    if(!items[i].ThanhToan.Value)
                //    {
                //        items[i].TrangThai = "Chờ thanh toán";
                //    }   
                //}    
               
                if(!string.IsNullOrEmpty(items[i].NguoiDangTiepNhan))
                {
                    if (items[i].NguoiDangTiepNhan == UserName)
                    {
                        items_filter.Add(items[i]);
                    }
                }    
                else
                {
                    items_filter.Add(items[i]);
                }    
            }    
            return items_filter;
        }
        public async Task XoaHoSo(Guid Id)
        {
            var Por_HoSoNguoiNops = await _dbContext.Por_HoSoNguoiNops.FirstOrDefaultAsync(o => o.Id == Id);
            if(Por_HoSoNguoiNops != null)
            {
                _dbContext.Por_HoSoNguoiNops.Remove(Por_HoSoNguoiNops);
            } 
            var Por_ThuatDats = await _dbContext.Por_ThuatDats.FirstOrDefaultAsync(o => o.IDHoSoNguoiNop == Id);
            if(Por_ThuatDats != null)
            {
                _dbContext.Por_ThuatDats.Remove(Por_ThuatDats);
            }
            var Por_HoSo_QuyTrinhs = await _dbContext.Por_HoSo_QuyTrinhs.FirstOrDefaultAsync(o => o.IDHoSo == Id);
            if(Por_HoSo_QuyTrinhs != null)
            {
                _dbContext.Por_HoSo_QuyTrinhs.Remove(Por_HoSo_QuyTrinhs);
            }               
            var Por_HoSo_Buoc_QuyTrinhs = await _dbContext.Por_HoSo_Buoc_QuyTrinhs.FirstOrDefaultAsync(o => o.IDHoSo == Id);
            if(Por_HoSo_Buoc_QuyTrinhs != null)
            {
                _dbContext.Por_HoSo_Buoc_QuyTrinhs.Remove(Por_HoSo_Buoc_QuyTrinhs);
            }
            await _dbContext.SaveChangesAsync();
        }    
        public async Task<List<HoSoTamDung>> HoSoTamDung(string SoHoSo)
        {
            var trangthai = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo && o.Code == TrangThaiQuyTrinh.tamdung.ToString()).FirstAsync();
            var query = from x in _dbContext.Por_HoSoNguoiNops
                        join y in _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo) on x.IDTrangThaiHS equals y.Id
                        join z in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals z.IDHoSo
                        where x.IDTrangThaiHS == trangthai.Id
                        select new { x.Id, x.SoHoSo, TrangThai = y.Name, z.NgayNop, };
            if (!string.IsNullOrEmpty(SoHoSo))
            {
                query = query.Where(o => o.SoHoSo.ToLower().Contains(SoHoSo.ToLower()));
            }
            return await query.Select(o => new HoSoTamDung()
            {
                Id = o.Id,
                SoHoSo = o.SoHoSo,
                TrangThai = o.TrangThai,
                //NgayNop = o.NgayNop.ToString("dd/MM/yyyy HH:mm:ss"),
                //NgayBatDauTamDung = (o.NgayBatDauTamDung.HasValue ? o.NgayBatDauTamDung.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""),
                //NgayKetThucTamDung = (o.NgayKetThucTamDung.HasValue ? o.NgayKetThucTamDung.Value.ToString("dd/MM/yyyy HH:mm:ss") : "")
            }).ToListAsync();
        }
        public async Task<List<HoSoTraKetQua>> HoSoTraKetQua(string SoHoSo, string UserName)
        {
            var trangthai = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo && o.Code == TrangThaiQuyTrinh.traketqua.ToString()).FirstAsync();
            var query = from x in _dbContext.Por_HoSoNguoiNops
                        join y in _dbContext.Sys_Categories on x.IDTrangThaiHS equals y.Id
                        join z in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals z.IDHoSo
                        join i in _dbContext.Por_QuyTrinhs on z.IDQuyTrinh equals i.Id
                        where x.IDTrangThaiHS == trangthai.Id
                        select new { x.Id, IdThuTuc = i.Id, i.TenThuTuc, x.SoHoSo, TrangThai = y.Name, z.NgayNop, z.NgayNhanKQ, z.NguoiTraKetQua };
            if (!string.IsNullOrEmpty(SoHoSo))
            {
                query = query.Where(o => o.SoHoSo.ToLower().Contains(SoHoSo.ToLower()));
            }
            //var permWorkflowIDs = await PermWorkflowIDs(UserName);
            //if (permWorkflowIDs != null && permWorkflowIDs.Count > 0)
            //{
            //    query = query.Where(o => permWorkflowIDs.Contains(o.IdThuTuc));
            //}
            //else
            //{
            //    query = query.Where(o => o.IdThuTuc == Guid.Empty);
            //}
            return await query.OrderByDescending(o => o.NgayNhanKQ).Select(o => new HoSoTraKetQua()
            {
                Id = o.Id,
                TenThuTuc = o.TenThuTuc,
                SoHoSo = o.SoHoSo,
                TrangThai = o.TrangThai,
                NgayNop = o.NgayNop.ToString("dd/MM/yyyy HH:mm:ss"),
                NgayTraKetQua = (o.NgayNhanKQ.HasValue ? o.NgayNhanKQ.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""),
                NguoiTraKetQua = o.NguoiTraKetQua
            }).ToListAsync();
        }
    }
}
