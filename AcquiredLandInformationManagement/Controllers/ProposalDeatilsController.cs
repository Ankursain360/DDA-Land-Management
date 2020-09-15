using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;

namespace AcquiredLandInformationManagement.Controllers

{
    
        public class ProposalDeatilsController : Controller
        {
            private readonly IProposaldetailsService _proposaldetailsService;

            public ProposalDeatilsController(IProposaldetailsService proposaldetailsService)
            {
            _proposaldetailsService = proposaldetailsService;
            }
            public async Task<IActionResult> Index()
            {
                var result = await _proposaldetailsService.GetAllProposaldetails();
                return View(result);
            }

            public IActionResult Create()
        {
            return View();
        }
    }
}
