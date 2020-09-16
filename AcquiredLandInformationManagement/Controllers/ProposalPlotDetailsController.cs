using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace DDAPropertyREG.Controllers
{
  
        public class ProposalPlotDetailsController : Controller
        {
            private readonly IProposalplotdetailsService _proposalplotdetailsService; 

            public ProposalPlotDetailsController(IProposalplotdetailsService proposalplotdetailsService)
            {
            _proposalplotdetailsService = proposalplotdetailsService;
            }
            public async Task<IActionResult> Index()
            {
                var result = await _proposalplotdetailsService.GetAllProposalplotdetails();
                return View(result);
            }
        public async Task<IActionResult> Create()
        {
            Proposalplotdetails proposalplotdetails = new Proposalplotdetails();
            //proposalplotdetails.IsActive = 1;

            proposalplotdetails.ProposaldetailsList = await _proposalplotdetailsService.GetAllProposaldetails();
            proposalplotdetails.VillageList = await _proposalplotdetailsService.GetAllVillage();
            //proposalplotdetails.KhasraList = await _proposalplotdetailsService.GetAllKhasra();


            return View(proposalplotdetails);
        }


        [HttpPost]

        public async Task<IActionResult> Create(Proposalplotdetails proposalplotdetails)
        {
            try
            {
                proposalplotdetails.ProposaldetailsList = await _proposalplotdetailsService.GetAllProposaldetails();
                proposalplotdetails.VillageList = await _proposalplotdetailsService.GetAllVillage();
                //proposalplotdetails.KhasraList = await _proposalplotdetailsService.GetAllKhasra();
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
    }
}
