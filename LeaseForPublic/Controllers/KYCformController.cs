using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Enum;
using LeaseForPublic.Filters;
using Libraries.Service.IApplicationService;

namespace LeaseForPublic.Controllers
{
    public class KYCformController : BaseController
    {
        private readonly IKycformService _kycformService;
       
        public KYCformController(IKycformService KycformService)
          
        {

            _kycformService = KycformService;
           
        }
        public IActionResult Index()
        {
            return View();
        }
       // [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }
    }
}
