using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Service.IApplicationService;
using Libraries.Model.Entity;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Hosting;
using Model.Entity;

namespace DamagePayee.Controllers
{
    public class SubstitutionMutationDetailsController : Controller
    {
        public IConfiguration _configuration;
        private readonly IMutationDetailsService _mutationDetailsService;

        public SubstitutionMutationDetailsController(IMutationDetailsService detailsService, IConfiguration configuration)
        {
            _mutationDetailsService = detailsService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            Mutationdetails details = new Mutationdetails();
            details.ZoneList = await _mutationDetailsService.GetAllZone();
            details.LocalityList = await _mutationDetailsService.GetAllLocality(details.ZoneId);

            return View(details);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Mutationdetails mutationDetails)
        {
            mutationDetails.ZoneList = await _mutationDetailsService.GetAllZone();
            mutationDetails.LocalityList = await _mutationDetailsService.GetAllLocality(mutationDetails.ZoneId);

            string GPAfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:GPAFilePath").Value.ToString();
            string PhotofilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:PHOTOFilePath").Value.ToString();
            string SignfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:SIGNFilePath").Value.ToString();
            string PhotoPropfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:PHOTOPROPFilePath").Value.ToString();
            string GPAgernalfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:GOAGERNFilePath").Value.ToString();
            string AtsfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:ATSFilePath").Value.ToString();
            string MoneyRecfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:MONEYRECFilePath").Value.ToString();
            string SignspcfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:SIGNSPECFIRFilePath").Value.ToString();
            string AddprooffilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:ADDPROOFFIRFilePath").Value.ToString();
            string AffidevitfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:AFFIDEVITFIRFilePath").Value.ToString();
            string IndemnityfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:INDEMNITYFIRFilePath").Value.ToString();

            if (ModelState.IsValid)
            {
                var result = await _mutationDetailsService.Create(mutationDetails);

                if (result == true)
                {
                    FileHelper fileHelper = new FileHelper();
                    if (mutationDetails.PhotoProp != null && mutationDetails.PhotoProp.Count > 0)
                    {
                        List<Mutationdetails> MutationDetails = new List<Mutationdetails>();
                        for (int i = 0; i < mutationDetails.PhotoProp.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(PhotoPropfilePath, mutationDetails.PhotoProp[i]);
                            //MutationDetails.Add(new Mutationdetailsphotoproperty
                            //{

                            //    //MutationDetailsId = mutationDetails.Id,
                            //    //PhotoPropFilePath = FilePath
                            //});
                        }
                        foreach (var item in MutationDetails)
                        {
                            result = await _mutationDetailsService.Create(item);
                        }
                    }
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    var list = await _mutationDetailsService.GetAllMutationDetails();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(mutationDetails);
                }
            }
            else
            {
                return View(mutationDetails);
            }
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? zoneId)
        {
            zoneId = zoneId ?? 0;
            return Json(await _mutationDetailsService.GetAllLocality(Convert.ToInt32(zoneId)));
        }
    }
}

