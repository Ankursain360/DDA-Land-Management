using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Dto.Search;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using Newtonsoft.Json;
using System.Text;



namespace AcquiredLandInformationManagement.Controllers
{
    public class DemandListDetailsController : BaseController
    {
        private readonly IDemandListDetailsService _demandListDetailsService;
        private readonly IPaymentdetailService _paymentdetailService;
        public IConfiguration _Configuration;
        string ENMDocumentFilePath = "";
        string PaymentProofDocumentFilePath = "";
        public object JsonRequestBehavior { get; private set; }

        public DemandListDetailsController(IDemandListDetailsService demandListDetailsService, IConfiguration configuration)
        {
            _demandListDetailsService = demandListDetailsService;
            _Configuration = configuration;
            ENMDocumentFilePath = _Configuration.GetSection("FilePaths:DemandListDetails:DocumentFIlePath").Value.ToString();
            PaymentProofDocumentFilePath = _Configuration.GetSection("FilePaths:PaymentDetail:PaymentProofDocumentFIlePath").Value.ToString();
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            ViewBag.VillageList = await _demandListDetailsService.GetVillageList();
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemandListDetailsSearchDto model)
        {
            var result = await _demandListDetailsService.GetPagedDMSFileUploadList(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Demandlistdetails demandlistdetails)
        {
            demandlistdetails.VillageList = await _demandListDetailsService.GetVillageList();
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Demandlistdetails demandlistdetails = new Demandlistdetails();
            demandlistdetails.IsActive = 1;
            await BindDropDown(demandlistdetails);
            return View(demandlistdetails);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create(Demandlistdetails demandlistdetails)
        {
            bool IsValidpdf = CheckMimeType(demandlistdetails);
            bool IsValidpdf1 = CheckMimeType1(demandlistdetails);
            await BindDropDown(demandlistdetails);
            demandlistdetails.VillageList = await _demandListDetailsService.GetVillageList();
            demandlistdetails.KhasraNoList = await _demandListDetailsService.GetKhasraList(demandlistdetails.VillageId);
          
            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {
                    if (IsValidpdf1 == true)
                    {
                        FileHelper fileHelper = new FileHelper();
                        demandlistdetails.ENMDocumentName = demandlistdetails.ENMDocumentIFormFile == null ? demandlistdetails.ENMDocumentName : fileHelper.SaveFile1(ENMDocumentFilePath, demandlistdetails.ENMDocumentIFormFile);

                        demandlistdetails.CreatedBy = SiteContext.UserId;
                        var result = await _demandListDetailsService.Create(demandlistdetails);

                        if (result == true)
                        {
                            //************ Save Appeal  ************  

                            if (
                                demandlistdetails.AppealNo != null



                                )
                            {
                                Appealdetail appealdetail = new Appealdetail();


                                appealdetail.AppealNo = demandlistdetails.AppealNo == null ? "" : demandlistdetails.AppealNo;
                                appealdetail.AppealByDept = demandlistdetails.AppealByDept == null ? "" : demandlistdetails.AppealByDept;
                                appealdetail.EnmSno = demandlistdetails.Enmsno.ToString();
                                appealdetail.DemandListNo = demandlistdetails.DemandListNo.ToString();
                                appealdetail.Department = demandlistdetails.Department == null ? "" : demandlistdetails.Department;
                                appealdetail.DateOfAppeal = demandlistdetails.DateOfAppeal;
                                appealdetail.PanelLawer = demandlistdetails.PanelLawer;
                                appealdetail.IsActive = 1;

                                appealdetail.DemandListId = demandlistdetails.Id;
                                appealdetail.CreatedBy = SiteContext.UserId;
                                result = await _demandListDetailsService.SaveAppeal(appealdetail);
                            }
                            //************ Save Payment  ************  

                            if (

                                demandlistdetails.VoucherNo != null


                                )
                            {
                                Paymentdetail paymentdetail = new Paymentdetail();
                                paymentdetail.PaymentProofDocumentName = demandlistdetails.PaymentProofDocumentIFormFile == null ?
                              demandlistdetails.PaymentProofDocumentName : fileHelper.SaveFile1(PaymentProofDocumentFilePath,
                              demandlistdetails.PaymentProofDocumentIFormFile);

                                paymentdetail.AmountPaid = (decimal)demandlistdetails.AmountPaid;
                                paymentdetail.EnmSno = demandlistdetails.Enmsno.ToString();
                                paymentdetail.DemandListNo = demandlistdetails.DemandListNo.ToString();
                                paymentdetail.ChequeDate = demandlistdetails.ChequeDate;
                                paymentdetail.ChequeNo = demandlistdetails.ChequeNo;
                                paymentdetail.BankName = demandlistdetails.BankName;
                                paymentdetail.VoucherNo = demandlistdetails.VoucherNo;
                                paymentdetail.PercentPaid = (decimal)demandlistdetails.PercentPaid;
                                paymentdetail.PaymentProofDocumentIFormFile = demandlistdetails.PaymentProofDocumentIFormFile;

                                paymentdetail.DemandListId = demandlistdetails.Id;
                                paymentdetail.CreatedBy = SiteContext.UserId;
                                result = await _demandListDetailsService.SavePayment(paymentdetail);
                            }
                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            ViewBag.VillageList = await _demandListDetailsService.GetVillageList();
                            return View("Index");

                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            await BindDropDown(demandlistdetails);
                            return View(demandlistdetails);

                        }
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                        return View(demandlistdetails);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(demandlistdetails);
                }
            }
            else
            {
                return View(demandlistdetails);
            }

        }
        public async Task<JsonResult> GetAppeal(int? Id)
        {
            Id = Id ?? 0;
            var data = await _demandListDetailsService.FetchSingleAppeal(Convert.ToInt32(Id));

            return Json(data);

        }
        public async Task<JsonResult> GetPayment(int? Id)
        {
            Id = Id ?? 0;
            var data = await _demandListDetailsService.FetchSinglePayment(Convert.ToInt32(Id));

            return Json(data);

        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _demandListDetailsService.FetchSingleResult(id);
            await BindDropDown(Data);

            Data.KhasraNoList = await _demandListDetailsService.GetKhasraList(Data.VillageId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Edit(int id, Demandlistdetails demandlistdetails)
        {
            bool IsValidpdf = CheckMimeType(demandlistdetails);
            bool IsValidpdf1 = CheckMimeType1(demandlistdetails);
            await BindDropDown(demandlistdetails);
            demandlistdetails.VillageList = await _demandListDetailsService.GetVillageList();
            demandlistdetails.KhasraNoList = await _demandListDetailsService.GetKhasraList(demandlistdetails.VillageId);
            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {
                    if (IsValidpdf1 == true)
                    {
                        FileHelper fileHelper = new FileHelper();
                        demandlistdetails.ENMDocumentName = demandlistdetails.ENMDocumentIFormFile == null ? demandlistdetails.ENMDocumentName : fileHelper.SaveFile1(ENMDocumentFilePath, demandlistdetails.ENMDocumentIFormFile);
                        demandlistdetails.ModifiedBy = SiteContext.UserId;
                        var result = await _demandListDetailsService.Update(id, demandlistdetails);

                        if (result == true)
                        {
                            //************ Save Appeal  ************  
                            var data = await _demandListDetailsService.FetchSingleAppeal(id);

                            if (data != null)

                            {


                                Appealdetail appealdetail = new Appealdetail();


                                appealdetail.AppealNo = demandlistdetails.AppealNo;
                                appealdetail.AppealByDept = demandlistdetails.AppealByDept;
                                appealdetail.DemandListNo = demandlistdetails.DemandListNo.ToString();
                                appealdetail.EnmSno = demandlistdetails.Enmsno.ToString();
                                appealdetail.Department = demandlistdetails.Department;
                                appealdetail.Department = demandlistdetails.Department;
                                appealdetail.DateOfAppeal = demandlistdetails.DateOfAppeal;
                                appealdetail.PanelLawer = demandlistdetails.PanelLawer;

                                appealdetail.DemandListId = demandlistdetails.Id;
                                appealdetail.CreatedBy = SiteContext.UserId;
                                appealdetail.IsActive = 1;
                                result = await _demandListDetailsService.UpdateAppeal(id, appealdetail);
                            }
                            else
                            {

                                Appealdetail appealdetail = new Appealdetail();

                                appealdetail.AppealNo = demandlistdetails.AppealNo;
                                appealdetail.AppealByDept = demandlistdetails.AppealByDept;
                                appealdetail.EnmSno = demandlistdetails.Enmsno.ToString();
                                appealdetail.DemandListNo = demandlistdetails.DemandListNo.ToString();
                                appealdetail.Department = demandlistdetails.Department;
                                appealdetail.DateOfAppeal = demandlistdetails.DateOfAppeal;
                                appealdetail.PanelLawer = demandlistdetails.PanelLawer;

                                appealdetail.DemandListId = demandlistdetails.Id;
                                appealdetail.CreatedBy = SiteContext.UserId;
                                appealdetail.IsActive = 1;
                                result = await _demandListDetailsService.SaveAppeal(appealdetail);

                            }
                            //************ Save Payment  ************  
                            var dataa = await _demandListDetailsService.FetchSinglePayment(id);

                            if (dataa != null)

                            {

                                Paymentdetail paymentdetail = new Paymentdetail();
                                paymentdetail.PaymentProofDocumentName = demandlistdetails.PaymentProofDocumentIFormFile == null ?
                               demandlistdetails.PaymentProofDocumentName : fileHelper.SaveFile1(PaymentProofDocumentFilePath,
                               demandlistdetails.PaymentProofDocumentIFormFile);


                                paymentdetail.AmountPaid = (decimal)demandlistdetails.AmountPaid;
                                paymentdetail.ChequeDate = demandlistdetails.ChequeDate;
                                paymentdetail.ChequeNo = demandlistdetails.ChequeNo;
                                paymentdetail.BankName = demandlistdetails.BankName;
                                paymentdetail.VoucherNo = demandlistdetails.VoucherNo;
                                paymentdetail.PercentPaid = (decimal)demandlistdetails.PercentPaid;
                                paymentdetail.PaymentProofDocumentIFormFile = demandlistdetails.PaymentProofDocumentIFormFile;
                                paymentdetail.EnmSno = demandlistdetails.Enmsno.ToString();
                                paymentdetail.DemandListNo = demandlistdetails.DemandListNo.ToString();
                                paymentdetail.DemandListId = demandlistdetails.Id;
                                paymentdetail.IsActive = 1;
                                result = await _demandListDetailsService.UpdatePayment(id, paymentdetail);
                            }
                            else
                            {
                                Paymentdetail paymentdetail = new Paymentdetail();
                                paymentdetail.AmountPaid = (decimal)demandlistdetails.AmountPaid;
                                paymentdetail.ChequeDate = demandlistdetails.ChequeDate;
                                paymentdetail.ChequeNo = demandlistdetails.ChequeNo;
                                paymentdetail.BankName = demandlistdetails.BankName;
                                paymentdetail.VoucherNo = demandlistdetails.VoucherNo;
                                paymentdetail.PercentPaid = (decimal)demandlistdetails.PercentPaid;
                                paymentdetail.PaymentProofDocumentIFormFile = demandlistdetails.PaymentProofDocumentIFormFile;
                                paymentdetail.EnmSno = demandlistdetails.Enmsno.ToString();
                                paymentdetail.DemandListNo = demandlistdetails.DemandListNo.ToString();
                                paymentdetail.DemandListId = demandlistdetails.Id;
                                paymentdetail.CreatedBy = SiteContext.UserId;
                                paymentdetail.IsActive = 1;
                                result = await _demandListDetailsService.SavePayment(paymentdetail);

                            }
                            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                            ViewBag.VillageList = await _demandListDetailsService.GetVillageList();
                            return View("Index");
                            //ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                            //var list = await _demandListDetailsService.GetAllDemandlistdetails();
                            //return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            await BindDropDown(demandlistdetails);
                            return View(demandlistdetails);

                        }
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                        return View(demandlistdetails);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(demandlistdetails);
                }
            }
            else
            {
                return View(demandlistdetails);
            }
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _demandListDetailsService.FetchSingleResult(id);
            await BindDropDown(Data);
            Data.KhasraNoList = await _demandListDetailsService.GetKhasraList(Data.VillageId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _demandListDetailsService.Delete(id, SiteContext.UserId);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            ViewBag.VillageList = await _demandListDetailsService.GetVillageList();
            return View("Index");
        }
        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? Id)
        {
            Id = Id ?? 0;
            return Json(await _demandListDetailsService.GetKhasraList(Convert.ToInt32(Id)));
        }


        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> DemanddetailsList()
        {
            var result = await _demandListDetailsService.GetAllDemandlistdetails();
            List<DemanddetailsListDto> data = new List<DemanddetailsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DemanddetailsListDto()
                    {
          
                        Id = result[i].Id,
                        DemandListNo = result[i].DemandListNo,
                        Village = result[i].Village == null ? "" : result[i].Village.Name,
                        KhasraNo = result[i].KhasraNo == null ? "" : result[i].KhasraNo.Name,
                        ENMSrNo = result[i].Enmsno.ToString(),
                        TotalAmount = result[i].TotalAmount.ToString(),
                       
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }

        public async Task<IActionResult> ViewENMDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Demandlistdetails Data = await _demandListDetailsService.FetchSingleResult(Id);
            string filename = ENMDocumentFilePath + Data.ENMDocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }
        public async Task<IActionResult> ViewPaymentProofDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Paymentdetail Data = await _demandListDetailsService.GetPaymentProofDocument(Id);
            string filename = PaymentProofDocumentFilePath + Data.PaymentProofDocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }




        [HttpPost]
        public JsonResult CheckFile()
        {
            bool IsImg = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            ENMDocumentFilePath = _Configuration.GetSection("FilePaths:DemandListDetails:DocumentFIlePath").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                ENMDocumentFilePath = _Configuration.GetSection("FilePaths:DemandListDetails:DocumentFIlePath").Value.ToString();
                string FilePath = Path.Combine(ENMDocumentFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(ENMDocumentFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(ENMDocumentFilePath);// Try to create the directory.
                    }
                    try
                    {
                        if (extension.ToLower() == ".pdf")
                        {
                            try
                            {
                                using (var stream = new FileStream(FilePath, FileMode.Create))
                                {
                                    files.CopyTo(stream);

                                }

                                iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(FilePath);
                                oPdfReader.Close();
                                fullpath = _Configuration.GetSection("FilePaths:DemandListDetails:DocumentFIlePath").Value.ToString();
                                FileInfo doc = new FileInfo(fullpath);
                                if (doc.Exists)
                                {
                                    doc.Delete();
                                }
                            }
                            catch (iTextSharp.text.exceptions.InvalidPdfException)
                            {
                                IsImg = false;
                            }

                        }
                        else
                        {
                            IsImg = false;
                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        IsImg = false;

                        if (System.IO.File.Exists(fullpath))
                        {
                            try
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            catch (Exception exs)
                            {
                            }
                        }
                        // Image.FromFile will throw this if file is invalid.  
                    }

                }
            }

            return Json(IsImg, JsonRequestBehavior);
        }



        public bool CheckMimeType(Demandlistdetails demandlistdetails)
        {
            bool Flag = true;
            string fullpath = string.Empty;          
            string extension = string.Empty;
            ENMDocumentFilePath = _Configuration.GetSection("FilePaths:DemandListDetails:DocumentFIlePath").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                ENMDocumentFilePath = _Configuration.GetSection("FilePaths:DemandListDetails:DocumentFIlePath").Value.ToString();
                string FilePath = Path.Combine(ENMDocumentFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(ENMDocumentFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(ENMDocumentFilePath);// Try to create the directory.
                    }
                    try
                    {
                        if (extension.ToLower() == ".pdf")
                        {
                            try
                            {
                                using (var stream = new FileStream(FilePath, FileMode.Create))
                                {
                                    files.CopyTo(stream);

                                }

                                iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(FilePath);
                                oPdfReader.Close();
                                fullpath = _Configuration.GetSection("FilePaths:DemandListDetails:DocumentFIlePath").Value.ToString();
                                FileInfo doc = new FileInfo(fullpath);
                                if (doc.Exists)
                                {
                                    doc.Delete();
                                }
                            }
                            catch (iTextSharp.text.exceptions.InvalidPdfException)
                            {
                                Flag = false;
                            }

                        }
                        else
                        {
                            Flag = false;
                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        Flag = false;

                        if (System.IO.File.Exists(fullpath))
                        {
                            try
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            catch (Exception exs)
                            {
                            }
                        }
                        // Image.FromFile will throw this if file is invalid.  
                    }

                }
            }

            return Flag;
        }

        public bool CheckMimeType1(Demandlistdetails demandlistdetails)
        {
            bool Flag = true;
            string fullpath = string.Empty;
           
            string extension = string.Empty;
            ENMDocumentFilePath = _Configuration.GetSection("FilePaths:DemandListDetails:PaymentProofDocumentFIlePath").Value.ToString();
            IFormFile files = demandlistdetails.PaymentProofDocumentIFormFile;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                ENMDocumentFilePath = _Configuration.GetSection("FilePaths:DemandListDetails:PaymentProofDocumentFIlePath").Value.ToString();
                string FilePath = Path.Combine(ENMDocumentFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(ENMDocumentFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(ENMDocumentFilePath);// Try to create the directory.
                    }
                    try
                    {
                        if (extension.ToLower() == ".pdf")
                        {
                            try
                            {
                                using (var stream = new FileStream(FilePath, FileMode.Create))
                                {
                                    files.CopyTo(stream);

                                }

                                iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(FilePath);
                                oPdfReader.Close();
                                fullpath = _Configuration.GetSection("FilePaths:DemandListDetails:PaymentProofDocumentFIlePath").Value.ToString();
                                FileInfo doc = new FileInfo(fullpath);
                                if (doc.Exists)
                                {
                                    doc.Delete();
                                }
                            }
                            catch (iTextSharp.text.exceptions.InvalidPdfException)
                            {
                                Flag = false;
                            }

                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        Flag = false;

                        if (System.IO.File.Exists(fullpath))
                        {
                            try
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            catch (Exception exs)
                            {
                            }
                        }
                        // Image.FromFile will throw this if file is invalid.  
                    }

                }
            }

            return Flag;
        }


    }

}
