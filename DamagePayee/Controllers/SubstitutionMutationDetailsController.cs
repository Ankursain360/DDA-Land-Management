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
using Microsoft.AspNetCore.Http;

namespace DamagePayee.Controllers
{
    public class SubstitutionMutationDetailsController : Controller
    {
        public IConfiguration _configuration;
        private readonly IMutationDetailsService _mutationDetailsService;
        string AtsfilePath = "";
        string GPAfilePath = "";
        //string PhotofilePath= "";
        //string SignfilePath= "";
           
        string MoneyRecfilePath= "";
        string SignspcfilePath = "";
        string AddprooffilePath = "";
        string AffidevitfilePath = "";
        string IndemnityfilePath = "";
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

            
            //string PhotofilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:PHOTOFilePath").Value.ToString();
            //string SignfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:SIGNFilePath").Value.ToString();
            string PhotoPropfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:PHOTOPROPFilePath").Value.ToString();
            string GPAstafilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:GPASTAfilePath").Value.ToString();
             //AgreementfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:ATSFilePath").Value.ToString();
            string MoneyRecfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:MONEYRECFilePath").Value.ToString();
            string SignspcfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:SIGNSPECFIRFilePath").Value.ToString();
            string AddprooffilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:ADDPROOFFIRFilePath").Value.ToString();
            string AffidevitfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:AFFIDEVITFIRFilePath").Value.ToString();
            string IndemnityfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:INDEMNITYFIRFilePath").Value.ToString();


            if (ModelState.IsValid)
            {
                //FileHelper fileHelper = new FileHelper();
                var result = await _mutationDetailsService.Create(mutationDetails);

                if (result == true)
                {
                    FileHelper fileHelper = new FileHelper();
                    if (mutationDetails.PropertyPhoto != null && mutationDetails.PropertyPhoto.Count > 0)
                    {
                        List<Mutationdetailsphotoproperty> MutationDetails = new List<Mutationdetailsphotoproperty>();
                        for (int i = 0; i < mutationDetails.PropertyPhoto.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(PhotoPropfilePath, mutationDetails.PropertyPhoto[i]);
                            MutationDetails.Add(new Mutationdetailsphotoproperty
                            {

                                MutationDetailsId = mutationDetails.Id,
                                PhotoPropFilePath = FilePath
                            });
                        }
                        foreach (var item in MutationDetails)
                        {
                            result = await _mutationDetailsService.SaveMutationPhotoPropFile(item);
                        }
                    }

                   
                  
                }

                /* For Ats File Upload*/
                string ATSFileName = "";
                string atsfilePath = "";
               // mutationDetails.AtsfilePathNew = agreementfilePath;
                AtsfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:ATSFilePath").Value.ToString();
                if (mutationDetails.AtsfilePathNew != null)
                {
                    if (!Directory.Exists(AtsfilePath))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(AtsfilePath);
                    }
                    ATSFileName = Guid.NewGuid().ToString() + "_" + mutationDetails.AtsfilePathNew.FileName;
                    atsfilePath = Path.Combine(AtsfilePath, ATSFileName);
                    using (var stream = new FileStream(atsfilePath, FileMode.Create))
                    {
                        mutationDetails.AtsfilePathNew.CopyTo(stream);
                    }
                    mutationDetails.AtsfilePath = atsfilePath;
                }

                /* For GPA File Upload*/
                string GPAFileName = "";
                string gpafilePath = "";

                GPAfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:GPAFilePath").Value.ToString();
                if (mutationDetails.GpafilePathNew != null)
                {
                    if (!Directory.Exists(GPAfilePath))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(GPAfilePath);
                    }
                    GPAFileName = Guid.NewGuid().ToString() + "_" + mutationDetails.GpafilePathNew.FileName;
                    gpafilePath = Path.Combine(GPAfilePath, GPAFileName);
                    using (var stream = new FileStream(gpafilePath, FileMode.Create))
                    {
                        mutationDetails.GpafilePathNew.CopyTo(stream);
                    }
                    mutationDetails.GpafilePath = gpafilePath;
                }

                /* For MoneyReceipt File Upload*/
                string MoneyFileName = "";
                string MoneyfilePath = "";

                MoneyRecfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:MONEYRECFilePath").Value.ToString();
                if (mutationDetails.MoneyfilePathNew != null)
                {
                    if (!Directory.Exists(MoneyRecfilePath))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(MoneyRecfilePath);
                    }
                    MoneyFileName = Guid.NewGuid().ToString() + "_" + mutationDetails.MoneyfilePathNew.FileName;
                    MoneyfilePath = Path.Combine(MoneyRecfilePath, MoneyFileName);
                    using (var stream = new FileStream(MoneyfilePath, FileMode.Create))
                    {
                        mutationDetails.MoneyfilePathNew.CopyTo(stream);
                    }
                    mutationDetails.MoneyRecieptFilePath = MoneyfilePath;
                }

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    var list = await _mutationDetailsService.GetAllMutationDetails();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View();
                }
               
            }
            return View(mutationDetails);

        }

        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? zoneId)
        {
            zoneId = zoneId ?? 0;
            return Json(await _mutationDetailsService.GetAllLocality(Convert.ToInt32(zoneId)));
        }
    }
}

