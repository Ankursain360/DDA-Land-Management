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
        private readonly IDamagepayeeregisterService _damagepayeeregisterService;
       
        string AtsfilePath = "";
        string GPAfilePath = "";
        //string PhotofilePath= "";
        //string SignfilePath= "";

        string MoneyRecfilePath = "";
        string SignspcfilePath = "";
        string AddprooffilePath = "";
        string AffidevitfilePath = "";
        string IndemnityfilePath = "";
        public SubstitutionMutationDetailsController(IMutationDetailsService detailsService, IConfiguration configuration,IDamagepayeeregisterService damagePayeeReg)
        {
            _mutationDetailsService = detailsService;
            _configuration = configuration;
            _damagepayeeregisterService = damagePayeeReg;
        }
        public async Task<IActionResult> Index(int id)
        {
            Mutationdetails Model = new Mutationdetails();
            var Data = await _damagepayeeregisterService.FetchSingleResult(id);

            Data.PropLocalityList = await _damagepayeeregisterService.GetLocalityList();
            Data.PropDistrictList = await _damagepayeeregisterService.GetDistrictList();

            Data.PersonalInfoDamageList = await _damagepayeeregisterService.GetPersonalInfoTemp(id);
            Data.AlloteeTypeDamageList = await _damagepayeeregisterService.GetAllottetypeTemp(id);

            Data.DamagePayeeRegisterList = await _damagepayeeregisterService.GetAllDamagepayeeregisterTemp();
            Model.DamagePayeeRegister = Data;

            return View(Model);
        }


        public async Task<IActionResult> Create(int id)
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


            string PhotoPropfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:PHOTOPROPFilePath").Value.ToString();

            if (true)
            {

                var result = await _mutationDetailsService.Create(mutationDetails);

                if (result == true)
                {

                    /*Previous Damage Assessee Details Repeater*/

                    FileHelper fileHelper = new FileHelper();
                    if (mutationDetails.Name != null &&
                        mutationDetails.FatherName != null &&
                        mutationDetails.DateGpadead != null &&
                        mutationDetails.GpastafilePath != null &&
                        mutationDetails.Name2.Count > 0 &&
                        mutationDetails.FatherName2.Count > 0 &&
                        mutationDetails.DateGpadead.Count > 0 &&
                        mutationDetails.GpastafilePath.Count > 0)


                    {
                        List<Mutationolddamageassesse> oldDamageAssess = new List<Mutationolddamageassesse>();
                        for (int i = 0; i < mutationDetails.Name2.Count; i++)
                        {
                            oldDamageAssess.Add(new Mutationolddamageassesse
                            {
                                Name = mutationDetails.Name2[i],
                                FatherName = mutationDetails.FatherName2[i],
                                DateGpadead = mutationDetails.DateGpadead[i],
                                GpastafilePath = mutationDetails.GpastafilePath[i],
                                MutationDetailsId = mutationDetails.Id
                            });
                        }
                        foreach (var item in oldDamageAssess)
                        {
                            result = await _mutationDetailsService.SaveMutationOldDamage(item);
                        }
                    }

                    /*For Photo of property file upload */

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

                    /* For Signature Specimen File Upload*/
                    string SignSpecFileName = "";
                    string signSpecfilePath = "";

                    SignspcfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:SIGNSPECFIRFilePath").Value.ToString();
                    if (mutationDetails.SignSpecPathNew != null)
                    {
                        if (!Directory.Exists(SignspcfilePath))
                        {
                            // Try to create the directory.
                            DirectoryInfo di = Directory.CreateDirectory(SignspcfilePath);
                        }
                        SignSpecFileName = Guid.NewGuid().ToString() + "_" + mutationDetails.SignSpecPathNew.FileName;
                        signSpecfilePath = Path.Combine(SignspcfilePath, SignSpecFileName);
                        using (var stream = new FileStream(signSpecfilePath, FileMode.Create))
                        {
                            mutationDetails.SignSpecPathNew.CopyTo(stream);
                        }
                        mutationDetails.SignatureSpecimenFilePath = signSpecfilePath;
                    }

                    /* For Address proof File Upload*/
                    string AddProofFileName = "";
                    string addProoffilePath = "";

                    AddprooffilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:ADDPROOFFIRFilePath").Value.ToString();
                    if (mutationDetails.AddressProofPathNew != null)
                    {
                        if (!Directory.Exists(AddprooffilePath))
                        {
                            // Try to create the directory.
                            DirectoryInfo di = Directory.CreateDirectory(AddprooffilePath);
                        }
                        AddProofFileName = Guid.NewGuid().ToString() + "_" + mutationDetails.AddressProofPathNew.FileName;
                        addProoffilePath = Path.Combine(AddprooffilePath, AddProofFileName);
                        using (var stream = new FileStream(addProoffilePath, FileMode.Create))
                        {
                            mutationDetails.AddressProofPathNew.CopyTo(stream);
                        }
                        mutationDetails.AddressProofFilePath = addProoffilePath;
                    }

                    /* For AffiDevit File Upload*/
                    string AffidevitFileName = "";
                    string affidevitfilePath = "";

                    AffidevitfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:AFFIDEVITFIRFilePath").Value.ToString();
                    if (mutationDetails.AffidavitPathNew != null)
                    {
                        if (!Directory.Exists(AffidevitfilePath))
                        {
                            // Try to create the directory.
                            DirectoryInfo di = Directory.CreateDirectory(AffidevitfilePath);
                        }
                        AffidevitFileName = Guid.NewGuid().ToString() + "_" + mutationDetails.AffidavitPathNew.FileName;
                        affidevitfilePath = Path.Combine(AffidevitfilePath, AffidevitFileName);
                        using (var stream = new FileStream(affidevitfilePath, FileMode.Create))
                        {
                            mutationDetails.AffidavitPathNew.CopyTo(stream);
                        }
                        mutationDetails.AffidavitFilePath = affidevitfilePath;
                    }

                    /* For IndemnityBond File Upload*/
                    string IndemnitybondFileName = "";
                    string IindemnityBondfilePath = "";

                    IndemnityfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:INDEMNITYFIRFilePath").Value.ToString();
                    if (mutationDetails.IndemnityPathNew != null)
                    {
                        if (!Directory.Exists(IndemnityfilePath))
                        {
                            // Try to create the directory.
                            DirectoryInfo di = Directory.CreateDirectory(IndemnityfilePath);
                        }
                        IndemnitybondFileName = Guid.NewGuid().ToString() + "_" + mutationDetails.IndemnityPathNew.FileName;
                        IindemnityBondfilePath = Path.Combine(IndemnityfilePath, IndemnitybondFileName);
                        using (var stream = new FileStream(IindemnityBondfilePath, FileMode.Create))
                        {
                            mutationDetails.IndemnityPathNew.CopyTo(stream);
                        }
                        mutationDetails.IndemnityFilePath = IindemnityBondfilePath;
                    }

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
        }

        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? zoneId)
        {
            zoneId = zoneId ?? 0;
            return Json(await _mutationDetailsService.GetAllLocality(Convert.ToInt32(zoneId)));
        }
    }
}

