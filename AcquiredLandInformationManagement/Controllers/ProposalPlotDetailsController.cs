using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
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
        public async Task<IActionResult> Index()
            {
                var result = await _proposalplotdetailsService.GetAllProposalplotdetails();
                return View(result);
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
            //proposalplotdetails.IsActive = 1;

            proposalplotdetails.ProposaldetailsList = await _proposalplotdetailsService.GetAllProposaldetails();
            proposalplotdetails.LocalityList = await _proposalplotdetailsService.GetAllLocality();
            proposalplotdetails.KhasraList = await _proposalplotdetailsService.GetAllKhasra();


            return View(proposalplotdetails);
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Proposalplotdetails proposalplotdetails)
        {
            try
            {
                proposalplotdetails.ProposaldetailsList = await _proposalplotdetailsService.GetAllProposaldetails();
                proposalplotdetails.LocalityList = await _proposalplotdetailsService.GetAllLocality();
                proposalplotdetails.KhasraList = await _proposalplotdetailsService.GetAllKhasra();
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
            Data.LocalityList = await _proposalplotdetailsService.GetAllLocality();
            
            Data.KhasraList = await _proposalplotdetailsService.GetAllKhasra();


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
            Data.LocalityList = await _proposalplotdetailsService.GetAllLocality();
            Data.KhasraList = await _proposalplotdetailsService.GetAllKhasra();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
      //  [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Proposalplotdetails> result = await _proposalplotdetailsService.GetAllProposalplotdetails();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Proposalplotdetails.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
    }
}
