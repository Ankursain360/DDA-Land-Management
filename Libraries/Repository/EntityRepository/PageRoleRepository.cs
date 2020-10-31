using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class PageRoleRepository : GenericRepository<PageRole>, IPageRoleRepository
    {
        public PageRoleRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> Add(AssignPageRoleWise assignPageRoleWise)
        {
            _dbContext.Add(assignPageRoleWise);
            var result=await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }
        public async Task<bool> AddPageRole(PageRole pageRole)
        {
            _dbContext.PageRole.Add(pageRole);
            var result=await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<bool> DeleteAssignPageRoleWise(AssignPageRoleWise assignPageRoleWise)
        {
            var data =await _dbContext.AssignPageRoleWises.Where(x => x.RoleId == assignPageRoleWise.RoleId && x.ModuleId == assignPageRoleWise.ModuleId).ToListAsync();
            foreach (var roleWise in data)
            {
                _dbContext.Remove(roleWise);
            }
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<bool> DeletePageRole(PageRole pageRole)
        {
            var data =await _dbContext.PageRole.Where(x => x.RoleId == pageRole.RoleId && x.ModuleId == pageRole.ModuleId && x.UserId==pageRole.UserId).ToListAsync();
            foreach (var userWise in data)
            {
                _dbContext.Remove(userWise);
            }
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<List<Module>> GetAllModule()
        {
            return await _dbContext.Module.ToListAsync();
        }

        public async Task<List<PageRole>> GetAllPageRole()
        {
            return await _dbContext.PageRole.Include(x=>x.Role).Include(x=>x.User).ToListAsync();
        }

        public async Task<List<Role>> GetAllRole()
        {
            return await _dbContext.Role.ToListAsync();
        }

        public async Task<List<User>> GetAllUser(int role)
        {
            return await _dbContext.User.Where(x=>x.RoleId==role).ToListAsync();
        }

        public async Task<List<PageRole>> GetPageRoleDetailsRoleWise(int moduleId, int roleId, int? userId)
        {
            List<PageRole> olist = new List<PageRole>();
            //var Data = await (from A in _dbContext.Module
            //                  //join B in _dbContext.Page on A.Id equals B.ModuleId
                              
            //                //  join C in _dbContext.PageRole on B.Id equals C.PageId into Details
            //                 // from D in Details.DefaultIfEmpty()
            //                  where A.Id == moduleId && (D.RoleId == roleId || D.RoleId == D.RoleId) && (D.UserId == userId || D.UserId == D.UserId)
            //                  select new
            //                  {
            //                      RoleId=D.RoleId,
            //                      ModuleId = A.Id,
            //                      ModuleName = A.Name,
            //                      //PageName = B.Name,

            //                      //PageId = B.Id,
            //                      RAdd = D.RAdd,
            //                      RDelete = D.RDelete,
            //                      REdit = D.REdit,
            //                      RView = D.RView,
            //                      RDisplay = D.RDisplay,
            //                      UserId = D.UserId
            //                  }).ToListAsync();

            //if (Data != null)
            //{
            //    //for (int i = 0; i < Data.Count; i++)
            //    //{
            //    //    olist.Add(new PageRole()
            //    //    {
            //    //        RoleId=Data[i].RoleId,
            //    //        ModuleId = Data[i].ModuleId,
            //    //        ModuleName = Data[i].ModuleName,
            //    //        PageId = Data[i].PageId,
            //    //        PageName = Data[i].PageName,
            //    //        RDisplay = Data[i].RDisplay,
            //    //        RAdd = Data[i].RAdd,
            //    //        RDelete = Data[i].RDelete,
            //    //        REdit = Data[i].REdit,
            //    //        RView = Data[i].RView,
            //    //        UserId = Data[i].UserId
            //    //    });
            //    //}
            //}
            return olist;
        }

        public async Task<List<PageRole>> GetPageRoleDetailsRoleWise(int moduleId, int roleId)
        {
            List<PageRole> olist = new List<PageRole>();
            //var Data = await (from A in _dbContext.Module
            //                  join B in _dbContext.Page on A.Id equals B.ModuleId
            //                  join C in _dbContext.AssignPageRoleWises on B.Id equals C.PageId into Details
            //                  from D in Details.DefaultIfEmpty()
            //                  where A.Id == moduleId && (D.RoleId == roleId || D.RoleId == null)
            //                  select new
            //                  {
            //                      RoleId = D.RoleId,
            //                      ModuleId = A.Id,
            //                      ModuleName = A.Name,
            //                      PageName = B.Name,
            //                      PageId = B.Id,
            //                      RAdd = D.RAdd,
            //                      RDelete = D.RDelete,
            //                      REdit = D.REdit,
            //                      RView = D.RView,
            //                      RDisplay = D.RDisplay,
            //                      UserId = 0
            //                  }).ToListAsync();

            //if (Data != null)
            //{
            //    for (int i = 0; i < Data.Count; i++)
            //    {
            //        olist.Add(new PageRole()
            //        {
            //            RoleId = Data[i].RoleId,
            //            ModuleId = Data[i].ModuleId,
            //            ModuleName = Data[i].ModuleName,
            //            PageId = Data[i].PageId,
            //            PageName = Data[i].PageName,
            //            RDisplay =(byte)(Data[i].RDisplay??0),
            //            RAdd = (byte)(Data[i].RAdd??0),
            //            REdit = (byte)(Data[i].REdit??0),
            //            RDelete = (byte)(Data[i].RDelete??0),
            //            RView = (byte)(Data[i].RView??0),
            //            UserId = Data[i].UserId
            //        });
            //    }
            //}
            return olist;
        }
    }
}
