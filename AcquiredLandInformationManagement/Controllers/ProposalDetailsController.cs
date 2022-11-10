using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Utility.Helper;

namespace AcquiredLandInformationManagement.Controllers

{
    
        public class ProposalDetailsController : BaseController
        {
            private readonly IProposaldetailsService _proposaldetailsService;

            public ProposalDetailsController(IProposaldetailsService proposaldetailsService)
            {
            _proposaldetailsService = proposaldetailsService;
            }
        [AuthorizeContext(ViewAction.View)]
        public  IActionResult Index()
            {
               
                return View();
            }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] ProposaldetailsSearchDto model)
        {
            var result = await _proposaldetailsService.GetPagedProposaldetails(model);
            return PartialView("_List", result);
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Proposaldetails proposaldeatils = new Proposaldetails();
            proposaldeatils.IsActive = 1;
            proposaldeatils.SchemeList = await _proposaldetailsService.GetAllScheme();
            return View(proposaldeatils);
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Proposaldetails proposaldeatils)
        {
            try
            {
                proposaldeatils.SchemeList = await _proposaldetailsService.GetAllScheme();
                if (ModelState.IsValid)
                {


                    var result = await _proposaldetailsService.Create(proposaldeatils);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _proposaldetailsService.GetAllProposaldetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(proposaldeatils);

                    }
                }
                else
                {
                    return View(proposaldeatils);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(proposaldeatils);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _proposaldetailsService.FetchSingleResult(id);
            Data.SchemeList = await _proposaldetailsService.GetAllScheme();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Proposaldetails proposaldeatils)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _proposaldetailsService.Update(id, proposaldeatils);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _proposaldetailsService.GetAllProposaldetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(proposaldeatils);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(proposaldeatils);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _proposaldetailsService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Page: {Name} already exist");
            }
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _proposaldetailsService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }
            var result = await _proposaldetailsService.GetAllProposaldetails();
            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View("Index", result);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _proposaldetailsService.FetchSingleResult(id);
            Data.SchemeList = await _proposaldetailsService.GetAllScheme();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> ProposalDetailsList([FromBody] ProposaldetailsSearchDto model)
        {
            var result = await _proposaldetailsService.GetAllProposaldetailsList(model);
            List<ProposalDetailsListDto> data = new List<ProposalDetailsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new ProposalDetailsListDto()
                    {
                        Id = result[i].Id,
                        SchemeName = result[i].Scheme == null ? "" : result[i].Scheme.Name,
                        ProposalName = result[i].Name,
                        RequiredAgency = result[i].RequiredAgency,
                        ProposalNoFileNo = result[i].ProposalFileNo,
                        ProposalDate = Convert.ToDateTime(result[i].ProposalDate).ToString("dd-MMM-yyyy"),
                        Area = result[i].Bigha.ToString()
                                  + '-' + result[i].Biswa.ToString()
                                  + '-' + result[i].Biswanshi.ToString(),
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            return Ok();

        }

        [HttpGet]
        public virtual ActionResult download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
