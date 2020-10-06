using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using SiteMaster.Controllers;

namespace LandTransfer.Controllers
{
    public class LandTransferController : BaseController
    {
        public readonly ILandTransferService _landTransferService;
        public LandTransferController(ILandTransferService landTransferService)
        {
            _landTransferService = landTransferService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            //LandTransfer model = new LandTransfer();
            return View();
        }
    }
}
