using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using LeaseDetails.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dto.Search;
using LeaseDetails.Filters;
using Core.Enum;
using Utility.Helper;
using Microsoft.Extensions.Configuration;
using Dto.Master;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace LeaseDetails.Controllers
{
    

    public class AllotmentLetterController : BaseController
    {
        private readonly ILeaseApplicationFormService _leaseApplicationFormService;
        public IConfiguration _configuration;

        public AllotmentLetterController(ILeaseApplicationFormService leaseApplicationFormService, IConfiguration configuration)
        {
            _leaseApplicationFormService = leaseApplicationFormService;
            _configuration = configuration;
           
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DocumentChecklistSearchDto model)
        {
            var result = await _leaseApplicationFormService.GetPagedAllotmentLetter(model);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> View(int id)
        {
            var Data = await _leaseApplicationFormService.FetchLeaseApplicationDetails(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
           // return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetGroundRateList(int id, string dn,string rn)
        {
            var Data = await _leaseApplicationFormService.FetchLeaseApplicationDetails(id);
            ViewBag.NewDate = dn;
            ViewBag.NewRefNo = rn;
          // RedirectToAction("AllotmentLetter", "AllotmentLetter");
           return Json(await _leaseApplicationFormService.FetchLeaseApplicationDetails(id));

            // return PartialView("AllotmentLetter", Data);
        }
        public async Task<IActionResult> Create(Leaseapplication leaseapp, string dn, string rn)
        {
            try
            {
             
                var Data = await _leaseApplicationFormService.FetchLeaseApplicationDetails(leaseapp.Id);

                if (Data == null)
                {
                    return NotFound();
                }
                return View("Create", Data);

            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(leaseapp);
            }
        }

       
    }
}
