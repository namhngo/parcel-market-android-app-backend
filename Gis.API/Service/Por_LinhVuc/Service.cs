using Microsoft.EntityFrameworkCore;
using Gis.API.Infrastructure;
using Gis.Core.Constant;
using Gis.Core.Core;
using Gis.Core.Helpers;
using Gis.Core.Interfaces;
using Gis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.API.Service.Por_LinhVuc
{
    public class Service : RepositoryBase<Model.Por_LinhVuc>, Por_LinhVuc.IService
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
        public async Task<List<ViewModel.Por_LinhVuc.LinhVucTree>> GetTreeAsync()
        {
            List<ViewModel.Por_LinhVuc.LinhVucTree> items = await _dbContext.Por_LinhVucs.Select(o =>
                new ViewModel.Por_LinhVuc.LinhVucTree() { Id = o.Id.ToString(), Code = o.Ma, Name = o.Ten, ParentId = o.IDCha.ToString() }).ToListAsync();
            List<ViewModel.Por_LinhVuc.LinhVucTree> trees = TreeHelpers<ViewModel.Por_LinhVuc.LinhVucTree>.ListToTrees(items);
            return trees;
        }
        public async Task<List<ViewModel.Por_LinhVuc.LinhVucTree>> GetTreeListAsync()
        {
            List<ViewModel.Por_LinhVuc.LinhVucTree> items = await _dbContext.Por_LinhVucs.Select(o =>
                new ViewModel.Por_LinhVuc.LinhVucTree() { Id = o.Id.ToString(), Code = o.Ma, Name = o.Ten, ParentId = o.IDCha.ToString() }).ToListAsync();
            List<ViewModel.Por_LinhVuc.LinhVucTree> trees = TreeHelpers<ViewModel.Por_LinhVuc.LinhVucTree>.ListToTrees(items, true);
            trees = trees.Traverse(o => o.Children).ToList();
            foreach(var tree in trees)
            {
                tree.Children = null;
            }
            return trees;
        }
        public async Task<List<Model.Por_LinhVuc>> GetByParentIdAsync(Guid ParentId)
        {
            List<Model.Por_LinhVuc> items = await _dbContext.Por_LinhVucs.Where(o => o.IDCha == ParentId).ToListAsync();
            return items;
        }
        public async Task<bool> IsDupicateAttributesAsync(Guid? Id, string Ten, Guid ParentId)
        {
            bool result = false;
            if (GuidHelpers.IsNullOrEmpty(Id))
            {
                result = await _dbContext.Por_LinhVucs.Where(o => o.Ten == Ten && o.IDCha == ParentId).AnyAsync();
            }
            else
            {
                var count = await _dbContext.Por_LinhVucs.Where(o => o.Id == Id && o.Ten == Ten && o.IDCha == ParentId).CountAsync();
                if (count <= 1)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            return await Task.FromResult(result);
        }
        public async Task DeleteById(Guid Id)
        {
            var organ = await _dbContext.Por_LinhVucs.FirstOrDefaultAsync(o => o.Id == Id);
            if (organ == null)
            {
                throw new Exception("Lĩnh vực không tồn tại !");
            }
            var organChilds = await _dbContext.Por_LinhVucs.Where(o => o.IDCha == Id).ToListAsync();
            if (organChilds != null && organChilds.Count > 0)
            {
                throw new Exception("Lĩnh vực hiện tại, chứa lĩnh vực con!");
            }
            _dbContext.Por_LinhVucs.Remove(organ);
            await UnitOfWork.SaveAsync();
        }
    }
}
