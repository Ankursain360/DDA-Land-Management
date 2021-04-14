using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using NewLandAcquisition.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Helper;

namespace NewLandAcquisition.Controllers
{
    public class AwardMasterDetailsController : BaseController
    {
        public readonly INewlandawardmasterdetailService _newlandawardmasterdetailService;
        public AwardMasterDetailsController(INewlandawardmasterdetailService newlandawardmasterdetailsService)
        {
            _newlandawardmasterdetailService = newlandawardmasterdetailsService;
        }
        public IActionResult Index()
        {

            return View();
        }


        [AuthorizeContext(ViewAction.View)]
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewlandawardmasterSearchDto model)
        {
            var result = await _newlandawardmasterdetailService.GetPagedawardmasterdetails(model);
            return PartialView("_List", result);
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlandawardmasterdetail model = new Newlandawardmasterdetail();
            model.IsActive = 1;
            model.NewlandvillageList = await _newlandawardmasterdetailService.Getvillage();
            model.section17List = await _newlandawardmasterdetailService.Getundersection17();
            model.section4List = await _newlandawardmasterdetailService.Getundersection4();
            model.section6List = await _newlandawardmasterdetailService.Getundersection6();
            model.purposalList = await _newlandawardmasterdetailService.GetPurposal();
            return View(model);
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandawardmasterdetail awardmasterdetail)
        {
            try
            {
                awardmasterdetail.NewlandvillageList = await _newlandawardmasterdetailService.Getvillage();
                awardmasterdetail.purposalList = await _newlandawardmasterdetailService.GetPurposal();
                awardmasterdetail.section6List = await _newlandawardmasterdetailService.Getundersection6();
                awardmasterdetail.section4List = await _newlandawardmasterdetailService.Getundersection4();
                awardmasterdetail.section17List = await _newlandawardmasterdetailService.Getundersection17();
                if (ModelState.IsValid)
                {
                    var result = await _newlandawardmasterdetailService.Create(awardmasterdetail);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _newlandawardmasterdetailService.Getawardmasterdetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(awardmasterdetail);
                    }
                }
                else
                {
                    return View(awardmasterdetail);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(awardmasterdetail);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newlandawardmasterdetailService.FetchSingleResult(id);
            Data.NewlandvillageList = await _newlandawardmasterdetailService.Getvillage();
            Data.purposalList = await _newlandawardmasterdetailService.GetPurposal();
            Data.section6List = await _newlandawardmasterdetailService.Getundersection6();
            Data.section4List = await _newlandawardmasterdetailService.Getundersection4();
            Data.section17List = await _newlandawardmasterdetailService.Getundersection17();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newlandawardmasterdetailService.FetchSingleResult(id);
            Data.NewlandvillageList = await _newlandawardmasterdetailService.Getvillage();
            Data.purposalList = await _newlandawardmasterdetailService.GetPurposal();
            Data.section6List = await _newlandawardmasterdetailService.Getundersection6();
            Data.section4List = await _newlandawardmasterdetailService.Getundersection4();
            Data.section17List = await _newlandawardmasterdetailService.Getundersection17();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandawardmasterdetail newlandawardmasterdetail)
        {
            newlandawardmasterdetail.NewlandvillageList = await _newlandawardmasterdetailService.Getvillage();
            newlandawardmasterdetail.purposalList = await _newlandawardmasterdetailService.GetPurposal();
            newlandawardmasterdetail.section6List = await _newlandawardmasterdetailService.Getundersection6();
            newlandawardmasterdetail.section4List = await _newlandawardmasterdetailService.Getundersection4();
            newlandawardmasterdetail.section17List = await _newlandawardmasterdetailService.Getundersection17();
            if (ModelState.IsValid)
            {
                var result = await _newlandawardmasterdetailService.Update(id, newlandawardmasterdetail);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    var list = await _newlandawardmasterdetailService.Getawardmasterdetails();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(newlandawardmasterdetail);
                }
            }
            else
            {
                return View(newlandawardmasterdetail);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality 
        {
            var result = await _newlandawardmasterdetailService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _newlandawardmasterdetailService.Getawardmasterdetails();
            return View("Index", list);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string AwardNumber)
        {
            var result = await _newlandawardmasterdetailService.CheckUniqueName(Id, AwardNumber);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Award Number : {AwardNumber} already exist");
            }
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> ExistCode(int Id, string name)
        {
            var result = await _newlandawardmasterdetailService.CheckUniqueName(Id, name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Award Number : {name} already exist");
            }
        }


        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Newlandawardmasterdetail> result = await _newlandawardmasterdetailService.GetAll();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"AwardMaster.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }


    }
}
