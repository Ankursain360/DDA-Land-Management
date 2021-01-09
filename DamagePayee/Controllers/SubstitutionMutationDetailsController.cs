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
        //public async Task<IActionResult> Index(int id)
        //{
        //    Mutationdetails Model = new Mutationdetails();
        //    var Data = await _damagepayeeregisterService.FetchSingleResult(id);

        //    Data.PropLocalityList = await _damagepayeeregisterService.GetLocalityList();
        //    Data.PropDistrictList = await _damagepayeeregisterService.GetDistrictList();

        //    Data.PersonalInfoDamageList = await _damagepayeeregisterService.GetPersonalInfoTemp(id);
        //    Data.AlloteeTypeDamageList = await _damagepayeeregisterService.GetAllottetypeTemp(id);
        //    Model.DamagePayeeRegister = Data;

        //    return View(Model);
        //}

        //async Task BindDropDown(Damagepayeeregistertemp DamagePayeeRegister)
        //{
        //    Damagepayeeregistertemp damage = new Damagepayeeregistertemp();

        //    DamagePayeeRegister.LocalityList = await _mutationDetailsService.GetLocalityList();
        //    DamagePayeeRegister.DistrictList = await _mutationDetailsService.GetDistrictList();
        //}
        public async Task<IActionResult> Create(int id)
        {
            Mutationdetailstemp mutationdetailstemp = new Mutationdetailstemp();
            Mutationdetailstemp Data = await _mutationDetailsService.FetchMutationSingleResult(id);
            if(Data != null)
            {
                mutationdetailstemp = Data;
            }
            mutationdetailstemp.DamagePayeeRegister = await _mutationDetailsService.FetchDamageResult(id);
            ViewBag.Locality = await _mutationDetailsService.GetLocalityList();
            ViewBag.District = await _mutationDetailsService.GetDistrictList();
            ViewBag.DamagePayeeId = id;
            ViewBag.Id = (Data == null ? 0 : mutationdetailstemp.Id);
            return View(mutationdetailstemp);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, Mutationdetailstemp mutationDetails)
        {
            var Data = await _damagepayeeregisterService.FetchSingleResult(id);


            AtsfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:ATSFilePath").Value.ToString();
            GPAfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:GPAFilePath").Value.ToString();
            IndemnityfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:INDEMNITYFIRFilePath").Value.ToString();
            AffidevitfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:AFFIDEVITFIRFilePath").Value.ToString();
            AddprooffilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:ADDPROOFFIRFilePath").Value.ToString();
            SignspcfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:SIGNSPECFIRFilePath").Value.ToString();
            MoneyRecfilePath = _configuration.GetSection("FilePaths:MutationDetaliFiles:MONEYRECFilePath").Value.ToString();
            string deathCertificate = _configuration.GetSection("FilePaths:MutationDetaliFiles:DeathCertificate").Value.ToString();
            string relationshipProof = _configuration.GetSection("FilePaths:MutationDetaliFiles:RelationshipProof").Value.ToString();
            string affitDevitLegal = _configuration.GetSection("FilePaths:MutationDetaliFiles:AffitdevitLegal").Value.ToString();
            string noObjectionCertificate = _configuration.GetSection("FilePaths:MutationDetaliFiles:NoObjectionCertificate").Value.ToString();
            string signatureOfSpec = _configuration.GetSection("FilePaths:MutationDetaliFiles:SignatureOfSpecimen").Value.ToString();

            var result = false;
            if (true)
            {
                FileHelper fileHelper = new FileHelper();
                /* For Ats File Upload*/
                string ATSFileName = "";
                string atsfilePath = "";
                // mutationDetails.AtsfilePathNew = agreementfilePath;
                if (mutationDetails.AtsfilePathNew != null)
                {
                    if (!Directory.Exists(AtsfilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(AtsfilePath);// Try to create the directory.
                    }
                    ATSFileName = Guid.NewGuid().ToString() + "_" + mutationDetails.AtsfilePathNew.FileName;
                    atsfilePath = Path.Combine(AtsfilePath, ATSFileName);
                    using (var stream = new FileStream(atsfilePath, FileMode.Create))
                    {
                        mutationDetails.AtsfilePathNew.CopyTo(stream);
                    }
                    mutationDetails.AtsfilePath = ATSFileName;
                }

                /* For GPA File Upload*/
                string GPAFileName = "";
                string gpafilePath = "";
                if (mutationDetails.GpafilePathNew != null)
                {
                    if (!Directory.Exists(GPAfilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(GPAfilePath);// Try to create the directory.
                    }
                    GPAFileName = Guid.NewGuid().ToString() + "_" + mutationDetails.GpafilePathNew.FileName;
                    gpafilePath = Path.Combine(GPAfilePath, GPAFileName);
                    using (var stream = new FileStream(gpafilePath, FileMode.Create))
                    {
                        mutationDetails.GpafilePathNew.CopyTo(stream);
                    }
                    mutationDetails.GpafilePath = GPAFileName;
                }

                /* For MoneyReceipt File Upload*/
                string MoneyFileName = "";
                string MoneyfilePath = "";
                if (mutationDetails.MoneyfilePathNew != null)
                {
                    if (!Directory.Exists(MoneyRecfilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(MoneyRecfilePath); // Try to create the directory.
                    }
                    MoneyFileName = Guid.NewGuid().ToString() + "_" + mutationDetails.MoneyfilePathNew.FileName;
                    MoneyfilePath = Path.Combine(MoneyRecfilePath, MoneyFileName);
                    using (var stream = new FileStream(MoneyfilePath, FileMode.Create))
                    {
                        mutationDetails.MoneyfilePathNew.CopyTo(stream);
                    }
                    mutationDetails.MoneyRecieptFilePath = MoneyFileName;
                }

                /* For Signature Specimen File Upload*/
                string SignSpecFileName = "";
                string signSpecfilePath = "";
                if (mutationDetails.SignSpecPathNew != null)
                {
                    if (!Directory.Exists(SignspcfilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(SignspcfilePath);// Try to create the directory.
                    }
                    SignSpecFileName = Guid.NewGuid().ToString() + "_" + mutationDetails.SignSpecPathNew.FileName;
                    signSpecfilePath = Path.Combine(SignspcfilePath, SignSpecFileName);
                    using (var stream = new FileStream(signSpecfilePath, FileMode.Create))
                    {
                        mutationDetails.SignSpecPathNew.CopyTo(stream);
                    }
                    mutationDetails.SignatureSpecimenFilePath = SignSpecFileName;
                }

                /* For Address proof File Upload*/
                string AddProofFileName = "";
                string addProoffilePath = "";
                if (mutationDetails.AddressProofPathNew != null)
                {
                    if (!Directory.Exists(AddprooffilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(AddprooffilePath);// Try to create the directory.
                    }
                    AddProofFileName = Guid.NewGuid().ToString() + "_" + mutationDetails.AddressProofPathNew.FileName;
                    addProoffilePath = Path.Combine(AddprooffilePath, AddProofFileName);
                    using (var stream = new FileStream(addProoffilePath, FileMode.Create))
                    {
                        mutationDetails.AddressProofPathNew.CopyTo(stream);
                    }
                    mutationDetails.AddressProofFilePath = AddProofFileName;
                }

                /* For AffiDevit File Upload*/
                string AffidevitFileName = "";
                string affidevitfilePath = "";
                if (mutationDetails.AffidavitPathNew != null)
                {
                    if (!Directory.Exists(AffidevitfilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(AffidevitfilePath);// Try to create the directory.
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
                if (mutationDetails.IndemnityPathNew != null)
                {
                    if (!Directory.Exists(IndemnityfilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(IndemnityfilePath);// Try to create the directory.
                    }
                    IndemnitybondFileName = Guid.NewGuid().ToString() + "_" + mutationDetails.IndemnityPathNew.FileName;
                    IindemnityBondfilePath = Path.Combine(IndemnityfilePath, IndemnitybondFileName);
                    using (var stream = new FileStream(IindemnityBondfilePath, FileMode.Create))
                    {
                        mutationDetails.IndemnityPathNew.CopyTo(stream);
                    }
                    mutationDetails.IndemnityFilePath = IndemnitybondFileName;
                }
              
                if (mutationDetails.DeathCertificatePathNew != null)
                {
                    mutationDetails.DeathCertificatePath = fileHelper.SaveFile1(deathCertificate, mutationDetails.DeathCertificatePathNew);
                }
                if (mutationDetails.RelationshipUploadPathNew != null)
                {
                    mutationDetails.RelationshipUploadPath = fileHelper.SaveFile1(relationshipProof, mutationDetails.RelationshipUploadPathNew);
                }
                if (mutationDetails.AffidevitLegalUploadPathNew != null)
                {
                    mutationDetails.AffidevitLegalUploadPath = fileHelper.SaveFile1(affitDevitLegal, mutationDetails.AffidevitLegalUploadPathNew);
                }
                if (mutationDetails.NonObjectUploadPathNew != null)
                {
                    mutationDetails.NonObjectHeirUploadPath = fileHelper.SaveFile1(noObjectionCertificate, mutationDetails.NonObjectUploadPathNew);
                }
                if (mutationDetails.SpecimenSignLegalUploadNew != null)
                {
                    mutationDetails.SpecimenSignLegalUpload = fileHelper.SaveFile1(signatureOfSpec, mutationDetails.SpecimenSignLegalUploadNew);
                }
                if (mutationDetails.MutationPurpose == "Purchaser")
                {
                    if (mutationDetails.PurchaseDate == null || mutationDetails.AtsfilePath == null || mutationDetails.GpafilePath == null || mutationDetails.MoneyRecieptFilePath == null || mutationDetails.SignatureSpecimenFilePath == null)
                    {
                        ViewBag.Message = Alert.Show("Purchaser Section related Document is Mandatory", "", AlertType.Warning);
                        return View(mutationDetails);
                    }
                }
                else
                {
                    if (mutationDetails.DeathCertificatePath == null || mutationDetails.RelationshipUploadPath == null || mutationDetails.AffidevitLegalUploadPath == null || mutationDetails.NonObjectHeirUploadPath == null || mutationDetails.SpecimenSignLegalUpload == null)
                    {
                        ViewBag.Message = Alert.Show("Inheritance Section related Document is Mandatory", "", AlertType.Warning);
                        return View(mutationDetails);
                    }
                }
                if(mutationDetails.Id ==0)
                {
                    mutationDetails.CreatedBy = SiteContext.UserId;
                    result = await _mutationDetailsService.Create(mutationDetails);
                }
                else
                {
                    mutationDetails.ModifiedBy = SiteContext.UserId;
                    result = await _mutationDetailsService.Update(id,mutationDetails);
                }
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    return View("Index1");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(mutationDetails);
                }
            }
        }
        public async Task<IActionResult> ViewATSGPAFile(int Id)
        {
            FileHelper file = new FileHelper();
            Allottetype Data = await _mutationDetailsService.GetAlloteeTypeFile(Id);
            string filename = _configuration.GetSection("FilePaths:DamagePayeeFiles:ATSGPADocument").Value.ToString() + Data.AtsgpadocumentPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> ViewPersonelInfoAadharFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _mutationDetailsService.GetPersonelInfoFile(Id);
            string filename = Data.AadharNoFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> ViewPersonelInfoPanFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _mutationDetailsService.GetPersonelInfoFile(Id);
            string filename = Data.PanNoFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> ViewPersonelInfoPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _mutationDetailsService.GetPersonelInfoFile(Id);
            string filename = Data.PhotographPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> ViewPersonelInfoSignautreFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _mutationDetailsService.GetPersonelInfoFile(Id);
            string filename = Data.SignaturePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadAgreementFile(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetailstemp Data = await _mutationDetailsService.FetchSingleResultMutationId(Id);
            string filename = Data.AtsfilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadGPAFile(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetailstemp Data = await _mutationDetailsService.FetchSingleResultMutationId(Id);
            string filename = Data.GpafilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadMoneyReceiptFile(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetailstemp Data = await _mutationDetailsService.FetchSingleResultMutationId(Id);
            string filename = Data.MoneyRecieptFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadSignSpecFile(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetailstemp Data = await _mutationDetailsService.FetchSingleResultMutationId(Id);
            string filename = Data.SignatureSpecimenFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadAddressProofFile(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetailstemp Data = await _mutationDetailsService.FetchSingleResultMutationId(Id);
            string filename = Data.AddressProofFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadAffitDevitFile(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetailstemp Data = await _mutationDetailsService.FetchSingleResultMutationId(Id);
            string filename = Data.AffidavitFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadIndemnityFile(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetailstemp Data = await _mutationDetailsService.FetchSingleResultMutationId(Id);
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
            Damagepayeeregister Data = await _mutationDetailsService.FetchDamageResult(Id);
            string path = Data.PropertyPhotoPath;
            return File(file.GetMemory(path), file.GetContentType(path), Path.GetFileName(path));
        }
        public async Task<IActionResult> ViewAtsFileMutation(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetailstemp Data = await _mutationDetailsService.FetchSingleResultMutationId(Id);
            string path = AtsfilePath + Data.AtsfilePath;
            return File(file.GetMemory(path), file.GetContentType(path), Path.GetFileName(path));
        }
        public async Task<IActionResult> ViewDeathCertificate(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetailstemp Data = await _mutationDetailsService.FetchSingleResultMutationId(Id);
            string path = Data.DeathCertificatePath;
            return File(file.GetMemory(path), file.GetContentType(path), Path.GetFileName(path));
        }
        public async Task<IActionResult> ViewRelationshipUpload(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetailstemp Data = await _mutationDetailsService.FetchSingleResultMutationId(Id);
            string path = Data.RelationshipUploadPath;
            return File(file.GetMemory(path), file.GetContentType(path), Path.GetFileName(path));
        }
        public async Task<IActionResult> ViewAffidevitLegalUpload(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetailstemp Data = await _mutationDetailsService.FetchSingleResultMutationId(Id);
            string path = Data.AffidevitLegalUploadPath;
            return File(file.GetMemory(path), file.GetContentType(path), Path.GetFileName(path));
        }
        public async Task<IActionResult> ViewNonObjectHeirUpload(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetailstemp Data = await _mutationDetailsService.FetchSingleResultMutationId(Id);
            string path = Data.NonObjectHeirUploadPath;
            return File(file.GetMemory(path), file.GetContentType(path), Path.GetFileName(path));
        }
        public async Task<IActionResult> ViewSpecimenSignLegalUpload(int Id)
        {
            FileHelper file = new FileHelper();
            Mutationdetailstemp Data = await _mutationDetailsService.FetchSingleResultMutationId(Id);
            string path = Data.SpecimenSignLegalUpload;
            return File(file.GetMemory(path), file.GetContentType(path), Path.GetFileName(path));
        }
    }
}

