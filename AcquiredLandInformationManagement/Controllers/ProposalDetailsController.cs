using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace AcquiredLandInformationManagement.Controllers

{
    
        public class ProposalDetailsController : Controller
        {
            private readonly IProposaldetailsService _proposaldetailsService;

            public ProposalDetailsController(IProposaldetailsService proposaldetailsService)
            {
            _proposaldetailsService = proposaldetailsService;
            }
            public async Task<IActionResult> Index()
            {
                var result = await _proposaldetailsService.GetAllProposaldetails();
                return View(result);
            }

        public async Task<IActionResult> Create()
        {
            Proposaldetails proposaldeatils = new Proposaldetails();
            proposaldeatils.IsActive = 1;
            proposaldeatils.SchemeList = await _proposaldetailsService.GetAllScheme();
            return View(proposaldeatils);
        }


        [HttpPost]
       
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


    }
}
