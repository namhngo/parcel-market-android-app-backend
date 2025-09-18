using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Gis.API.Infrastructure;
using Gis.API.Model;
using Gis.Core.Constant;
using Gis.Core.Core;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gis.API.ViewModel.Portal;
using static Gis.API.Infrastructure.Enums;
using Gis.API.ViewModel.SendMessage;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using static Humanizer.On;
using System.Linq.Dynamic.Core;

namespace Gis.API.Service.Por_HoSo
{
    public class Service: Por_HoSo.IService
    {
        private readonly DomainDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;                
        public Service(DomainDbContext dbContext, IDateTimeProvider dateTimeProvider, IUserProvider userService) 
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userService;            
        }
        public async Task<List<ViewModel.BaoCao.HoSoTheoTungLoai>> BaoCaoHoSoTheoTungLoai(ViewModel.BaoCao.SearchHoSoTheoTungLoai search, int? totalLimitItems)
        {
            var query = from x in _dbContext.Por_HoSoNguoiNops
                        join y in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals y.IDHoSo
                        join z in _dbContext.Por_QuyTrinhs on y.IDQuyTrinh equals z.Id
                        join i in _dbContext.Sys_Categories on x.IDTrangThaiHS equals i.Id
                        select new { x.SoHoSo, y.NguoiXuLy, IdQuyTrinh = z.Id, TenQuyTrinh = z.TenThuTuc, x.HoTen, x.SoDienThoai, IDTrangThaiHoSo = i.Id, TrangThaiHoSo = i.Name, y.NgayNop };
            if (search.LoaiHoSo != null && search.LoaiHoSo.Length > 0)
            {
                query = query.Where(o => search.LoaiHoSo.Contains(o.IdQuyTrinh));
            }
            if (search.TrangThaiHoSo != null && search.TrangThaiHoSo.Length > 0)
            {
                query = query.Where(o => search.TrangThaiHoSo.Contains(o.IDTrangThaiHoSo));
            }
            if (search.CanBoXuLy != null && search.CanBoXuLy.Length > 0)
            {
                query = query.Where(o => search.CanBoXuLy.Contains(o.NguoiXuLy));
            }
            if (search.TuNgay.HasValue)
            {
                query = query.Where(o => o.NgayNop >= search.TuNgay);
            }
            if (search.DenNgay.HasValue)
            {
                query = query.Where(o => o.NgayNop <= search.DenNgay);
            }
            if(totalLimitItems.HasValue)
            {
                query = query.Take(totalLimitItems.Value);
            }    
            var items = await query.Select(o => new ViewModel.BaoCao.HoSoTheoTungLoai()
            {
                MaHoSo = o.SoHoSo,
                LoaiDichVu = o.TenQuyTrinh,
                NguoiDan = o.HoTen,
                SoDienThoai = o.SoDienThoai,
                TrangThaiHoSo = o.TrangThaiHoSo,
                NgayNop = o.NgayNop.ToString("dd/MM/yyyy")
            }).ToListAsync();
            return items;
        }
        public async Task<List<ViewModel.BaoCao.ThuaDatDuocTimKiem>> BaoCaoThuaDatDuocTimKiem(ViewModel.BaoCao.SearchThuaDatDuocTimKiem search, int? totalLimitItems)
        {

            var query = _dbContext.Por_LogSearchGiss.AsQueryable();
            var TenPhuongXas = await _dbContext.Por_PhuongXaThiTrans.Where(o => search.XaPhuong.Contains(o.Id)).Select(o => o.Ten).ToListAsync();
            if(search.XaPhuong != null && search.XaPhuong.Length > 0)
            {
                query = query.Where(o => TenPhuongXas.Contains(o.TenPhuongXa));
            }
            if (!string.IsNullOrEmpty(search.SoTo))
            {
                query = query.Where(o => o.To == search.SoTo);
            }
            if (!string.IsNullOrEmpty(search.SoThua))
            {
                query = query.Where(o => o.Thua == search.SoThua);
            }
            if(search.TuNgay != null)
            {
                query = query.Where(o => o.NgayTim >= search.TuNgay);
            }
            if (search.DenNgay != null)
            {
                query = query.Where(o => o.NgayTim <= search.DenNgay);
            }
            if (totalLimitItems.HasValue)
            {
                query = query.Take(totalLimitItems.Value);
            }
            var items = await query.GroupBy(o => new { o.TenPhuongXa, o.To, o.Thua}).Select(o => new ViewModel.BaoCao.ThuaDatDuocTimKiem() { PhuongXa = o.Key.TenPhuongXa, SoTo = o.Key.To, SoThua = o.Key.Thua, LuotTimKiem = o.Where(o => o.NgayTim != null).Count()}).ToListAsync();
            return items;
        }
        public async Task<List<ViewModel.BaoCao.TiepNhanHoSo>> BaoCaoTiepNhanHoSo(ViewModel.BaoCao.SearchTiepNhanHoSo search, int? totalLimitItems)
        {
            var query = from x in _dbContext.Por_HoSoNguoiNops
                        join y in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals y.IDHoSo
                        join z in _dbContext.Por_QuyTrinhs on y.IDQuyTrinh equals z.Id
                        join i in _dbContext.Sys_Categories on x.IDTrangThaiHS equals i.Id
                        select new { x.SoHoSo, y.NguoiXuLy, IdQuyTrinh = z.Id, TenQuyTrinh = z.TenThuTuc, x.HoTen, x.SoDienThoai, IDTrangThaiHoSo = i.Id, TrangThaiHoSo = i.Name, y.NgayNop, y.NgayTiepNhan, y.NgayNhanKQ };
            if (search.LoaiHoSo != null && search.LoaiHoSo.Length > 0)
            {
                query = query.Where(o => search.LoaiHoSo.Contains(o.IdQuyTrinh));
            }
            if (search.TrangThaiHoSo != null && search.TrangThaiHoSo.Length > 0 )
            {
                query = query.Where(o => search.TrangThaiHoSo.Contains(o.IDTrangThaiHoSo));
            }
            if (search.TuNgay.HasValue)
            {
                query = query.Where(o => o.NgayTiepNhan >= search.TuNgay);
            }
            if (search.DenNgay.HasValue)
            {
                query = query.Where(o => o.NgayTiepNhan <= search.DenNgay);
            }
            if (totalLimitItems.HasValue)
            {
                query = query.Take(totalLimitItems.Value);
            }
            var items = await query.Select(o => new ViewModel.BaoCao.TiepNhanHoSo()
            {
                MaHoSo = o.SoHoSo,
                LoaiDichVu = o.TenQuyTrinh,
                NguoiDan = o.HoTen,
                SoDienThoai = o.SoDienThoai,
                TrangThaiHoSo = o.TrangThaiHoSo,
                NgayNop = o.NgayNop.ToString("dd/MM/yyyy"),
                NgayTiepNhan = o.NgayTiepNhan.HasValue ? o.NgayTiepNhan.Value.ToString("dd/MM/yyyy") : "",
                NgayTraKetQua = o.NgayNhanKQ.HasValue ? o.NgayNhanKQ.Value.ToString("dd/MM/yyyy") : ""
            }).ToListAsync();
            return items;
        }
        public async Task<ViewModel.HoSoQuyTrinh.HoSoQuyTrinh> XemChiTietHoSo(Guid Id)
        {
            ViewModel.HoSoQuyTrinh.HoSoQuyTrinh item = new ViewModel.HoSoQuyTrinh.HoSoQuyTrinh();
            var item_hoso = await _dbContext.Por_HoSoNguoiNops.Where(o => o.Id == Id).FirstOrDefaultAsync();
            var item_loaihoso = await (from x in _dbContext.Por_QuyTrinhs
                                       join y in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals y.IDQuyTrinh
                                       select new ViewModel.HoSoQuyTrinh.LoaiHoSo
                                       {
                                           IDQuyTrinh = x.Id,
                                           IDBuocQuyTrinh = y.IDBuocQuyTrinh,
                                           Ma = x.MaThuTuc,
                                           Ten = x.TenThuTuc,
                                           ThoiGianThucHien = x.ThoiGianThucHien + "",
                                           NgayDuKienNhanKQ = y.NgayDuKienNhanKQ.ToString("dd/MM/yyyy")
                                       }).FirstOrDefaultAsync();
            var item_thuadat = await _dbContext.Por_ThuatDats.Where(o => o.IDHoSoNguoiNop == Id).FirstOrDefaultAsync();
            var item_hosodinhkem = await _dbContext.Por_FileHoSoNguoiNops.Where(o => o.IDHoSoNguoiNop == Id)
                                    .Select(o => new ViewModel.HoSoQuyTrinh.FileHoSoNguoiNop() { Id = o.Id, Ten = o.Ten, URL = o.URL, IDFileMauThanhPhanHStrongQT = o.IDFileMauThanhPhanHStrongQT }).ToListAsync();
            //
            var hosobuocquytrinh = await _dbContext.Por_HoSo_Buoc_QuyTrinhs.Where(o => o.IDHoSo == item_hoso.Id && o.IDQuyTrinh == item_loaihoso.IDQuyTrinh && o.IDBuocQuyTrinh == item_loaihoso.IDBuocQuyTrinh).FirstOrDefaultAsync();
            if(hosobuocquytrinh != null)
            {
                var dstrangthai = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo).ToListAsync();
                var trangthai_tamdung = dstrangthai.First(o => o.Code == TrangThaiQuyTrinh.tamdung.ToString());
                if(trangthai_tamdung.Id == hosobuocquytrinh.IDTrangThai)
                {
                    item.NguoiTamDung = hosobuocquytrinh.NguoiTamDung;
                    item.TieuChiTamDung = hosobuocquytrinh.TieuChiTamDung;
                    item.NoiDungTamDung = hosobuocquytrinh.NoiDungTamDung;
                    item.NgayBatDauTamDung = hosobuocquytrinh.NgayBatDauTamDung.HasValue ? hosobuocquytrinh.NgayBatDauTamDung.Value.ToString("dd/MM/yyyy") : "";
                    item.NgayKetThucTamDung = hosobuocquytrinh.NgayKetThucTamDung.HasValue ? hosobuocquytrinh.NgayKetThucTamDung.Value.ToString("dd/MM/yyyy") : "";
                }    
            }    
            //dich vu
            item.loaiHoSo = item_loaihoso;
            //thong tin nguoi nop
            var PhuongXa = await _dbContext.Por_PhuongXaThiTrans.Where(o => o.Id == item_hoso.PhuongXa).FirstOrDefaultAsync();
            var QuanHuyen = await _dbContext.Por_QuanHuyens.Where(o => o.Id == item_hoso.QuanHuyen).FirstOrDefaultAsync();
            var TinhThanhPho = await _dbContext.Por_TinhThanhPhos.Where(o => o.Id == item_hoso.TinhThanhPho).FirstOrDefaultAsync();
            item.thongTinNguoiNop = new ViewModel.HoSoQuyTrinh.ThongTinNguoiNop();
            item.thongTinNguoiNop.Id = item_hoso.Id;
            item.thongTinNguoiNop.SoHoSo = item_hoso.SoHoSo;
            item.thongTinNguoiNop.HoTen = item_hoso.HoTen;
            item.thongTinNguoiNop.SoDienThoai = item_hoso.SoDienThoai;
            item.thongTinNguoiNop.Email = item_hoso.Email;
            item.thongTinNguoiNop.SoNha = item_hoso.SoNha;
            item.thongTinNguoiNop.TenDuong = item_hoso.TenDuong;
            item.thongTinNguoiNop.PhuongXa = PhuongXa.Id;
            item.thongTinNguoiNop.TenPhuongXa = PhuongXa.Ten;
            item.thongTinNguoiNop.QuanHuyen = QuanHuyen.Id;
            item.thongTinNguoiNop.TenQuanHuyen = QuanHuyen.Ten;
            item.thongTinNguoiNop.TinhThanhPho = TinhThanhPho.Id;
            item.thongTinNguoiNop.TenTinhThanhPho = TinhThanhPho.Ten;
            //thong tin thua dat
            PhuongXa = await _dbContext.Por_PhuongXaThiTrans.Where(o => o.Id == item_thuadat.PhuongXa).FirstOrDefaultAsync();
            QuanHuyen = await _dbContext.Por_QuanHuyens.Where(o => o.Id == item_thuadat.QuanHuyen).FirstOrDefaultAsync();
            TinhThanhPho = await _dbContext.Por_TinhThanhPhos.Where(o => o.Id == item_thuadat.TinhThanhPho).FirstOrDefaultAsync();
            //item.thongTinKhuDat = new ViewModel.HoSoQuyTrinh.ThongTinKhuDat();
            //item.thongTinKhuDat.Id = item_thuadat.Id;
            //item.thongTinKhuDat.SoTo = item_thuadat.SoTo;
            //item.thongTinKhuDat.SoThua = item_thuadat.SoThua;
            //item.thongTinKhuDat.SoNha = item_thuadat.SoNha;
            //item.thongTinKhuDat.TenDuong = item_thuadat.TenDuong;
            //item.thongTinKhuDat.PhuongXa = PhuongXa.Id;
            //item.thongTinKhuDat.TenPhuongXa = PhuongXa.Ten;
            //item.thongTinKhuDat.QuanHuyen = QuanHuyen.Id;
            //item.thongTinKhuDat.TenQuanHuyen = QuanHuyen.Ten;
            //item.thongTinKhuDat.TinhThanhPho = TinhThanhPho.Id;
            //item.thongTinKhuDat.TenTinhThanhPho = TinhThanhPho.Ten;
            //ho so dinh kem
            item.hoSoDinhKem = new ViewModel.HoSoQuyTrinh.HoSoDinhKem();
            item.hoSoDinhKem.fileHoSoNguoiNops = item_hosodinhkem;
            var MucDichSuDung = await _dbContext.Sys_Categories.Where(o => o.Id == item_hoso.IDMucDichSuDung).FirstOrDefaultAsync();
            if(MucDichSuDung != null)
            {
                item.hoSoDinhKem.IDMucDichSuDung = MucDichSuDung.Id;
                item.hoSoDinhKem.TenMucDichSuDung = MucDichSuDung.Name;
            }                
            //thanh toan dich vu
            //item.hinhThucThanhToan = new ViewModel.HoSoQuyTrinh.HinhThucThanhToan();
            //var HinhThucThanhToan = await _dbContext.Sys_Categories.Where(o => o.Id == item_hoso.IDHinhThucThanhToan).FirstOrDefaultAsync();
            //item.hinhThucThanhToan.ThanhToan = item_hoso.ThanhToan;
            //if (HinhThucThanhToan != null)
            //{
            //    item.hinhThucThanhToan.TenHinhThucThanhToan = HinhThucThanhToan.Name;   
            //}                
            ////hinh thuc nhan ket qua
            //item.hinhThucNhanKetQua = new ViewModel.HoSoQuyTrinh.HinhThucNhanKetQua();
            //var HinhThucNhanKetQua = await _dbContext.Sys_Categories.Where(o => o.Id == item_hoso.IDHinhThucNhanKetQua).FirstOrDefaultAsync();
            //if(HinhThucNhanKetQua != null)
            //{
            //    item.hinhThucNhanKetQua.TenHinhThucNhanKetQua = HinhThucNhanKetQua.Name;
            //}                
            return item;
        }
        public async Task<ViewModel.HoSoQuyTrinh.HoSoDashboard> HoSoDashboard()
        {
            var item = new ViewModel.HoSoQuyTrinh.HoSoDashboard();
            item.items = new List<ViewModel.HoSoQuyTrinh.LoaiHoSoDashboard>();
            var dsThuTuc = await _dbContext.Por_QuyTrinhs.ToListAsync();
            var dsHoSoDVC = await _dbContext.Por_HoSo_QuyTrinhs.Select(o => new { o.Id, o.IDHoSo, o.IDQuyTrinh, o.NgayTiepNhan, o.NgayHuy, o.NgayNhanKQ}).ToListAsync();
            var dsHoSoPA = await _dbContext.Por_HoSoPA_QuyTrinhs.Select(o => new { o.Id, o.IDHoSo, o.IDQuyTrinh, o.NgayTiepNhan, o.NgayHuy, o.NgayNhanKQ }).ToListAsync();
            for(int i = 0;i < dsThuTuc.Count;i++)
            {
                var thuTuc = new ViewModel.HoSoQuyTrinh.LoaiHoSoDashboard();
                thuTuc.TenThuTuc = dsThuTuc[i].TenThuTuc;
                //thuTuc.HoSoDVC = dsThuTuc[i].CongKhai;
                //if (dsThuTuc[i].CongKhai)
                //{
                //    thuTuc.HoSoChoTiepNhan = dsHoSoDVC.Where(o => o.NgayTiepNhan.HasValue == false && o.IDQuyTrinh == dsThuTuc[i].Id).Count();
                //    thuTuc.HoSoDangXyLy = dsHoSoDVC.Where(o => o.NgayTiepNhan.HasValue == true && o.IDQuyTrinh == dsThuTuc[i].Id).Count();
                //    thuTuc.HoSoHuy = dsHoSoDVC.Where(o => o.NgayHuy.HasValue == true && o.IDQuyTrinh == dsThuTuc[i].Id).Count();
                //    thuTuc.HoSoTraKetQua = dsHoSoDVC.Where(o => o.NgayNhanKQ.HasValue == true && o.IDQuyTrinh == dsThuTuc[i].Id).Count();
                //}
                //else
                //{
                    thuTuc.HoSoChoTiepNhan = dsHoSoPA.Where(o => o.NgayTiepNhan.HasValue == false && o.IDQuyTrinh == dsThuTuc[i].Id).Count();
                    thuTuc.HoSoDangXyLy = dsHoSoPA.Where(o => o.NgayTiepNhan.HasValue == true && o.IDQuyTrinh == dsThuTuc[i].Id).Count();
                    thuTuc.HoSoHuy = dsHoSoPA.Where(o => o.NgayHuy.HasValue == true && o.IDQuyTrinh == dsThuTuc[i].Id).Count();
                    thuTuc.HoSoTraKetQua = dsHoSoPA.Where(o => o.NgayNhanKQ.HasValue == true && o.IDQuyTrinh == dsThuTuc[i].Id).Count();
                //}
                item.items.Add(thuTuc);
            }    
            return item;
        }
        public async Task<ViewModel.HoSoQuyTrinh.InPhieuBienNhan> InPhieuBienNhan(Guid Id, string UserName)
        {
            //
            var ConfigMauPhieuBienNhan = await _dbContext.Sys_Configs.Where(o => o.Type == Core.Enums.ConfigType.PhieuBienNhan && o.Code == PhieuBienNhanConfig.MauPhieuBienNhan.ToString()).FirstAsync();
            var TemplateMauPhieuBienNhan = await _dbContext.Por_TemplatePhieuBienNhans.Where(o => o.Id == Guid.Parse(ConfigMauPhieuBienNhan.Value)).FirstOrDefaultAsync();
            //
            var item = new ViewModel.HoSoQuyTrinh.InPhieuBienNhan();
            var nguoinhan = await _dbContext.Por_HoSoNguoiNops.Where(o => o.Id == Id).FirstAsync();
            var nguoigiao = await _dbContext.Sys_Users.Where(o => o.UserName == UserName).FirstAsync();
            var LinhVuc = await (from x in _dbContext.Por_HoSo_QuyTrinhs
                           join y in _dbContext.Por_QuyTrinhs on x.IDQuyTrinh equals y.Id
                           join z in _dbContext.Por_LinhVucs on y.IDLinhVuc equals z.Id
                           select new { x.IDHoSo, z.So, y.TenThuTuc }).FirstOrDefaultAsync(o => o.IDHoSo == Id);
            item.SoHoSo = LinhVuc.So.ToUpper() + StringHelpers.RandomString(5); ;
            item.HovatenNguoiNhan = nguoinhan.HoTen;
            item.HovatenNguoiGiao = nguoigiao.FullName;
            item.Email = nguoinhan.Email;
            item.Dienthoai = nguoinhan.SoDienThoai;
            item.LoaiHoSo = LinhVuc.TenThuTuc;
            var PhuongXa = await _dbContext.Por_PhuongXaThiTrans.Where(o => o.Id == nguoinhan.PhuongXa).FirstOrDefaultAsync();
            var QuanHuyen = await _dbContext.Por_QuanHuyens.Where(o => o.Id == nguoinhan.QuanHuyen).FirstOrDefaultAsync();
            var TinhThanhPho = await _dbContext.Por_TinhThanhPhos.Where(o => o.Id == nguoinhan.TinhThanhPho).FirstOrDefaultAsync();
            item.DiaChi = nguoinhan.SoNha + ", " + nguoinhan.TenDuong + ", " + PhuongXa.Ten + ", " + QuanHuyen.Ten + ", " + TinhThanhPho.Ten;
            if (TemplateMauPhieuBienNhan != null)
            {
                var NoiDung = TemplateMauPhieuBienNhan.NoiDung;
                var CotSql = TemplateMauPhieuBienNhan.CotSql;

                string[] arr_column_sql = CotSql.Split(",");
                List<string> arr_value = new List<string>();
                for (var i = 0; i < arr_column_sql.Length; i++)
                {
                    if (arr_column_sql[i] == "SoHoSo")
                    {
                        arr_value.Add(item.SoHoSo);
                    }
                    else if (arr_column_sql[i] == "HovatenNguoiNhan")
                    {
                        arr_value.Add(item.HovatenNguoiNhan);
                    }
                    else if (arr_column_sql[i] == "HovatenNguoiGiao")
                    {
                        arr_value.Add(item.HovatenNguoiGiao);
                    }
                    else if (arr_column_sql[i] == "ChuKyHovatenNguoiGiao")
                    {
                        arr_value.Add(item.HovatenNguoiGiao);
                    }      
                    else if (arr_column_sql[i] == "Email")
                    {
                        arr_value.Add(item.Email);
                    }
                    else if (arr_column_sql[i] == "Dienthoai")
                    {
                        arr_value.Add(item.Dienthoai);
                    }
                    else if (arr_column_sql[i] == "DiaChi")
                    {
                        arr_value.Add(item.DiaChi);
                    }
                    else if (arr_column_sql[i] == "LoaiHoSo")
                    {
                        arr_value.Add(item.LoaiHoSo);
                    }
                    else if (arr_column_sql[i] == "NgayHienTai")
                    {
                        arr_value.Add(DateTime.Now.ToString("dd/MM/yyyy"));
                    }
                }
                item.NoiDungHTML = string.Format(TemplateMauPhieuBienNhan.NoiDung, arr_value.ToArray());
            }
            return item;
        }
        public async Task<ViewModel.HoSoQuyTrinh.InPhieuTiepNhan> InPhieuTiepNhan(Guid Id)
        {
            var item = new ViewModel.HoSoQuyTrinh.InPhieuTiepNhan();
            var hoso_quytrinh = await (from x in _dbContext.Por_HoSo_QuyTrinhs
                                       join y in _dbContext.Por_QuyTrinhs on x.IDQuyTrinh equals y.Id
                                       where x.IDHoSo == Id
                                       select new { x.Id, x.IDQuyTrinh, x.NguoiTiepNhan, x.NgayTiepNhan, x.NgayNop, x.NgayDuKienNhanKQ, TenThuTuc = y.TenThuTuc })
                                       .FirstAsync();
            var nguoinop = await _dbContext.Por_HoSoNguoiNops.Where(o => o.Id == Id).FirstAsync();
            var nguoitiepnhan = await _dbContext.Sys_Users.Where(o => o.UserName == hoso_quytrinh.NguoiTiepNhan).FirstAsync();
            item.SoHoSo = nguoinop.SoHoSo;
            item.HovatenNguoiNhan = nguoitiepnhan.FullName;
            item.HovatenNguoiNop = nguoinop.HoTen;
            item.Email = nguoinop.Email;
            item.Dienthoai = nguoinop.SoDienThoai;

            var PhuongXa = await _dbContext.Por_PhuongXaThiTrans.Where(o => o.Id == nguoinop.PhuongXa).FirstOrDefaultAsync();
            var QuanHuyen = await _dbContext.Por_QuanHuyens.Where(o => o.Id == nguoinop.QuanHuyen).FirstOrDefaultAsync();
            var TinhThanhPho = await _dbContext.Por_TinhThanhPhos.Where(o => o.Id == nguoinop.TinhThanhPho).FirstOrDefaultAsync();
            item.DiaChi = nguoinop.SoNha + ", " + nguoinop.TenDuong + ", " + PhuongXa.Ten + ", " + QuanHuyen.Ten + ", " + TinhThanhPho.Ten;
            item.VeViec = hoso_quytrinh.TenThuTuc;
            item.NgayNhan = hoso_quytrinh.NgayTiepNhan.HasValue ? hoso_quytrinh.NgayTiepNhan.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
            item.NgayTraKetQua = hoso_quytrinh.NgayDuKienNhanKQ.ToString("dd/MM/yyyy HH:mm:ss");
            item.Thanhphanhoso = await _dbContext.Por_FileMauThanhPhanHStrongQTs.Where(o => o.IDQuyTrinh == hoso_quytrinh.IDQuyTrinh).ToListAsync();
            return item;
        }
        public async Task<List<ViewModel.HoSoQuyTrinh.HoSo>> GetDSHoSo(string search, string userName)
        {    
            if(string.IsNullOrEmpty(userName))
            {
                return null;
            }
            var query = from x in _dbContext.Por_HoSoNguoiNops   
                        join y in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals y.IDHoSo
                        join z in _dbContext.Por_QuyTrinhs on y.IDQuyTrinh equals z.Id                        
                        join a in _dbContext.Por_BuocQuyTrinhs on y.IDBuocQuyTrinh equals a.Id
                            into sub_Por_BuocQuyTrinhs from aa in sub_Por_BuocQuyTrinhs.DefaultIfEmpty()                       
                        select new { x.Id, x.ThanhToan, x.TaiKhoanNguoiGui, x.SoHoSo, x.CreatedDateTime, y.NgayTiepNhan, z.TenThuTuc, y.NgayNhanKQ,                                     
                                    IdTrangThai = x.IDTrangThaiHS, y.NgayDuKienNhanKQ,
                                    IdChucNang = aa != null ? aa.IDChucNangBuocQuyTrinh : Guid.Empty
                        };
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(o => o.SoHoSo.Contains(search));
            }
            else
            {
                query = query.Where(o => o.TaiKhoanNguoiGui == userName);
            }
            //else
            //{
            //    if (!string.IsNullOrEmpty(userName))
            //    {
            //        query = query.Where(o => o.TaiKhoanNguoiGui == userName);
            //    }    
            //    else
            //    {
            //        query = query.Where(o => o.TaiKhoanNguoiGui == "-1");
            //    }    
            //}
            var ChucNangs = await _dbContext.Por_ChucNang_BuocQuyTrinhs.ToListAsync();
            var TrangThais = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo).ToListAsync();
            var items = await query.ToListAsync();
            var items_mapping = new List<ViewModel.HoSoQuyTrinh.HoSo>();
            foreach(var o in items)
            {
                var item = new ViewModel.HoSoQuyTrinh.HoSo()
                {
                    Id = o.Id,
                    SoHoSo = o.SoHoSo,
                    TaiKhoanNguoiGui = o.TaiKhoanNguoiGui,
                    NgayTiepNhan = o.NgayTiepNhan.HasValue ? o.NgayTiepNhan.Value.ToString("dd/MM/yyyy"): "",
                    CreatedDateTime = o.CreatedDateTime.ToString("dd/MM/yyyy"),
                    TenThuTuc = o.TenThuTuc,
                    ThoiDiemDuKienXuLyHoanTat = o.NgayDuKienNhanKQ.ToString("dd/MM/yyyy"),
                    ThoiDiemXuLyHoanTat = o.NgayNhanKQ != null ? o.NgayNhanKQ.Value.ToString("dd/MM/yyyy") : "",
                    TinhTrang = ""
                };
                var trangthai = TrangThais.FirstOrDefault(e => e.Id == o.IdTrangThai);
                if(trangthai == null)
                    continue;
                item.ChoTiepTuc = false;
                item.ChoPhepSua = false;
                if (item.TaiKhoanNguoiGui == userName)
                {
                    if (trangthai.Code == TrangThaiQuyTrinh.tamdung.ToString())
                    {
                        item.ChoTiepTuc = true;
                        item.ChoPhepSua = true;
                    }
                    if (string.IsNullOrEmpty(item.NgayTiepNhan))
                    {
                        item.ChoPhepSua = true;
                    }
                }
                if (trangthai.Code == TrangThaiQuyTrinh.traketqua.ToString())
                {
                    item.TinhTrang = trangthai.Name;
                }
                else if (trangthai.Code == TrangThaiQuyTrinh.huy.ToString())
                {
                    item.TinhTrang = trangthai.Name;
                }
                else if (trangthai.Code == TrangThaiQuyTrinh.tamdung.ToString())
                {
                    item.TinhTrang = trangthai.Name;
                }
                else if (trangthai.Code == TrangThaiQuyTrinh.dangvanchuyen.ToString())
                {
                    item.TinhTrang = trangthai.Name;
                }
                else
                if (trangthai.Code == TrangThaiQuyTrinh.tamdung.ToString())
                {
                    item.TinhTrang = trangthai.Name;
                }
                else
                {
                    var chucnung = ChucNangs.FirstOrDefault(e => e.Id == o.IdChucNang);
                    if (chucnung != null)
                    {
                        item.TinhTrang = trangthai.Name + " " + chucnung.Ten;
                    }
                    if (string.IsNullOrEmpty(item.NgayTiepNhan) && o.ThanhToan != null)
                    {
                        if(!o.ThanhToan.Value)
                        {
                            item.TinhTrang = "Chờ thanh toán";
                        }    
                    }
                }
                items_mapping.Add(item);
            }                
            return items_mapping;
        }    
        public async Task<List<LoaiHoSo>> LayDsLoaiHoSo()
        {
            var items = await (from x in _dbContext.Por_QuyTrinhs
                               select new LoaiHoSo()
                               {
                                   Id = x.Id,
                                   Ma = x.MaThuTuc,
                                   Ten = x.TenThuTuc,
                                   ThoiGianThucHien = x.ThoiGianThucHien,
                               }).ToListAsync();
            //var items_filter = new List<LoaiHoSo>();
            //for(var i = 0;i < items.Count;i++)
            //{
            //    var existed = _dbContext.Por_HoSo_QuyTrinhs.Any(o => o.IDQuyTrinh == items[i].Id);
            //    if(existed)
            //    {
            //        items_filter.Add(items[i]);
            //    }
            //}
            return items;
        }
        public async Task<List<Model.Sys_Category>> LayDsHinhThucNhanKetQua()
        {
            var items = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.HinhThucNhanKetQua).ToListAsync();
            return items;
        }
        public async Task<List<Model.Sys_Category>> LayDsHinhThucThanhToan()
        {
            var items = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.HinhThucThanhToan).ToListAsync();
            return items;
        }
        public async Task<List<Model.Sys_Category>> LayDsTieuChiTamDung()
        {
            var items = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TieuChiTamDung).ToListAsync();
            return items;
        }
        
        public async Task<List<Model.Sys_Category>> LayDsMucDichSuDung()
        {
            var items = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.MucDichSuDung).ToListAsync();
            return items;
        }
        public async Task<List<Model.Sys_Category>> LayDsLoaiPhanAnh()
        {
            var items = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.LoaiPhanAnh).ToListAsync();
            return items;
        }        
        public async Task<List<Model.Por_FileMauThanhPhanHStrongQT>> LayDsFileMauThanhPhanHS(Guid IdQuyTrinh)
        {
            var items = await _dbContext.Por_FileMauThanhPhanHStrongQTs.Where(o => o.IDQuyTrinh == IdQuyTrinh).ToListAsync();
            return items;
        }
        public async Task<ViewModel.HoSoQuyTrinh.SuaHoSoQuyTrinh> SuaHoSo(ViewModel.HoSoQuyTrinh.SuaHoSoQuyTrinh item)
        {
            var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone);
            var modelSave_HoSoNguoiNop = await _dbContext.Por_HoSoNguoiNops.FirstOrDefaultAsync(o => o.Id == item.thongTinNguoiNop.Id);
            ObjectHelpers.Mapping<ViewModel.HoSoQuyTrinh.ThongTinNguoiNop, Model.Por_HoSoNguoiNop>(item.thongTinNguoiNop, modelSave_HoSoNguoiNop);

            var modelSave_ThuatDat = await _dbContext.Por_ThuatDats.FirstOrDefaultAsync(o => o.Id == item.thongTinKhuDat.Id);
            ObjectHelpers.Mapping<ViewModel.HoSoQuyTrinh.ThongTinKhuDat, Model.Por_ThuatDat>(item.thongTinKhuDat, modelSave_ThuatDat);

            modelSave_HoSoNguoiNop.IDMucDichSuDung = item.hoSoDinhKem.IDMucDichSuDung;
            await _dbContext.SaveChangesAsync();
            return item;
        }
        public async Task<ViewModel.HoSoQuyTrinh.HoSoQuyTrinh> NopHoSo(ViewModel.HoSoQuyTrinh.HoSoQuyTrinh item)
        {
            var DateTimeOffsetNow = DateTimeOffset.Now.AddHours(Sys_Const.TimeZone); 
            //Thong tin nguoi nop
            var Por_HoSoNguoiNop = new Model.Por_HoSoNguoiNop();
            var BuocQuyTrinhFirst = await (from x in _dbContext.Por_QuyTrinhs
                                           join y in _dbContext.Por_BuocQuyTrinhs on x.Id equals y.IDQuyTrinh
                                           join z in _dbContext.Por_LinhVucs on x.IDLinhVuc equals z.Id
                                           where x.Id == item.loaiHoSo.IDQuyTrinh
                                           select new { x.MienPhi, MaLinhVuc = z.Ma, IDQuyTrinh = x.Id, ThoiGianThucHien = x.ThoiGianThucHien, IDBuocQuyTrinh = y.Id, ThuTuBuoc = y.ThuTuBuoc })
                      .OrderBy(o => o.ThuTuBuoc)
                      .FirstAsync();
            var TrangThaiChoXuLy = await _dbContext.Sys_Categories.Where(o => o.Type == Core.Enums.CategoryType.TrangThaiHoSo && o.Code == TrangThaiQuyTrinh.choxuly.ToString()).FirstAsync();
            Guid IDBuocQuyTrinh = Guid.NewGuid();
            var modelSave_HoSoNguoiNop = new Model.Por_HoSoNguoiNop();
            ObjectHelpers.Mapping<ViewModel.HoSoQuyTrinh.ThongTinNguoiNop, Model.Por_HoSoNguoiNop>(item.thongTinNguoiNop, modelSave_HoSoNguoiNop);
            modelSave_HoSoNguoiNop.Id = Guid.NewGuid();
            modelSave_HoSoNguoiNop.SoHoSo = BuocQuyTrinhFirst.MaLinhVuc.ToUpper() + StringHelpers.RandomString(5);
            modelSave_HoSoNguoiNop.TaiKhoanNguoiGui = item.taiKhoanNguoiNop;
            modelSave_HoSoNguoiNop.IDTrangThaiHS = TrangThaiChoXuLy.Id;
           // modelSave_HoSoNguoiNop.IDMucDichSuDung = item.hoSoDinhKem.IDMucDichSuDung;
            //modelSave_HoSoNguoiNop.IDHinhThucThanhToan = item.hinhThucThanhToan.IDHinhThucThanhToan;
            //if(BuocQuyTrinhFirst.MienPhi)
            //{
            //    modelSave_HoSoNguoiNop.IDHinhThucThanhToan = Guid.Empty;
            //}
            //modelSave_HoSoNguoiNop.IDHinhThucNhanKetQua = item.hinhThucNhanKetQua.IDHinhThucNhanKetQua;
            modelSave_HoSoNguoiNop.CreatedDateTime = DateTimeOffsetNow;
            //
            //modelSave_HoSoNguoiNop.ThanhToan = false;
            //if (BuocQuyTrinhFirst.MienPhi)
            //{
            //    modelSave_HoSoNguoiNop.ThanhToan = null;
            //}
            //
            item.thongTinNguoiNop.Id = modelSave_HoSoNguoiNop.Id;
            item.thongTinNguoiNop.SoHoSo = modelSave_HoSoNguoiNop.SoHoSo;
            await _dbContext.Por_HoSoNguoiNops.AddAsync(modelSave_HoSoNguoiNop);
            Model.Por_HoSo_QuyTrinh por_HoSo_QuyTrinh = new Model.Por_HoSo_QuyTrinh();
            por_HoSo_QuyTrinh.Id = Guid.NewGuid();            
            por_HoSo_QuyTrinh.IDHoSo = modelSave_HoSoNguoiNop.Id;
            por_HoSo_QuyTrinh.IDQuyTrinh = BuocQuyTrinhFirst.IDQuyTrinh;
            por_HoSo_QuyTrinh.IDBuocQuyTrinh = BuocQuyTrinhFirst.IDBuocQuyTrinh;
            por_HoSo_QuyTrinh.ThoiGianThucHien = BuocQuyTrinhFirst.ThoiGianThucHien;
            por_HoSo_QuyTrinh.NgayNop = DateTimeOffsetNow;
            por_HoSo_QuyTrinh.NgayTiepNhan = null;
            por_HoSo_QuyTrinh.NgayNhanKQ = null;
            por_HoSo_QuyTrinh.NgayDuKienNhanKQ = DateTimeOffsetNow.AddDays(BuocQuyTrinhFirst.ThoiGianThucHien);
            await _dbContext.Por_HoSo_QuyTrinhs.AddAsync(por_HoSo_QuyTrinh);
            //Thong tin khu dat
            //var modelSave_ThuatDat = new Model.Por_ThuatDat();            
            //ObjectHelpers.Mapping<ViewModel.HoSoQuyTrinh.ThongTinKhuDat, Model.Por_ThuatDat>(item.thongTinKhuDat, modelSave_ThuatDat);
            //modelSave_ThuatDat.Id = Guid.NewGuid();
            //modelSave_ThuatDat.IDHoSoNguoiNop = modelSave_HoSoNguoiNop.Id;
            //item.thongTinKhuDat.Id = modelSave_ThuatDat.Id;
           // await _dbContext.Por_ThuatDats.AddAsync(modelSave_ThuatDat);
            //
            await _dbContext.SaveChangesAsync();
            await SendMessageHoSoDiaChinh(modelSave_HoSoNguoiNop.Id, SmsConfig.Mau_Sms_HSDVC_NopHoSo.ToString(), EmailConfig.Mau_Email_HSDVC_NopHoSo.ToString());
            //
            return item;
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
        public async Task<ViewModel.HoSoQuyTrinh.HoSoQuyTrinh> SuaHoSo(ViewModel.HoSoQuyTrinh.HoSoQuyTrinh item)
        {
            var quytrinh = await (from x in _dbContext.Por_QuyTrinhs
                            join y in _dbContext.Por_HoSo_QuyTrinhs on x.Id equals y.IDQuyTrinh
                            where y.IDHoSo == item.thongTinNguoiNop.Id
                            select new { x.MienPhi }).FirstOrDefaultAsync();
            var modelSave_HoSoNguoiNop = await _dbContext.Por_HoSoNguoiNops.FirstAsync(o => o.Id == item.thongTinNguoiNop.Id);
            string SoHoSo = modelSave_HoSoNguoiNop.SoHoSo;
            ObjectHelpers.Mapping<ViewModel.HoSoQuyTrinh.ThongTinNguoiNop, Model.Por_HoSoNguoiNop>(item.thongTinNguoiNop, modelSave_HoSoNguoiNop);
            modelSave_HoSoNguoiNop.SoHoSo = SoHoSo;
            //modelSave_HoSoNguoiNop.IDHinhThucThanhToan = item.hinhThucThanhToan.IDHinhThucThanhToan;
            //modelSave_HoSoNguoiNop.IDHinhThucNhanKetQua = item.hinhThucNhanKetQua.IDHinhThucNhanKetQua;            
            //
           // ObjectHelpers.Mapping<ViewModel.HoSoQuyTrinh.ThongTinKhuDat, Model.Por_ThuatDat>(item.thongTinKhuDat, modelSave_ThuatDat);
            //            
            await _dbContext.SaveChangesAsync();
            //
            return item;
        }
        public async Task XoaFileNop(Guid hosoId, string idsFileNop)
        {
            var items = await _dbContext.Por_FileHoSoNguoiNops.Where(o => o.IDHoSoNguoiNop == hosoId).ToListAsync();
            if(string.IsNullOrEmpty(idsFileNop))
            {
                _dbContext.Por_FileHoSoNguoiNops.RemoveRange(items);
                await _dbContext.SaveChangesAsync();
            }
            var itemsXoa = new List<Model.Por_FileHoSoNguoiNop>();
            if(!string.IsNullOrEmpty(idsFileNop))
            {
                var idsFileNopArr = idsFileNop.Split("_");
                foreach (var item in items)
                {
                    bool xoa = true;
                    foreach(var id in idsFileNopArr)
                    {
                        if(!string.IsNullOrEmpty(id))
                        {
                            if (item.Id == Guid.Parse(id))
                            {
                                xoa = false;
                                break;
                            }
                        }
                    }  
                    if(xoa)
                    {
                        itemsXoa.Add(item);
                    }
                }
            }
            if(itemsXoa.Count > 0)
            {
                _dbContext.Por_FileHoSoNguoiNops.RemoveRange(itemsXoa);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task XoaFileDinhKem(Guid hosoId, string idsFileNop)
        {
            var items = await _dbContext.Por_FileHoSos.Where(o => o.IDHoSoNguoiNop == hosoId).ToListAsync();
            if (string.IsNullOrEmpty(idsFileNop))
            {
                _dbContext.Por_FileHoSos.RemoveRange(items);
                await _dbContext.SaveChangesAsync();
            }
            var itemsXoa = new List<Model.Por_FileHoSo>();
            if (!string.IsNullOrEmpty(idsFileNop))
            {
                var idsFileNopArr = idsFileNop.Split("_");
                foreach (var item in items)
                {
                    bool xoa = true;
                    foreach (var id in idsFileNopArr)
                    {
                        if (!string.IsNullOrEmpty(id))
                        {
                            if (item.Id == Guid.Parse(id))
                            {
                                xoa = false;
                                break;
                            }
                        }
                    }
                    if (xoa)
                    {
                        itemsXoa.Add(item);
                    }
                }
            }
            if (itemsXoa.Count > 0)
            {
                _dbContext.Por_FileHoSos.RemoveRange(itemsXoa);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
