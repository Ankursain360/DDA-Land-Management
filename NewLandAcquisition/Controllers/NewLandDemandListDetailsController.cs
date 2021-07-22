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
using NewLandAcquisition.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using Newtonsoft.Json;
using System.Text;

namespace NewLandAcquisition.Controllers
{
    public class NewLandDemandListDetailsController : BaseController
    {
        private readonly INewLandDemandListDetailsService _newLandDemandListDetailsService;
        public IConfiguration _Configuration;
        string ENMDocumentFilePath = "";

        public NewLandDemandListDetailsController(INewLandDemandListDetailsService newLandDemandListDetailsService, IConfiguration configuration)
        {
            _newLandDemandListDetailsService = newLandDemandListDetailsService;
            _Configuration = configuration;
            ENMDocumentFilePath = _Configuration.GetSection("FilePaths:NewLandDemandListDetails:DocumentFIlePath").Value.ToString();
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            ViewBag.VillageList = await _newLandDemandListDetailsService.GetVillageList();
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewLandDemandListDetailsSearchDto model)
        {
            var result = await _newLandDemandListDetailsService.GetPagedDMSFileUploadList(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Newlanddemandlistdetails newlanddemandlistdetails)
        {
            newlanddemandlistdetails.VillageList = await _newLandDemandListDetailsService.GetVillageList();
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlanddemandlistdetails newlanddemandlistdetails = new Newlanddemandlistdetails();
            newlanddemandlistdetails.IsActive = 1;
            await BindDropDown(newlanddemandlistdetails);
            return View(newlanddemandlistdetails);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create(Newlanddemandlistdetails newlanddemandlistdetails)
        {
            await BindDropDown(newlanddemandlistdetails);
            newlanddemandlistdetails.VillageList = await _newLandDemandListDetailsService.GetVillageList();

            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();
                newlanddemandlistdetails.ENMDocumentName = newlanddemandlistdetails.ENMDocumentIFormFile == null ? newlanddemandlistdetails.ENMDocumentName : fileHelper.SaveFile1(ENMDocumentFilePath, newlanddemandlistdetails.ENMDocumentIFormFile);
                newlanddemandlistdetails.CreatedBy = SiteContext.UserId;
                var result = await _newLandDemandListDetailsService.Create(newlanddemandlistdetails);

                if (result == true)
                {
                    //************ Save Appeal  ************  

                    if (
                        newlanddemandlistdetails.AppealNo != null

                        //demandlistdetails.AppealByDept != null &&
                        //demandlistdetails.Department != null &&
                        // demandlistdetails.DateOfAppeal != null &&
                        //demandlistdetails.PanelLawer != null

                        )
                    {
                        Newlandappealdetail newlandappealdetail = new Newlandappealdetail();

                        //item.PropertyType == null ? "" : item.PropertyType.Name
                        //appealdetail.AppealNo = demandlistdetails.AppealNo == null ? "0" : demandlistdetails.AppealNo;
                        newlandappealdetail.AppealNo = newlanddemandlistdetails.AppealNo;
                        newlandappealdetail.AppealByDept = newlanddemandlistdetails.AppealByDept;
                        newlandappealdetail.EnmSno = newlanddemandlistdetails.Enmsno.ToString();
                        newlandappealdetail.DemandListNo = newlanddemandlistdetails.DemandListNo.ToString();
                        newlandappealdetail.Department = newlanddemandlistdetails.Department;
                        newlandappealdetail.DateOfAppeal = newlanddemandlistdetails.DateOfAppeal;
                        newlandappealdetail.PanelLawer = newlanddemandlistdetails.PanelLawer;
                        newlandappealdetail.IsActive = 1;

                        newlandappealdetail.DemandListId = newlanddemandlistdetails.Id;
                        newlandappealdetail.CreatedBy = SiteContext.UserId;
                        result = await _newLandDemandListDetailsService.SaveAppeal(newlandappealdetail);
                    }
                    //************ Save Payment  ************  

                    if (
                        //demandlistdetails.AmountPaid != null &&
                        //demandlistdetails.ChequeDate != null &&
                        //demandlistdetails.ChequeNo != null &&
                        // demandlistdetails.BankName != null &&
                        newlanddemandlistdetails.VoucherNo != null
                        //  demandlistdetails.PercentPaid != null &&
                        //  //demandlistdetails.PaymentProofDocumentIFormFile != null &&
                        //demandlistdetails.PercentPaid != null

                        )
                    {
                        Newlandpaymentdetail newlandpaymentdetail = new Newlandpaymentdetail();
                        //  paymentdetail.PaymentProofDocumentName = paymentdetail.PaymentProofDocumentIFormFile == null ?
                        //paymentdetail.PaymentProofDocumentName : fileHelper.SaveFile1(PaymentProofDocumentFilePath,
                        //paymentdetail.PaymentProofDocumentIFormFile);

                        newlandpaymentdetail.AmountPaid = (decimal)newlanddemandlistdetails.AmountPaid;
                        newlandpaymentdetail.EnmSno = newlanddemandlistdetails.Enmsno.ToString();
                        newlandpaymentdetail.DemandListNo = newlanddemandlistdetails.DemandListNo.ToString();
                        newlandpaymentdetail.ChequeDate = newlanddemandlistdetails.ChequeDate;
                        newlandpaymentdetail.ChequeNo = newlanddemandlistdetails.ChequeNo;
                        newlandpaymentdetail.BankName = newlanddemandlistdetails.BankName;
                        newlandpaymentdetail.VoucherNo = newlanddemandlistdetails.VoucherNo;
                        newlandpaymentdetail.PercentPaid = (decimal)newlanddemandlistdetails.PercentPaid;
                        //paymentdetail.PaymentProofDocumentIFormFile = demandlistdetails.PaymentProofDocumentIFormFile;

                        newlandpaymentdetail.DemandListId = newlanddemandlistdetails.Id;
                        newlandpaymentdetail.CreatedBy = SiteContext.UserId;
                        result = await _newLandDemandListDetailsService.SavePayment(newlandpaymentdetail);
                    }
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    ViewBag.VillageList = await _newLandDemandListDetailsService.GetVillageList();
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    await BindDropDown(newlanddemandlistdetails);
                    return View(newlanddemandlistdetails);

                }
            }
            else
            {
                return View(newlanddemandlistdetails);
            }

        }
        public async Task<JsonResult> GetAppeal(int? Id)
        {
            Id = Id ?? 0;
            var data = await _newLandDemandListDetailsService.FetchSingleAppeal(Convert.ToInt32(Id));

            return Json(data);

        }
        public async Task<JsonResult> GetPayment(int? Id)
        {
            Id = Id ?? 0;
            var data = await _newLandDemandListDetailsService.FetchSinglePayment(Convert.ToInt32(Id));

            return Json(data);

        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newLandDemandListDetailsService.FetchSingleResult(id);
            await BindDropDown(Data);
            Data.KhasraNoList = await _newLandDemandListDetailsService.GetKhasraList(Data.VillageId);
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
        public async Task<IActionResult> Edit(int id, Newlanddemandlistdetails newlanddemandlistdetails)
        {
            await BindDropDown(newlanddemandlistdetails);
            newlanddemandlistdetails.VillageList = await _newLandDemandListDetailsService.GetVillageList();

            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();
                newlanddemandlistdetails.ENMDocumentName = newlanddemandlistdetails.ENMDocumentIFormFile == null ? newlanddemandlistdetails.ENMDocumentName : fileHelper.SaveFile1(ENMDocumentFilePath, newlanddemandlistdetails.ENMDocumentIFormFile);
                newlanddemandlistdetails.ModifiedBy = SiteContext.UserId;
                var result = await _newLandDemandListDetailsService.Update(id, newlanddemandlistdetails);

                if (result == true)
                {
                    //************ Save Appeal  ************  
                    var data = await _newLandDemandListDetailsService.FetchSingleAppeal(id);

                    if (data != null)

                    {


                        Newlandappealdetail newlandappealdetail = new Newlandappealdetail();


                        newlandappealdetail.AppealNo = newlanddemandlistdetails.AppealNo;
                        newlandappealdetail.AppealByDept = newlanddemandlistdetails.AppealByDept;
                        newlandappealdetail.DemandListNo = newlanddemandlistdetails.DemandListNo.ToString();
                        newlandappealdetail.Department = newlanddemandlistdetails.Department;
                        newlandappealdetail.Department = newlanddemandlistdetails.Department;
                        newlandappealdetail.DateOfAppeal = newlanddemandlistdetails.DateOfAppeal;
                        newlandappealdetail.PanelLawer = newlanddemandlistdetails.PanelLawer;

                        newlandappealdetail.DemandListId = newlanddemandlistdetails.Id;
                        newlandappealdetail.CreatedBy = SiteContext.UserId;
                        newlandappealdetail.IsActive = 1;
                        result = await _newLandDemandListDetailsService.UpdateAppeal(id, newlandappealdetail);
                    }
                    else
                    {

                        Newlandappealdetail newlandappealdetail = new Newlandappealdetail();

                        newlandappealdetail.AppealNo = newlanddemandlistdetails.AppealNo;
                        newlandappealdetail.AppealByDept = newlanddemandlistdetails.AppealByDept;
                        newlandappealdetail.EnmSno = newlanddemandlistdetails.Enmsno.ToString();
                        newlandappealdetail.DemandListNo = newlanddemandlistdetails.DemandListNo.ToString();
                        newlandappealdetail.Department = newlanddemandlistdetails.Department;
                        newlandappealdetail.DateOfAppeal = newlanddemandlistdetails.DateOfAppeal;
                        newlandappealdetail.PanelLawer = newlanddemandlistdetails.PanelLawer;

                        newlandappealdetail.DemandListId = newlanddemandlistdetails.Id;
                        newlandappealdetail.CreatedBy = SiteContext.UserId;
                        newlandappealdetail.IsActive = 1;
                        result = await _newLandDemandListDetailsService.SaveAppeal(newlandappealdetail);

                    }
                    //************ Save Payment  ************  
                    var dataa = await _newLandDemandListDetailsService.FetchSinglePayment(id);

                    if (dataa != null)

                    {

                        Newlandpaymentdetail newlandpaymentdetail = new Newlandpaymentdetail();
                        // paymentdetail.PaymentProofDocumentName = paymentdetail.PaymentProofDocumentIFormFile == null ?
                        //paymentdetail.PaymentProofDocumentName : fileHelper.SaveFile1(PaymentProofDocumentFilePath,
                        //paymentdetail.PaymentProofDocumentIFormFile);

                        newlandpaymentdetail.AmountPaid = (decimal)newlanddemandlistdetails.AmountPaid;
                        newlandpaymentdetail.ChequeDate = newlanddemandlistdetails.ChequeDate;
                        newlandpaymentdetail.ChequeNo = newlanddemandlistdetails.ChequeNo;
                        newlandpaymentdetail.BankName = newlanddemandlistdetails.BankName;
                        newlandpaymentdetail.VoucherNo = newlanddemandlistdetails.VoucherNo;
                        newlandpaymentdetail.PercentPaid = (decimal)newlanddemandlistdetails.PercentPaid;
                        //paymentdetail.PaymentProofDocumentName = demandlistdetails.PaymentProofDocumentName;
                        newlandpaymentdetail.EnmSno = newlanddemandlistdetails.Enmsno.ToString();
                        newlandpaymentdetail.DemandListNo = newlanddemandlistdetails.DemandListNo.ToString();
                        newlandpaymentdetail.DemandListId = newlanddemandlistdetails.Id;
                        newlandpaymentdetail.IsActive = 1;
                        result = await _newLandDemandListDetailsService.UpdatePayment(id, newlandpaymentdetail);
                    }
                    else
                    {
                        Newlandpaymentdetail newlandpaymentdetail = new Newlandpaymentdetail();
                        newlandpaymentdetail.AmountPaid = (decimal)newlanddemandlistdetails.AmountPaid;
                        newlandpaymentdetail.ChequeDate = newlanddemandlistdetails.ChequeDate;
                        newlandpaymentdetail.ChequeNo = newlanddemandlistdetails.ChequeNo;
                        newlandpaymentdetail.BankName = newlanddemandlistdetails.BankName;
                        newlandpaymentdetail.VoucherNo = newlanddemandlistdetails.VoucherNo;
                        newlandpaymentdetail.PercentPaid = (decimal)newlanddemandlistdetails.PercentPaid;
                        //paymentdetail.PaymentProofDocumentIFormFile = demandlistdetails.PaymentProofDocumentIFormFile;
                        newlandpaymentdetail.EnmSno = newlanddemandlistdetails.Enmsno.ToString();
                        newlandpaymentdetail.DemandListNo = newlanddemandlistdetails.DemandListNo.ToString();
                        newlandpaymentdetail.DemandListId = newlanddemandlistdetails.Id;
                        newlandpaymentdetail.CreatedBy = SiteContext.UserId;
                        newlandpaymentdetail.IsActive = 1;
                        result = await _newLandDemandListDetailsService.SavePayment(newlandpaymentdetail);

                    }

                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    ViewBag.VillageList = await _newLandDemandListDetailsService.GetVillageList();
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    await BindDropDown(newlanddemandlistdetails);
                    return View(newlanddemandlistdetails);

                }
            }
            else
            {
                return View(newlanddemandlistdetails);
            }
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newLandDemandListDetailsService.FetchSingleResult(id);
            await BindDropDown(Data);
            Data.KhasraNoList = await _newLandDemandListDetailsService.GetKhasraList(Data.VillageId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _newLandDemandListDetailsService.Delete(id, SiteContext.UserId);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            ViewBag.VillageList = await _newLandDemandListDetailsService.GetVillageList();
            return View("Index");
        }
        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? Id)
        {
            Id = Id ?? 0;
            return Json(await _newLandDemandListDetailsService.GetKhasraList(Convert.ToInt32(Id)));
        }


        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> DemanddetailsList()
        {
            var result = await _newLandDemandListDetailsService.GetAllDemandlistdetails();
            List<NewLandDemanddetailsListDto> data = new List<NewLandDemanddetailsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NewLandDemanddetailsListDto()
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
            Newlanddemandlistdetails Data = await _newLandDemandListDetailsService.FetchSingleResult(Id);
            string filename = ENMDocumentFilePath + Data.ENMDocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }
    }

}

