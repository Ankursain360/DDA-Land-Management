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
  
        public class ProposalPlotDetailsController : Controller
        {
            private readonly IProposalplotdetailsService _proposalplotdetailsService; 

            public ProposalPlotDetailsController(IProposalplotdetailsService proposalplotdetailsService)
            {
            _proposalplotdetailsService = proposalplotdetailsService;
            }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
            {
                return View();
            }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] ProposalplotdetailSearchDto model)
        {
            var result = await _proposalplotdetailsService.GetPagedProposalplotdetails(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Proposalplotdetails proposalplotdetails = new Proposalplotdetails();
            
            proposalplotdetails.ProposaldetailsList = await _proposalplotdetailsService.GetAllProposaldetails();
            proposalplotdetails.AcquiredlandvillageList = await _proposalplotdetailsService.GetAllVillage();
            proposalplotdetails.KhasraList = await _proposalplotdetailsService.GetAllKhasra(proposalplotdetails.AcquiredlandvillageId);


            return View(proposalplotdetails);
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Proposalplotdetails proposalplotdetails)
        {
            try
            {
                proposalplotdetails.ProposaldetailsList = await _proposalplotdetailsService.GetAllProposaldetails();
                proposalplotdetails.AcquiredlandvillageList = await _proposalplotdetailsService.GetAllVillage();
                proposalplotdetails.KhasraList = await _proposalplotdetailsService.GetAllKhasra(proposalplotdetails.AcquiredlandvillageId);

                if (ModelState.IsValid)
                {


                    var result = await _proposalplotdetailsService.Create(proposalplotdetails);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _proposalplotdetailsService.GetAllProposalplotdetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(proposalplotdetails);

                    }
                }
                else
                {
                    return View(proposalplotdetails);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(proposalplotdetails);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _proposalplotdetailsService.FetchSingleResult(id);
            Data.ProposaldetailsList = await _proposalplotdetailsService.GetAllProposaldetails();
            Data.AcquiredlandvillageList = await _proposalplotdetailsService.GetAllVillage();
            Data.KhasraList = await _proposalplotdetailsService.GetAllKhasra(Data.AcquiredlandvillageId);



            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Proposalplotdetails proposalplotdetails)
        {

            proposalplotdetails.ProposaldetailsList = await _proposalplotdetailsService.GetAllProposaldetails();
            proposalplotdetails.AcquiredlandvillageList = await _proposalplotdetailsService.GetAllVillage();
            proposalplotdetails.KhasraList = await _proposalplotdetailsService.GetAllKhasra(proposalplotdetails.AcquiredlandvillageId);

            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _proposalplotdetailsService.Update(id, proposalplotdetails);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _proposalplotdetailsService.GetAllProposalplotdetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(proposalplotdetails);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(proposalplotdetails);
        }



        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _proposalplotdetailsService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }
            var result = await _proposalplotdetailsService.GetAllProposalplotdetails();
            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View("Index", result);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _proposalplotdetailsService.FetchSingleResult(id);
           
            Data.ProposaldetailsList = await _proposalplotdetailsService.GetAllProposaldetails();

            Data.AcquiredlandvillageList = await _proposalplotdetailsService.GetAllVillage();
            Data.KhasraList = await _proposalplotdetailsService.GetAllKhasra(Data.AcquiredlandvillageId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
      
        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _proposalplotdetailsService.GetAllKhasra(Convert.ToInt32(villageId)));
        }
        [AuthorizeContext(ViewAction.Download)]
       

        public async Task<IActionResult> ProposalplotdetailsList([FromBody] ProposalplotdetailSearchDto model)
        {
            var result = await _proposalplotdetailsService.GetAllProposalplotdetailsList(model);
            List<ProposalplotdetailsListDto> data = new List<ProposalplotdetailsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new ProposalplotdetailsListDto()
                    {
                        Id = result[i].Id,
                        ProposalName = result[i].Proposaldetails == null ? "" : result[i].Proposaldetails.Name,
                        VillageName = result[i].Acquiredlandvillage == null ? "" : result[i].Acquiredlandvillage.Name,
                        KhasraNo = result[i].Khasra == null ? "" : result[i].Khasra.Name,

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
