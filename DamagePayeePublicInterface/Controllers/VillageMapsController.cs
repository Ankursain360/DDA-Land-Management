using Dto.Search;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamagePayeePublicInterface.Controllers
{
    public class VillageMapsController : Controller
    {
        private readonly IAcquiredlandvillageService _acquiredlandvillage;

        public VillageMapsController(IAcquiredlandvillageService acquiredlandvillage)
        {
            _acquiredlandvillage = acquiredlandvillage;
        }
        public async Task<IActionResult> Index()
        {
            VillageReportDetailsSearchDto dto = new VillageReportDetailsSearchDto();
            ViewBag.VillageList = await _acquiredlandvillage.GetAllVillageList();
            return View(dto);
          
        }
        //[HttpPost]
        //public async Task<IActionResult> getVillageMaps(string code)
        //{
        //    var provider = new PhysicalFileProvider(webHostEnvironment.WebRootPath);
        //    var contents = provider.GetDirectoryContents(Path.Combine("uploadedfiles", "images"));
        //    var objFiles = contents.OrderBy(m => m.LastModified);

        //    return new JsonResult(objFiles);
            
        //}

    }
}
