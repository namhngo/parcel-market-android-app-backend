using Microsoft.EntityFrameworkCore;
using Gis.API.Infrastructure;
using Gis.Core.Constant;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_FileMauThanhPhanHStrongQT
{
    public class Service : RepositoryBase<Model.Por_FileMauThanhPhanHStrongQT>, Por_FileMauThanhPhanHStrongQT.IService
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
        public async Task<List<Model.Por_FileMauThanhPhanHStrongQT>> DsFileMauThanhPhanHSTheoQuyTrinh(Guid Id)
        {
            return await _dbContext.Por_FileMauThanhPhanHStrongQTs.Where(o => o.IDQuyTrinh == Id).ToListAsync();
        }
        public async Task XoaFile(List<Model.Por_FileMauThanhPhanHStrongQT> itemsEdit, List<Model.Por_FileMauThanhPhanHStrongQT> itemsSaveNew)
        {
            if(itemsEdit.Count > 0 || itemsSaveNew.Count > 0)
            {
                Guid IDQuyTrinh = Guid.Empty;
                if(itemsEdit.Count > 0)
                {
                    IDQuyTrinh = itemsEdit[0].IDQuyTrinh;
                }
                else if (itemsSaveNew.Count > 0)
                {
                    IDQuyTrinh = itemsSaveNew[0].IDQuyTrinh;
                }
                if(IDQuyTrinh != Guid.Empty)
                {
                    var items = await _dbContext.Por_FileMauThanhPhanHStrongQTs.Where(o => o.IDQuyTrinh == IDQuyTrinh).ToListAsync();
                    var itemsRemove = new List<Model.Por_FileMauThanhPhanHStrongQT>();
                    for (var i = 0; i < items.Count; i++)
                    {
                        var item = items[i];
                        bool xoa = true;
                        for (var x = 0; x < itemsEdit.Count; x++)
                        {
                            if (items[i].Id == itemsEdit[x].Id)
                            {
                                xoa = false;
                                break;
                            }
                        }
                        //
                        if(xoa)
                        {
                            for (var x = 0; x < itemsSaveNew.Count; x++)
                            {
                                if (items[i].Id == itemsSaveNew[x].Id)
                                {
                                    xoa = false;
                                    break;
                                }
                            }
                        }
                        if (xoa)
                        {
                            itemsRemove.Add(item);
                        }
                    }
                    if(itemsRemove.Count > 0)
                    {
                        _dbContext.Por_FileMauThanhPhanHStrongQTs.RemoveRange(itemsRemove);
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
