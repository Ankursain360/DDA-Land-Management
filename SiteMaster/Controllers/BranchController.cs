using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using SiteMaster.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
namespace SiteMaster.Controllers
{
    public class BranchController : BaseController
    {

        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] BranchSearchDto model)
        {
            var result = await _branchService.GetPagedBranch(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Branch branch)
        {
            branch.DepartmentList = await _branchService.GetDropDownList();
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Branch branch = new Branch();
            branch.IsActive = 1;
            await BindDropDown(branch);
            return View(branch);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Branch branch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await BindDropDown(branch);
                    var result = await _branchService.Create(branch);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var result1 = await _branchService.GetAllDetails();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(branch);

                    }
                }
                else
                {
                    return View(branch);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(branch);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _branchService.FetchSingleResult(id);
            await BindDropDown(Data);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Branch branch)
        {
            await BindDropDown(branch);
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _branchService.Update(id, branch);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _branchService.GetAllDetails();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(branch);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(branch);
                }
            }
            return View(branch);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _branchService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Branch Name : {Name} already exist");
            }
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsCodeExist(int Id, string Code)
        {
            var result = await _branchService.CheckUniqueCode(Id, Code);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Branch Code: {Code} already exist");
            }
        }


        public async Task<IActionResult> DeleteConfirmed(int id)  
        {
            var result = await _branchService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            return RedirectToAction("Index", "Branch");

        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _branchService.FetchSingleResult(id);
            await BindDropDown(Data);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }



       // [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> BranchList()
        {
            var result = await _branchService.GetAllDetails();
            List<BranchListDto> data = new List<BranchListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new BranchListDto()
                    {
                        Id = result[i].Id,
                        BranchCode= result[i].Code,
                        BranchName = result[i].Name,
                        Department=result[i].Department==null ? "" :result[i].Department.Name,
                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

      
    }
}
