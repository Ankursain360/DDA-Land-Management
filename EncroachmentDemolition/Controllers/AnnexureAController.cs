using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;

namespace EncroachmentDemolition.Controllers
{
    public class AnnexureAController : Controller
    {
        //  private readonly  IAnnexureAService _annexureAService;

       // public IConfiguration _configuration;
        public readonly IEncroachmentRegisterationService _encroachmentRegisterationService;

        //public AnnexureAController(IAnnexureAService annexureAService)
        //{
        //    _encroachmentRegisterationService = encroachmentRegisterationService;

        //    _annexureAService = annexureAService;
        //}

        public AnnexureAController(IEncroachmentRegisterationService encroachmentRegisterationService)
        {
            _encroachmentRegisterationService = encroachmentRegisterationService;
         //   _configuration = configuration;
        }




        public IActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<PartialViewResult> List([FromBody] EncroachmentRegisterationDto model)
        //{
        //    var result = await _encroachmentRegisterationService.GetPagedEncroachmentRegisteration(model);
        //    return PartialView("_List", result);
        //}



        //public IActionResult Index()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Create()
        {

           

           

      //     var list = await _annexureAService.GetDemolitionchecklist();
            
           // var list1 = await _annexureAService.GetDemolitiondocument();
            return View();
           // return View(list1);
        }

    }
}