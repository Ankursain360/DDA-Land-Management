using Dto.Search;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LIMSPublicInterface.Controllers
{
    public class VillageMapsController : Controller
    {
        private readonly IAcquiredlandvillageService _acquiredlandvillage;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VillageMapsController(IAcquiredlandvillageService acquiredlandvillage, IWebHostEnvironment webHostEnvironment)
        {
            _acquiredlandvillage = acquiredlandvillage;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            VillageReportDetailsSearchDto dto = new VillageReportDetailsSearchDto();
            ViewBag.VillageList = await _acquiredlandvillage.GetAllVillageList();
            return View(dto);

        }

        public async Task<PartialViewResult> List(int Id)
        {
            VillageReportDetailsSearchDto dto = new VillageReportDetailsSearchDto();
            var result = await _acquiredlandvillage.FetchSingleResult(Id);

            var provider = new PhysicalFileProvider(_webHostEnvironment.WebRootPath);
            var villagesmallmaps = provider.GetDirectoryContents(Path.Combine("VillageMaps", "MAPS"));
            var objFiles = villagesmallmaps.Where(x => x.Name == (result.Code + ".gif"));

            //fullMaps
            var villagefullmaps = provider.GetDirectoryContents(Path.Combine("VillageMaps", "MAPS"));
            var fullimages = villagefullmaps.Where(x => x.Name.ToLower() == (result.Code + "_f.gif"));
            //village Massavies
            var villageMassaviesmaps = provider.GetDirectoryContents(Path.Combine("VillageMaps", "ScannedSheets"));
            var Massaviesimages = villageMassaviesmaps.Where(x => x.Name.StartsWith(result.Code + "_"));

            var ImageList = new List<string>();
            foreach (var item in objFiles.ToList())
            {
                dto.VillageSmallMap = (item.Name);
            }
            foreach (var item in fullimages.ToList())
            {
                dto.VillageFullMap = (item.Name);
            }
            foreach (var item in Massaviesimages.ToList())
            {
                if (dto.VillageMassaviesMap == null)
                {
                    //It's null - create it
                    dto.VillageMassaviesMap = new List<string>(); 
                }
                dto.VillageMassaviesMap.Add(item.Name);

            }

            return PartialView("_Maps", dto);
        }

    }
}
