using Dto.Master;
using GIS.Helper;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Microsoft.AspNetCore.Hosting;
using Dto.Search;
using Core.Enum;
using GIS.Filters;
using System.Linq;

namespace GIS.Controllers
{
    public class DDADecisionSupportSystemReportController : Controller
    {
        private readonly IGISService _GISService;
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IApplicationModificationDetailsService _modificationDetails;
        public IConfiguration _Configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        string InputImages = string.Empty;
        string ChangeDetectionImage = string.Empty;
        public DDADecisionSupportSystemReportController(IGISService GISService, IUserProfileService userProfileService, ISiteContext siteContext, IConfiguration configuration, IHostingEnvironment en, IApplicationModificationDetailsService modificationDetails)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
            _GISService = GISService;
            _Configuration = configuration;
            _hostingEnvironment = en;
            _modificationDetails = modificationDetails;
            InputImages = _Configuration.GetSection("FilePaths:InputImages:FirstPhotoPath").Value.ToString();
            ChangeDetectionImage = _Configuration.GetSection("FilePaths:OutPutImages:ChangedImagePath").Value.ToString();
        }
        public void updateDateFun()
        {
            var updatedDate = _modificationDetails.GetApplicationModificationDetails();
            var dt = Convert.ToDateTime(updatedDate).ToString("dd/MMM/yyyy HH:MM:ss tt");
            if (updatedDate != null)
            {
                TempData["updatedDate"] = dt;

            }
            else
            {
                TempData["updatedDate"] = "No Data Available";

            }

        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            updateDateFun();
            var data = await _GISService.GetAllChangeDetectionData();
            var result = data.Select(x => new { x.Village.Name, x.FirstPhotoPath, x.SecondPhotoPath, x.Village.Id }).ToList();
            List<AIchangeDetectionListSearchDto> obj = new List<AIchangeDetectionListSearchDto>();
            for (int i = 0; i < result.Count; i++)
            {
                obj.Add(new AIchangeDetectionListSearchDto
                {
                    id = result[i].Id,
                    FirstPhoto = result[i].Name + "_" + result[i].FirstPhotoPath.Split('.')[0],
                    SecondPhoto = result[i].Name + "_" + result[i].SecondPhotoPath.Split('.')[0],
                });

            }

            return View(obj);
        }
    }
}
