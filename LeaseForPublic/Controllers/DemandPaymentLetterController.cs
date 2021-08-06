using System;
using Core.Enum;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utility.Helper;

namespace LeaseForPublic.Controllers
{
    public class DemandPaymentLetterController : BaseController
    {
        private readonly IDemandDetailsService _demandDetailsService;
        private readonly IKycformService _kycformService;

        public DemandPaymentLetterController(IDemandDetailsService demandDetailsService, IKycformService kycform)
        
        {
            _demandDetailsService = demandDetailsService;
            _kycformService = kycform;
           
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemandDetailsSearchDto model)
        {
            var result = await _demandDetailsService.GetDemandPaymentDetails(model, "8506092802");

            return PartialView("_List", result);
        }



      
        public async Task<IActionResult> View(int id)
        {
           
            var Data = await _demandDetailsService.GetPaymentDemandLetter(id);
           
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


    }
}
