using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using AcquiredLandInformationManagement.Helper;

namespace AcquiredLandInformationManagement.Controllers
{
    public class EncroachmentDetailsController : BaseController
    {
        private readonly IEnchroachmentService _enchroachmentService;

        public EncroachmentDetailsController(IEnchroachmentService enchroachmentService)
        {
            _enchroachmentService = enchroachmentService;
        }
        public async Task<IActionResult> Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] EnchroachmentSearchDto model)
        {
            var result = await _enchroachmentService.GetPagedEnchroachment(model);

            return PartialView("_List", result);
        }

        public async Task<IActionResult> Create()
        {
            Enchroachment enchroachment = new Enchroachment();
            enchroachment.IsActive = 1;

          //  enchroachment.KhasraList = await _enchroachmentService.BindKhasra();
            enchroachment.VillageList = await _enchroachmentService.GetAllVillage();
            enchroachment.KhasraList = await _enchroachmentService.BindKhasra(enchroachment.VillageId);
            enchroachment.NencroachmentList = await _enchroachmentService.GetAllNencroachment();
            enchroachment.ReasonsList = await _enchroachmentService.GetAllReasons();

            return View(enchroachment);
        }
        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? VillageId)
        {
            VillageId = VillageId ?? 0;
            return Json(await _enchroachmentService.BindKhasra(Convert.ToInt32(VillageId)));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Enchroachment enchroachment)
        {
            try
            {

                enchroachment.KhasraList = await _enchroachmentService.BindKhasra(enchroachment.VillageId);
                enchroachment.VillageList = await _enchroachmentService.GetAllVillage();
                enchroachment.NencroachmentList = await _enchroachmentService.GetAllNencroachment();
                enchroachment.ReasonsList = await _enchroachmentService.GetAllReasons();

                if (ModelState.IsValid)
                {
                    enchroachment.CreatedBy = SiteContext.UserId;
                    var result = await _enchroachmentService.Create(enchroachment);

                    if (result == true)
                    {
                        // ------------ save encroacher name details------------- //
                        if (enchroachment.EName != null &&
                        enchroachment.EAddress != null )

                        {
                            if (enchroachment.EName.Count > 0  &&
                                enchroachment.EAddress.Count > 0   )

                            {
                                List<EncrocherPeople> owner = new List<EncrocherPeople>();
                                for (int i = 0; i < enchroachment.EName.Count; i++)
                                {
                                    owner.Add(new EncrocherPeople
                                    {
                                        NAME = enchroachment.EName.Count <= i ? string.Empty : enchroachment.EName[i],
                                      //  FatherName = enchroachment.FatherName.Count <= i ? string.Empty : jarai.FatherName[i],
                                       ADDRESS = enchroachment.EAddress.Count <= i ? string.Empty :enchroachment.EAddress[i],
                                        EnchId = enchroachment.Id,
                                        CreatedBy = SiteContext.UserId
                                    });
                                }
                                foreach (var item in owner)
                                {
                                    result = await _enchroachmentService.SaveEName(item);
                                }
                            }
                        }
                        //------------------end encroacher naem details here -----------------------//
                       
                        // ------------ save Payment details------------- //
                        if (enchroachment.Amount != null && enchroachment.ChequeNo != null &&
                        enchroachment.ChequeDate != null)

                        {
                            if (enchroachment.Amount.Count > 0 &&
                                enchroachment.ChequeDate.Count > 0 && enchroachment.ChequeNo.Count > 0)

                            {
                                List<Enchroachmentpayment> owner1 = new List<Enchroachmentpayment>();
                                for (int i = 0; i < enchroachment.Amount.Count; i++)
                                {
                                    owner1.Add(new Enchroachmentpayment
                                    {
                                        Amount = enchroachment.Amount.Count <= i ? string.Empty : enchroachment.Amount[i],
                                        //  FatherName = enchroachment.FatherName.Count <= i ? string.Empty : jarai.FatherName[i],
                                        ChequeDate = enchroachment.ChequeDate.Count <= i ? string.Empty : enchroachment.ChequeDate[i],
                                       ChequeNo=enchroachment.ChequeNo.Count<=i? string.Empty : enchroachment.ChequeNo[i],
                                        EnchId = enchroachment.Id,
                                        CreatedBy = SiteContext.UserId
                                    });
                                }
                                foreach (var item in owner1)
                                {
                                    result = await _enchroachmentService.SavePayment(item);
                                }
                            }
                        }
                        //------------------end payment details here -----------------------//


                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _enchroachmentService.GetAllEnchroachment();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(enchroachment);
                    }
                }
                else
                {
                    return View(enchroachment);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(enchroachment);
            }
        }




        public async Task<IActionResult> Edit(int id)
        {
            
            var Data = await _enchroachmentService.FetchSingleResult(id);

            Data.NencroachmentList = await _enchroachmentService.GetAllNencroachment();
            Data.ReasonsList = await _enchroachmentService.GetAllReasons();
            Data.KhasraList = await _enchroachmentService.BindKhasra(Data.VillageId);
           // Data.KhasraList = await _enchroachmentService.BindKhasra();
            Data.VillageList = await _enchroachmentService.GetAllVillage();

            Enchroachment enchroachment = new Enchroachment();
            enchroachment.IsActive = enchroachment.IsActive;

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Enchroachment enchroachment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    enchroachment.NencroachmentList = await _enchroachmentService.GetAllNencroachment();
                    enchroachment.ReasonsList = await _enchroachmentService.GetAllReasons();
                    enchroachment.KhasraList = await _enchroachmentService.BindKhasra(enchroachment.VillageId);
                    enchroachment.VillageList = await _enchroachmentService.GetAllVillage();
                    var result = await _enchroachmentService.Update(id, enchroachment);
                    if (result == true)
                    {
                        // ------------ save encroacher name details------------- //
                        if (enchroachment.EName != null &&
                        enchroachment.EAddress != null)

                        {
                            int k = enchroachment.EName.Count-1;
                            int j = k- 1;
                            if (enchroachment.EName[k] != enchroachment.EName[j])
                            {
                                if (enchroachment.EName.Count > 0 &&
                                enchroachment.EAddress.Count > 0)

                                {
                                    int epr = (enchroachment.EName.Count) - 1;
                                    List<EncrocherPeople> owner = new List<EncrocherPeople>();
                                    for (int i = epr; i < enchroachment.EName.Count; i++)
                                    {

                                        owner.Add(new EncrocherPeople
                                        {
                                            NAME = enchroachment.EName.Count <= i ? string.Empty : enchroachment.EName[i],
                                            //  FatherName = enchroachment.FatherName.Count <= i ? string.Empty : jarai.FatherName[i],
                                            ADDRESS = enchroachment.EAddress.Count <= i ? string.Empty : enchroachment.EAddress[i],
                                            EnchId = enchroachment.Id,
                                            CreatedBy = SiteContext.UserId
                                        });

                                    }
                                    foreach (var item in owner)
                                    {
                                        result = await _enchroachmentService.SaveEName(item);
                                    }
                                }
                            }
                        }
                        //------------------end encroacher naem details here -----------------------//

                        // ------------ save Payment details------------- //
                        if (enchroachment.Amount != null && enchroachment.ChequeNo != null &&
                        enchroachment.ChequeDate != null)

                        {
                            int k1 = enchroachment.ChequeNo.Count-1;
                            int j1 = k1 - 1;
                            if (enchroachment.ChequeNo[k1] != enchroachment.ChequeNo[j1])
                            {
                                if (enchroachment.Amount.Count > 0 &&
                                enchroachment.ChequeDate.Count > 0 && enchroachment.ChequeNo.Count > 0)

                                {
                                    int ep = (enchroachment.Amount.Count) - 1;
                                    List<Enchroachmentpayment> owner1 = new List<Enchroachmentpayment>();
                                    for (int i = ep; i < enchroachment.Amount.Count; i++)
                                    {
                                        owner1.Add(new Enchroachmentpayment
                                        {
                                            Amount = enchroachment.Amount.Count <= i ? string.Empty : enchroachment.Amount[i],
                                            //  FatherName = enchroachment.FatherName.Count <= i ? string.Empty : jarai.FatherName[i],
                                            ChequeDate = enchroachment.ChequeDate.Count <= i ? string.Empty : enchroachment.ChequeDate[i],
                                            ChequeNo = enchroachment.ChequeNo.Count <= i ? string.Empty : enchroachment.ChequeNo[i],
                                            EnchId = enchroachment.Id,
                                            CreatedBy = SiteContext.UserId
                                        });
                                    }
                                    foreach (var item in owner1)
                                    {
                                        result = await _enchroachmentService.SavePayment(item);
                                    }
                                }
                            }
                        }
                        //------------------end payment details here -----------------------//

                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _enchroachmentService.GetAllEnchroachment();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(enchroachment);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(enchroachment);
                }
            }
            else
            {
                return View(enchroachment);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _enchroachmentService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _enchroachmentService.GetAllEnchroachment();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _enchroachmentService.FetchSingleResult(id);
            Data.NencroachmentList = await _enchroachmentService.GetAllNencroachment();
            Data.ReasonsList = await _enchroachmentService.GetAllReasons();

            Data.KhasraList = await _enchroachmentService.BindKhasra(Data.VillageId);
            Data.VillageList = await _enchroachmentService.GetAllVillage();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetEnchorcherNameDetails([FromBody] EncrocherNameSearchDto model)
        {
            int UserId = SiteContext.UserId;
            var result = await _enchroachmentService.GetPagedEncrocherPeople(model,UserId);
            return PartialView("_ListEName", result);
            //if (result != null)
            //{
            //    return PartialView("_ListEName", result);
            //}
            //else
            //{
            //    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            //    return PartialView();
            //}
        }
        public async Task<JsonResult> GetDetailsOwner(int? Id)
        {
            Id = Id ?? 0;
            var data = await _enchroachmentService.GetAllOwner(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.NAME,
                x.ADDRESS
                
            }));
        }
        public async Task<JsonResult> GetDetailsPayment(int? Id)
        {
            Id = Id ?? 0;
            var data = await _enchroachmentService.GetAllPayment(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.Amount,
                x.ChequeDate,
                x.ChequeNo
                
            }));
        }

    }
}