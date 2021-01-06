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
using DamagePayee.Helper;

namespace DamagePayee.Controllers
{
    public class SubstitutionMutationDetailsController : BaseController
    {
        public IConfiguration _configuration;
        private readonly IMutationDetailsService _mutationDetailsService;
        private readonly IDamagepayeeregisterService _damagepayeeregisterService;

        string PropPhotofilePath = "";
        string AtsfilePath = "";
        string GPAfilePath = "";
        //string PhotofilePath= "";
        //string SignfilePath= "";

        string MoneyRecfilePath = "";
        string SignspcfilePath = "";
        string AddprooffilePath = "";
        string AffidevitfilePath = "";
        string IndemnityfilePath = "";
        public SubstitutionMutationDetailsController(IMutationDetailsService detailsService, IConfiguration configuration, IDamagepayeeregisterService damagePayeeReg)
        {
            _mutationDetailsService = detailsService;
            _configuration = configuration;
            _damagepayeeregisterService = damagePayeeReg;
        }
        public IActionResult Index1()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] SubstitutionMutationDetailsDto model)
        {
            var result = await _mutationDetailsService.GetPagedSubsitutionMutationDetails(model);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Index(int id)
        {
            Mutationdetails Model = new Mutationdetails();
            var Data = await _damagepayeeregisterService.FetchSingleResult(id);

            Data.PropLocalityList = await _damagepayeeregisterService.GetLocalityList();
            Data.PropDistrictList = await _damagepayeeregisterService.GetDistrictList();

            Data.PersonalInfoDamageList = await _damagepayeeregisterService.GetPersonalInfoTemp(id);
            Data.AlloteeTypeDamageList = await _damagepayeeregisterService.GetAllottetypeTemp(id);

            //Data.DamagePayeeRegisterList = await _damagepayeeregisterService.GetAllDamagepayeeregisterTemp();
            Model.DamagePayeeRegister = Data;

            return View(Model);
        }

        async Task BindDropDown(Damagepayeeregistertemp DamagePayeeRegister)
        {
            Damagepayeeregistertemp damage = new Damagepayeeregistertemp();

            DamagePayeeRegister.LocalityList = await _mutationDetailsService.GetLocalityList();
            DamagePayeeRegister.DistrictList = await _mutationDetailsService.GetDistrictList();
        }
        public async Task<IActionResult> Create(int id)
        {
            Mutationdetailstemp mutationdetailstemp = new Mutationdetailstemp();

            mutationdetailstemp.DamagePayeeRegister = await _mutationDetailsService.FetchMutationDetailsUserId(id);
            ViewBag.Locality = await _mutationDetailsService.GetLocalityList();
            ViewBag.District = await _mutationDetailsService.GetDistrictList();
            if (mutationdetailstemp != null)
            {
                return View(mutationdetailstemp);
            }
            else
            {
                return View(mutationdetailstemp);
            }


        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, Mutationdetailstemp mutationDetails)
        {
            var Data = await _damagepayeeregisterService.FetchSingleResult(id);
          
            string deathCertificate = _configuration.GetSection("FilePaths:MutationDetaliFiles:DeathCertificate").Value.ToString();
            string relationshipProof = _configuration.GetSection("FilePaths:MutationDetaliFiles:RelationshipProof").Value.ToString();
            string affitDevitLegal = _configuration.GetSection("FilePaths:MutationDetaliFiles:AffitdevitLegal").Value.ToString();
            string noObjectionCertificate = _configuration.GetSection("FilePaths:MutationDetaliFiles:NoObjectionCertificate").Value.ToString();
            string signatureOfSpec = _configuration.GetSection("FilePaths:MutationDetaliFiles:SignatureOfSpecimen").Value.ToString();

            if (true)
            {
                FileHelper fileHelper = new FileHelper();
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
              
                if (mutationDetails.DeathCertificatePathNew != null)
                {
                    mutationDetails.DeathCertificatePath = fileHelper.SaveFile(deathCertificate, mutationDetails.DeathCertificatePathNew);
                }
                if (mutationDetails.RelationshipUploadPathNew != null)
                {
                    mutationDetails.RelationshipUploadPath = fileHelper.SaveFile(relationshipProof, mutationDetails.RelationshipUploadPathNew);
                }
                if (mutationDetails.AffidevitLegalUploadPathNew != null)
                {
                    mutationDetails.AffidevitLegalUploadPath = fileHelper.SaveFile(affitDevitLegal, mutationDetails.AffidevitLegalUploadPathNew);
                }
                if (mutationDetails.NonObjectUploadPathNew != null)
                {
                    mutationDetails.NonObjectHeirUploadPath = fileHelper.SaveFile(noObjectionCertificate, mutationDetails.NonObjectUploadPathNew);
                }
                if (mutationDetails.SpecimenSignLegalUploadNew != null)
                {
                    mutationDetails.SpecimenSignLegalUpload = fileHelper.SaveFile(signatureOfSpec, mutationDetails.SpecimenSignLegalUploadNew);
                }

                var result = await _mutationDetailsService.Create(mutationDetails);


                //if (result == true)
                //{
                    /*Previous Damage Assessee Details Repeater*/

                    // FileHelper fileHelper = new FileHelper();
                    //    if (mutationDetails.Name != null &&
                    //        mutationDetails.FatherName != null &&
                    //        mutationDetails.DateGpadead != null &&
                    //        mutationDetails.GpastafilePath != null &&
                    //        mutationDetails.Name2.Count > 0 &&
                    //        mutationDetails.FatherName2.Count > 0 &&
                    //        mutationDetails.DateGpadead.Count > 0 &&
                    //        mutationDetails.GpastafilePath.Count > 0)


                    //    {
                    //        List<Mutationolddamageassesse> oldDamageAssess = new List<Mutationolddamageassesse>();
                    //        for (int i = 0; i < mutationDetails.Name2.Count; i++)
                    //        {
                    //            oldDamageAssess.Add(new Mutationolddamageassesse
                    //            {
                    //                Name = mutationDetails.Name2[i],
                    //                FatherName = mutationDetails.FatherName2[i],
                    //                DateGpadead = mutationDetails.DateGpadead[i],
                    //                GpastafilePath = mutationDetails.GpastafilePath[i],
                    //                MutationDetailsId = mutationDetails.Id
                    //            });
                    //        }
                    //        foreach (var item in oldDamageAssess)
                    //        {
                    //            result = await _mutationDetailsService.SaveMutationOldDamage(item);
                    //        }
                    //    }

                    //    if (mutationDetails.Name != null &&
                    //        mutationDetails.FatherName != null &&
                    //        mutationDetails.DateGpadead != null &&
                    //        mutationDetails.GpastafilePath != null &&
                    //        mutationDetails.Name2.Count > 0 &&
                    //        mutationDetails.FatherName2.Count > 0 &&
                    //        mutationDetails.DateGpadead.Count > 0 &&
                    //        mutationDetails.GpastafilePath.Count > 0)


                    //    {
                    //        List<Mutationolddamageassesse> oldDamageAssess = new List<Mutationolddamageassesse>();
                    //        for (int i = 0; i < mutationDetails.Name2.Count; i++)
                    //        {
                    //            oldDamageAssess.Add(new Mutationolddamageassesse
                    //            {
                    //                Name = mutationDetails.Name2[i],
                    //                FatherName = mutationDetails.FatherName2[i],
                    //                DateGpadead = mutationDetails.DateGpadead[i],
                    //                GpastafilePath = mutationDetails.GpastafilePath[i],
                    //                MutationDetailsId = mutationDetails.Id
                    //            });
                    //        }
                    //        foreach (var item in oldDamageAssess)
                    //        {
                    //            result = await _mutationDetailsService.SaveMutationOldDamage(item);
                    //        }
                    //    }
                //}


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

        public async Task<JsonResult> AlloteeTypeRepeter(int? Id)
        {
            Id = Id ?? 0;
            var data = await _damagepayeeregisterService.GetNewAlloteeRepeater(Convert.ToInt32(Id));
            //return Json(data.Select(x => new { x.CountOfStructure, DateOfEncroachment = Convert.ToDateTime(x.DateOfEncroachment).ToString("yyyy-MM-dd"), x.Area, x.NameOfStructure, x.ReferenceNoOnLocation, x.Type, x.ConstructionStatus }));
            return Json(data.Select(x => new { x.Name, x.FatherName, Date = Convert.ToDateTime(x.Date).ToString("yyyy-MM-dd"), x.AtsgpadocumentPath }));
        }

        public async Task<JsonResult> PreviousDamageAssesseeRepeter(int? Id)
        {
            Id = Id ?? 0;
            var data = await _damagepayeeregisterService.GetPreviousAssesseRepeater(Convert.ToInt32(Id));
            //return Json(data.Select(x => new { x.CountOfStructure, DateOfEncroachment = Convert.ToDateTime(x.DateOfEncroachment).ToString("yyyy-MM-dd"), x.Area, x.NameOfStructure, x.ReferenceNoOnLocation, x.Type, x.ConstructionStatus }));
            return Json(data.Select(x => new { x.Name, x.FatherName, x.Gender, x.Address, x.MobileNo, x.EmailId, x.AadharNo, x.AadharNoFilePath, x.PanNo, x.PanNoFilePath, x.PhotographPath, x.SignaturePath }));
        }

        public async Task<IActionResult> DownloadATSFile(int Id)
        {
            FileHelper file = new FileHelper();
            Allottetypetemp Data = await _damagepayeeregisterService.GetATSFilePath(Id);
            string filename = Data.AtsgpadocumentPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadAadharPathFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfotemp Data = await _damagepayeeregisterService.GetAadharFilePath(Id);
            string filename = Data.AadharNoFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadPanPathFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfotemp Data = await _damagepayeeregisterService.GetPanFilePath(Id);
            string filename = Data.PanNoFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadPhotographPathFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfotemp Data = await _damagepayeeregisterService.GetPhotographPath(Id);
            string filename = Data.PhotographPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadSignaturePathFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfotemp Data = await _damagepayeeregisterService.GetSignaturePath(Id);
            string filename = Data.SignaturePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }

        public async Task<IActionResult> DownloadPropertyPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregistertemp Data = await _damagepayeeregisterService.GetPropertyPhotoPath(Id);
            string filename = Data.PropertyPhotoPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadAgreementFile(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetails Data = await _mutationDetailsService.SaveMutationAtsFilePath(Id);
            string filename = Data.AtsfilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadGPAFile(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetails Data = await _mutationDetailsService.SaveMutationGPAFilePath(Id);
            string filename = Data.GpafilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadMoneyReceiptFile(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetails Data = await _mutationDetailsService.SaveMutationMoneyReceiptFilePath(Id);
            string filename = Data.MoneyRecieptFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadSignSpecFile(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetails Data = await _mutationDetailsService.SaveMutationSignSPCFilePath(Id);
            string filename = Data.SignatureSpecimenFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadAddressProofFile(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetails Data = await _mutationDetailsService.SaveMutationAddressProofFilePath(Id);
            string filename = Data.AddressProofFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadAffitDevitFile(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetails Data = await _mutationDetailsService.SaveMutationAffitDevitFilePath(Id);
            string filename = Data.AffidavitFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadIndemnityFile(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetails Data = await _mutationDetailsService.SaveMutationIndemnityFilePath(Id);
            string filename = Data.IndemnityFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<JsonResult> GetDetailspersonelinfotemp(int? Id)
        {
            Id = Id ?? 0;
            var data = await _mutationDetailsService.GetPersonalInfo(Convert.ToInt32(Id));
            return Json(data.Select(x => new {
                x.Id,
                x.Name,
                x.FatherName,
                x.Gender,
                x.Address,
                x.MobileNo,
                x.EmailId,
                x.AadharNoFilePath,
                x.PanNoFilePath,
                x.PhotographPath,
                x.SignaturePath,
                x.AadharNo,
                x.PanNo
            }));
        }
        public async Task<JsonResult> GetDetailsAllottetypetemp(int? Id)
        {
            Id = Id ?? 0;
            var data = await _mutationDetailsService.GetAllottetype(Convert.ToInt32(Id));
            return Json(data.Select(x => new {
                x.Id,
                x.Name,
                x.FatherName,
                Date = Convert.ToDateTime(x.Date).ToString("yyyy-MM-dd"),
                x.AtsgpadocumentPath
            }));
        }
        public async Task<IActionResult> DownloadPropertyPhoto(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregistertemp Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string path = Data.PropertyPhotoPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
    }
}

