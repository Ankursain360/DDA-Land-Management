using Core.Enum;
using CourtCasesManagement.Filters;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Helper;

namespace CourtCasesManagement.Controllers
{
    public class LawyerMasterController : BaseController
    {
        private readonly ILawyerService _lawyerService;


        public LawyerMasterController(ILawyerService lawyerService)
        {
            _lawyerService = lawyerService;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var result = await _lawyerService.GetAllLawyer();
            return View(result);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LawyerSearchDto model)
        {
            var result = await _lawyerService.GetPagedLawyer(model);
            return PartialView("_List", result);
        }

        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Lawyer lawyer)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _lawyerService.Create(lawyer);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _lawyerService.GetAllLawyer();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(lawyer);

                    }
                }
                else
                {
                    return View(lawyer);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(lawyer);
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _lawyerService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Lawyer lawyer)
        {
            if (ModelState.IsValid)
            {
                var result = await _lawyerService.Update(id, lawyer);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                    var list = await _lawyerService.GetAllLawyer();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(lawyer);
                }
            }
            return View(lawyer);
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _lawyerService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Pankaj
        {
            var result = await _lawyerService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            return RedirectToAction("Index", "Lawyer");
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Pankaj
        {
            try
            {
                var result = await _lawyerService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _lawyerService.GetAllLawyer();
            return View("Index", list);
        }

        public async Task<IActionResult> LawerMasterList()
        {
            var result = await _lawyerService.GetAllLawyer();
            List<LawerMasterListDto> data = new List<LawerMasterListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new LawerMasterListDto()
                    {
                        Id = result[i].Id,
                        LawyerName= result[i].Name,
                        CourtPhoneNo = result[i].CourtPhoneNo,
                        ChamberAddress = result[i].ChamberAddress,
                        ValidForm = result[i].ValidFrom.ToString(),
                        ValidTo = result[i].ValidTo.ToString(),                      
                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
