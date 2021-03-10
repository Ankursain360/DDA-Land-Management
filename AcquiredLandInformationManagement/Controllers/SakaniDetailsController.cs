using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace AcquiredLandInformationManagement.Controllers
{
    public class SakaniDetailsController : BaseController
    {
        private readonly ISakanidetailService _sakanidetailService;
        public SakaniDetailsController(ISakanidetailService sakanidetailService)
        {
            _sakanidetailService = sakanidetailService;
        }

        public  IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] SakaniDetailsSearchDto model)
        {
            var result = await _sakanidetailService.GetPagedSaknidetail(model);
            return PartialView("_List", result);
        }

        public async Task<IActionResult> Create()
        {
            Saknidetails sakni = new Saknidetails();
            sakni.IsActive = 1;
            sakni.AcquiredlandvillageList = await _sakanidetailService.GetAllVillage();
            sakni.KhasraList = await _sakanidetailService.GetAllKhasra(sakni.VillageId);
            sakni.VillageId = 0;
            return View(sakni);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Saknidetails sakni)
        {

            sakni.AcquiredlandvillageList = await _sakanidetailService.GetAllVillage();
            sakni.KhasraList = await _sakanidetailService.GetAllKhasra(sakni.VillageId);
            
            if (ModelState.IsValid)
            {
                sakni.CreatedBy = SiteContext.UserId;
                var result = await _sakanidetailService.Create(sakni);

                if (result == true)
                {
                    //************ Save Owner  ************  
                    if (sakni.OwnerName != null &&
                        sakni.FatherName != null &&
                        sakni.Address != null)

                    {
                        if (sakni.FatherName.Count > 0 &&
                            sakni.FatherName.Count > 0 &&
                            sakni.Address.Count > 0
                           )

                        {
                            List<Sakniowner> owner = new List<Sakniowner>();
                            for (int i = 0; i < sakni.OwnerName.Count; i++)
                            {
                                owner.Add(new Sakniowner
                                {
                                    OwnerName = sakni.OwnerName.Count <= i ? string.Empty : sakni.OwnerName[i],
                                    FatherName = sakni.FatherName.Count <= i ? string.Empty : sakni.FatherName[i],
                                    Address = sakni.Address.Count <= i ? string.Empty : sakni.Address[i],
                                    SakniDetailId = sakni.Id,
                                    CreatedBy = SiteContext.UserId
                                });
                            }
                            foreach (var item in owner)
                            {
                                result = await _sakanidetailService.SaveOwner(item);
                            }
                        }
                    }


                    //************ Save Lessee details  ************  

                    if (sakni.LesseeName != null &&
                        sakni.LFather != null &&
                        sakni.LAddress != null &&
                        sakni.LShare != null &&
                        sakni.LMortgage != null
                        )

                    {
                        if (sakni.LesseeName.Count > 0 &&
                            sakni.LFather.Count > 0 &&
                            sakni.LAddress.Count > 0 &&
                            sakni.LShare.Count > 0 &&
                            sakni.LMortgage.Count > 0
                           )

                        {
                            List<Saknilessee> lessee = new List<Saknilessee>();
                            for (int i = 0; i < sakni.LesseeName.Count; i++)
                            {
                                lessee.Add(new Saknilessee
                                {
                                    LesseeName = sakni.LesseeName.Count <= i ? string.Empty : sakni.LesseeName[i],
                                    FatherName = sakni.LFather.Count <= i ? string.Empty : sakni.LFather[i],
                                    Address = sakni.LAddress.Count <= i ? string.Empty : sakni.LAddress[i],
                                    LesseeShare = sakni.LShare.Count <= i ? string.Empty : sakni.LShare[i],
                                    LesseeMortgage = sakni.LMortgage.Count <= i ? string.Empty : sakni.LMortgage[i],
                                    SakniDetailId = sakni.Id,
                                    CreatedBy = SiteContext.UserId
                                });
                            }
                            foreach (var item in lessee)
                            {
                                result = await _sakanidetailService.Savelessee(item);
                            }
                        }
                    }

                    //************ Save tenant  ************  

                    if (sakni.TName != null &&
                        sakni.TFatherName != null &&
                        sakni.TAddress != null)

                    {
                        if (sakni.TName.Count > 0 &&
                            sakni.TFatherName.Count > 0 &&
                            sakni.TAddress.Count > 0
                           )

                        {
                            List<Saknitenant> tenant = new List<Saknitenant>();
                            for (int i = 0; i < sakni.TName.Count; i++)
                            {
                                tenant.Add(new Saknitenant
                                {
                                    TenantName = sakni.TName.Count <= i ? string.Empty : sakni.TName[i],
                                    FatherName = sakni.TFatherName.Count <= i ? string.Empty : sakni.TFatherName[i],
                                    Address = sakni.TAddress.Count <= i ? string.Empty : sakni.TAddress[i],
                                    SakniDetailId = sakni.Id,
                                    CreatedBy = SiteContext.UserId
                                });
                            }
                            foreach (var item in tenant)
                            {
                                result = await _sakanidetailService.SaveTenant(item);
                            }
                        }
                    }
                    //************ Save Khasra  ************  

                    if (
                        sakni.Plot != null &&
                        sakni.Area != null &&
                        sakni.Category != null &&
                        sakni.LeaseAmount != null &&
                        sakni.RenewalDate != null
                        )
                    {
                        Saknikhasra Khsra = new Saknikhasra();

                        Khsra.KhasraId = sakni.KhasraId;
                        Khsra.PlotNo = sakni.Plot;
                        Khsra.AreaSqYard = sakni.Area;
                        Khsra.Category = sakni.Category;
                        Khsra.LeaseAmount = sakni.LeaseAmount;
                        Khsra.RenewalDate = sakni.RenewalDate;
                        Khsra.SakniDetailId = sakni.Id;
                        Khsra.CreatedBy = SiteContext.UserId;
                        result = await _sakanidetailService.SaveSaknikhasra(Khsra);
                    }

                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    var list = await _sakanidetailService.GetAllSaknidetail();
                    return View("Index", list);

                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(sakni);
                }
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(sakni);
            }

        }

        public async Task<JsonResult> GetDetailsOwner(int? Id)
        {
            Id = Id ?? 0;
            var data = await _sakanidetailService.GetAllOwner(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.OwnerName,
                x.FatherName,
                x.Address
            }));
        }
        public async Task<JsonResult> GetSakniKhasra(int? Id)
        {
            Id = Id ?? 0;
            var data = await _sakanidetailService.FetchSingleSaknikhasra(Convert.ToInt32(Id));
           
            return Json(data);
           
        }
        public async Task<JsonResult> GetDetailslesssee(int? Id)
        {
            Id = Id ?? 0;
            var data = await _sakanidetailService.GetAllSaknilessee(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.LesseeName,
                x.FatherName,
                x.Address,
                x.LesseeShare,
                x.LesseeMortgage
            }));
        }
        public async Task<JsonResult> GetDetailsTenant(int? Id)
        {
            Id = Id ?? 0;
            var data = await _sakanidetailService.GetAllTenant(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.TenantName,
                x.FatherName,
                x.Address
            }));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _sakanidetailService.FetchSingleResult(id);
            Data.AcquiredlandvillageList = await _sakanidetailService.GetAllVillage();
            Data.KhasraList = await _sakanidetailService.GetAllKhasra(Data.VillageId);
            Saknidetails sakni = new Saknidetails();
            sakni.IsActive = sakni.IsActive;
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Saknidetails sakni)
        {

            sakni.AcquiredlandvillageList = await _sakanidetailService.GetAllVillage();
            sakni.KhasraList = await _sakanidetailService.GetAllKhasra(sakni.VillageId);
           
            if (ModelState.IsValid)
            {
               
                sakni.ModifiedBy = SiteContext.UserId;
                var result = await _sakanidetailService.Update(id, sakni);

                if (result == true)
                {
                    //************ Save Owner  ************  
                    if (sakni.OwnerName != null &&
                        sakni.FatherName != null &&
                        sakni.Address != null)

                    {
                        if (sakni.FatherName.Count > 0 &&
                            sakni.FatherName.Count > 0 &&
                            sakni.Address.Count > 0
                           )

                        {
                            List<Sakniowner> owner = new List<Sakniowner>();
                            result = await _sakanidetailService.DeleteOwner(id);
                            for (int i = 0; i < sakni.OwnerName.Count; i++)
                            {
                                owner.Add(new Sakniowner
                                {
                                    OwnerName = sakni.OwnerName.Count <= i ? string.Empty : sakni.OwnerName[i],
                                    FatherName = sakni.FatherName.Count <= i ? string.Empty : sakni.FatherName[i],
                                    Address = sakni.Address.Count <= i ? string.Empty : sakni.Address[i],
                                    SakniDetailId = sakni.Id,
                                    CreatedBy = SiteContext.UserId
                                });
                            }
                            foreach (var item in owner)
                            {
                                result = await _sakanidetailService.SaveOwner(item);
                            }
                        }
                    }


                    //************ Save Lessee details  ************  

                    if (sakni.LesseeName != null &&
                        sakni.LFather != null &&
                        sakni.LAddress != null &&
                        sakni.LShare != null &&
                        sakni.LMortgage != null
                        )

                    {
                        if (sakni.LesseeName.Count > 0 &&
                            sakni.LFather.Count > 0 &&
                            sakni.LAddress.Count > 0 &&
                            sakni.LShare.Count > 0 &&
                            sakni.LMortgage.Count > 0
                           )

                        {
                            List<Saknilessee> lessee = new List<Saknilessee>();
                            result = await _sakanidetailService.Deletelessee(id);
                            for (int i = 0; i < sakni.LesseeName.Count; i++)
                            {
                                lessee.Add(new Saknilessee
                                {
                                    LesseeName = sakni.LesseeName.Count <= i ? string.Empty : sakni.LesseeName[i],
                                    FatherName = sakni.LFather.Count <= i ? string.Empty : sakni.LFather[i],
                                    Address = sakni.LAddress.Count <= i ? string.Empty : sakni.LAddress[i],
                                    LesseeShare = sakni.LShare.Count <= i ? string.Empty : sakni.LShare[i],
                                    LesseeMortgage = sakni.LMortgage.Count <= i ? string.Empty : sakni.LMortgage[i],
                                    SakniDetailId = sakni.Id,
                                    CreatedBy = SiteContext.UserId
                                });
                            }
                            foreach (var item in lessee)
                            {
                                result = await _sakanidetailService.Savelessee(item);
                            }
                        }
                    }

                    //************ Save tenant  ************  

                    if (sakni.TName != null &&
                        sakni.TFatherName != null &&
                        sakni.TAddress != null)

                    {
                        if (sakni.TName.Count > 0 &&
                            sakni.TFatherName.Count > 0 &&
                            sakni.TAddress.Count > 0
                           )

                        {
                            List<Saknitenant> tenant = new List<Saknitenant>();
                            result = await _sakanidetailService.DeleteTenant(id);
                            for (int i = 0; i < sakni.TName.Count; i++)
                            {
                                tenant.Add(new Saknitenant
                                {
                                    TenantName = sakni.TName.Count <= i ? string.Empty : sakni.TName[i],
                                    FatherName = sakni.TFatherName.Count <= i ? string.Empty : sakni.TFatherName[i],
                                    Address = sakni.TAddress.Count <= i ? string.Empty : sakni.TAddress[i],
                                    SakniDetailId = sakni.Id,
                                    CreatedBy = SiteContext.UserId
                                });
                            }
                            foreach (var item in tenant)
                            {
                                result = await _sakanidetailService.SaveTenant(item);
                            }
                        }
                    }



                    //************ Save Khasra  ************  

                    if (sakni.KhasraId != null
                      
                        )
                    {
                       
                        Saknikhasra Khsra = new Saknikhasra();

                        Khsra.KhasraId = sakni.KhasraId;
                        Khsra.PlotNo = sakni.Plot;
                        Khsra.AreaSqYard = sakni.Area;
                        Khsra.Category = sakni.Category;
                        Khsra.LeaseAmount = sakni.LeaseAmount;
                        Khsra.RenewalDate = sakni.RenewalDate;
                        Khsra.SakniDetailId = sakni.Id;
                        Khsra.CreatedBy = SiteContext.UserId;
                        
                       result = await _sakanidetailService.UpdateKhasra(id,Khsra);
                    }

                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    var list = await _sakanidetailService.GetAllSaknidetail();
                    return View("Index", list);

                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(sakni);
                }
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(sakni);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
           
                var result = await _sakanidetailService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
            
               var list = await _sakanidetailService.GetAllSaknidetail();
               return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _sakanidetailService.FetchSingleResult(id);

            Data.AcquiredlandvillageList = await _sakanidetailService.GetAllVillage();
            Data.KhasraList = await _sakanidetailService.GetAllKhasra(Data.VillageId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _sakanidetailService.GetAllKhasra(Convert.ToInt32(villageId)));
        }
    }
}