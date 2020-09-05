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
using SiteMaster.Models;

namespace SiteMaster.Controllers
{
    public class DesignationController : Controller
    {

        private readonly IDesignationService _designationService;

        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _designationService.GetAllDesignation();
            return View(result);
            //return View(_context.TblMasterDesignation.Where(x => x.IsActive == 1).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        //public async Task<IActionResult> Edit()
        //{
        //    var result = await _designationService.Update();
        //}

    
    }

}
