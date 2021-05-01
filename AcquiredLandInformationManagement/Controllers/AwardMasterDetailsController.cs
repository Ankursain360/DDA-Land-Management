using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using AcquiredLandInformationManagement.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Helper;
using Dto.Master;

namespace AcquiredLandInformationManagement.Controllers
{
    public class AwardMasterDetailsController : BaseController
    {
        public readonly IAwardmasterdetailsService _awardmasterdetailsService;
        public AwardMasterDetailsController(IAwardmasterdetailsService awardmasterdetailsService)
        {
            _awardmasterdetailsService = awardmasterdetailsService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {

            return View();
        }
        
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] AwardMasterDetailsSearchDto model)
        {
            var result = await _awardmasterdetailsService.GetPagedawardmasterdetails(model);
            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Awardmasterdetail model = new Awardmasterdetail();
            model.IsActive = 1;
            model.AcquiredlandvillageList = await _awardmasterdetailsService.Getvillage();
            model.section17List = await _awardmasterdetailsService.Getundersection17();
            model.section4List = await _awardmasterdetailsService.Getundersection4();
            model.section6List = await _awardmasterdetailsService.Getundersection6();
            model.purposalList = await _awardmasterdetailsService.GetPurposal();
            return View(model);
        }
      
      
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
            {
                var Data = await _awardmasterdetailsService.FetchSingleResult(id);
                 Data.AcquiredlandvillageList = await _awardmasterdetailsService.Getvillage();
                 Data.purposalList = await _awardmasterdetailsService.GetPurposal();
                  Data.section6List = await _awardmasterdetailsService.Getundersection6();
                  Data.section4List = await _awardmasterdetailsService.Getundersection4();
                   Data.section17List = await _awardmasterdetailsService.Getundersection17();
            if (Data == null)
                {
                    return NotFound();
                }
                return View(Data);
            }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
            {
            var Data = await _awardmasterdetailsService.FetchSingleResult(id);
            Data.AcquiredlandvillageList = await _awardmasterdetailsService.Getvillage();
            Data.purposalList = await _awardmasterdetailsService.GetPurposal();
            Data.section6List = await _awardmasterdetailsService.Getundersection6();
            Data.section4List = await _awardmasterdetailsService.Getundersection4();
            Data.section17List = await _awardmasterdetailsService.Getundersection17();
            if (Data == null)
                {
                    return NotFound();
                }
                return View(Data);
            }
        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Awardmasterdetail awardmasterdetail)
        {
            awardmasterdetail.AcquiredlandvillageList = await _awardmasterdetailsService.Getvillage();
            awardmasterdetail.purposalList = await _awardmasterdetailsService.GetPurposal();
            awardmasterdetail.section6List = await _awardmasterdetailsService.Getundersection6();
            awardmasterdetail.section4List = await _awardmasterdetailsService.Getundersection4();
            awardmasterdetail.section17List = await _awardmasterdetailsService.Getundersection17();
            if (ModelState.IsValid)
            {
                var result = await _awardmasterdetailsService.Update(id, awardmasterdetail);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    var list = await _awardmasterdetailsService.Getawardmasterdetails();
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
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality 
        {
            var result = await _awardmasterdetailsService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _awardmasterdetailsService.Getawardmasterdetails();
            return View("Index", list);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string AwardNumber)
        {
            var result = await _awardmasterdetailsService.CheckUniqueName(Id, AwardNumber);
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
            var result = await _awardmasterdetailsService.CheckUniqueName(Id, name);
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
       
        public async Task<IActionResult> AwardmasterdetailList()
        {
            var result =  await _awardmasterdetailsService.Getawardmasterdetails();
            List<AwardmasterdetailListDto> data = new List<AwardmasterdetailListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new AwardmasterdetailListDto()
                    {
                        Id = result[i].Id,
                        AwardNumber = result[i].AwardNumber,
                        Awarddate = Convert.ToDateTime(result[i].AwardDate).ToString("dd-MMM-yyyy"),
                        VillageName = result[i].Acquiredlandvillage == null ? "" : result[i].Acquiredlandvillage.Name,
                        ProposalName = result[i].Proposal == null ? "" : result[i].Proposal.Name,

                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Awardmasterdetail Award)
        {
            try
            {
                Award.AcquiredlandvillageList = await _awardmasterdetailsService.Getvillage();
                Award.purposalList = await _awardmasterdetailsService.GetPurposal();
                Award.section6List = await _awardmasterdetailsService.Getundersection6();
                Award.section4List = await _awardmasterdetailsService.Getundersection4();
                Award.section17List = await _awardmasterdetailsService.Getundersection17();

                if (ModelState.IsValid)
                {
                   

                    var result = await _awardmasterdetailsService.Create(Award);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);

                        var list = await _awardmasterdetailsService.Getawardmasterdetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Award);

                    }
                }
                else
                {
                    return View(Award);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(Award);
            }
        }
    }
}
