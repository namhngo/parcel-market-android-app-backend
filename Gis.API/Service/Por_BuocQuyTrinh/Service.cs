using Microsoft.EntityFrameworkCore;
using Gis.API.Infrastructure;
using Gis.Core.Constant;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gis.API.ViewModel.Por_BuocQuyTrinh;

namespace Gis.API.Service.Por_BuocQuyTrinh
{
    public class Service:RepositoryBase<Model.Por_BuocQuyTrinh>, Por_BuocQuyTrinh.IService
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
        public async Task<List<DsBuocQuyTrinhTheoQT>> DsBuocQuyTrinhTheoQuyTrinh(Guid Id)
        {
            var items = await (from x in _dbContext.Por_BuocQuyTrinhs
                         join y in _dbContext.Por_ChucNang_BuocQuyTrinhs on x.IDChucNangBuocQuyTrinh equals y.Id
                         where x.IDQuyTrinh == Id
                         select new DsBuocQuyTrinhTheoQT()
                         {
                             Id = x.Id,
                             Ten = x.Ten,
                             ThoiGianThucHien = x.ThoiGianThucHien,
                             ThuocThoiGianThucHien = x.ThuocThoiGianThucHien,
                             IDQuyTrinh = x.IDQuyTrinh,
                             IDChucNangBuocQuyTrinh = x.IDChucNangBuocQuyTrinh,
                             TenChucNangBuocQuyTrinh = y.Ten,
                             ThuTuBuoc = x.ThuTuBuoc,
                             IDsNguoiDungThamGia = x.IDsNguoiDungThamGia,
                             GuiEmail = x.GuiEmail,
                             IDMauEmail = x.IDMauEmail,
                             GuiSms = x.GuiSms,
                             IDMauSms = x.IDMauSms,
                         }).OrderBy(o => o.ThuTuBuoc).ToListAsync();
            return items;
        }
    }
}
