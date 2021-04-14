using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
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
    public class JaraiDetailsController : BaseController
    {
        private readonly IJaraidetailService _jaraidetailService;
        public JaraiDetailsController(IJaraidetailService jaraidetailService)
        {
            _jaraidetailService = jaraidetailService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] JaraiDetailsSearchDto model)
        {
            var result = await _jaraidetailService.GetPagedJaraidetail(model);
            return PartialView("_List", result);
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Jaraidetails jarai = new Jaraidetails();
            jarai.IsActive = 1;
            jarai.AcquiredlandvillageList = await _jaraidetailService.GetAllVillage();
            jarai.KhasraList = await _jaraidetailService.GetAllKhasra(jarai.VillageId);
            return View(jarai);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Jaraidetails jarai)
        {

                jarai.AcquiredlandvillageList = await _jaraidetailService.GetAllVillage();
                jarai.KhasraList = await _jaraidetailService.GetAllKhasra(jarai.VillageId);

                if (ModelState.IsValid)
                {
                    jarai.CreatedBy = SiteContext.UserId;
                    var result = await _jaraidetailService.Create(jarai);

                    if (result == true)
                    {
                    //************ Save Owner  ************  
                    if (jarai.OwnerName != null &&
                        jarai.FatherName != null &&
                        jarai.Address != null )
                     
                    {
                        if (jarai.FatherName.Count > 0 &&
                            jarai.FatherName.Count > 0 &&
                            jarai.Address.Count > 0 
                           )

                        {
                            List<Jaraiowner> owner = new List<Jaraiowner>();
                            for (int i = 0; i < jarai.OwnerName.Count; i++)
                            {
                                owner.Add(new Jaraiowner
                                {
                                    OwnerName = jarai.OwnerName.Count <= i ? string.Empty : jarai.OwnerName[i],
                                    FatherName = jarai.FatherName.Count <= i ? string.Empty : jarai.FatherName[i],
                                    Address = jarai.Address.Count <= i ? string.Empty : jarai.Address[i],
                                    JaraiDetailId = jarai.Id,
                                    CreatedBy = SiteContext.UserId
                            });
                            }
                            foreach (var item in owner)
                            {
                                result = await _jaraidetailService.SaveOwner(item);
                            }
                        }
                    }


                    //************ Save Lessee details  ************  

                    if (jarai.LesseeName != null &&
                        jarai.Father != null &&
                        jarai.LAddress != null &&
                        jarai.Mortgage != null
                        )

                    {
                        if (jarai.LesseeName.Count > 0 &&
                            jarai.Father.Count > 0 &&
                            jarai.LAddress.Count > 0 &&
                            jarai.Mortgage.Count > 0
                           )

                        {
                            List<Jarailessee> lessee = new List<Jarailessee>();
                            for (int i = 0; i < jarai.LesseeName.Count; i++)
                            {
                                lessee.Add(new Jarailessee
                                {
                                    LesseeName = jarai.LesseeName.Count <= i ? string.Empty : jarai.LesseeName[i],
                                    FatherName = jarai.Father.Count <= i ? string.Empty : jarai.Father[i],
                                    Address = jarai.LAddress.Count <= i ? string.Empty : jarai.LAddress[i],
                                    MortgageDetails = jarai.Mortgage.Count <= i ? string.Empty : jarai.Mortgage[i],
                                    JaraiDetailId = jarai.Id,
                                    CreatedBy = SiteContext.UserId
                                });
                            }
                            foreach (var item in lessee)
                            {
                                result = await _jaraidetailService.SaveJarailessee(item);
                            }
                        }
                    }

                    //************ Save farmer  ************  

                    if (jarai.FarmerName != null &&
                        jarai.FFatherName != null &&
                        jarai.FAddress != null)

                    {
                        if (jarai.FarmerName.Count > 0 &&
                            jarai.FFatherName.Count > 0 &&
                            jarai.FAddress.Count > 0
                           )

                        {
                            List<Jaraifarmer> farmer = new List<Jaraifarmer>();
                            for (int i = 0; i < jarai.FarmerName.Count; i++)
                            {
                                farmer.Add(new Jaraifarmer
                                {
                                    FarmerName = jarai.FarmerName.Count <= i ? string.Empty : jarai.FarmerName[i],
                                    FatherName = jarai.FFatherName.Count <= i ? string.Empty : jarai.FFatherName[i],
                                    Address = jarai.FAddress.Count <= i ? string.Empty : jarai.FAddress[i],
                                    JaraiDetailId = jarai.Id,
                                    CreatedBy = SiteContext.UserId
                                });
                            }
                            foreach (var item in farmer)
                            {
                                result = await _jaraidetailService.Savefarmer(item);
                            }
                        }
                    }


                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    var list = await _jaraidetailService.GetAllJaraidetail();
                    return View("Index", list);

                }
                else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(jarai);
                    }
                }
                else
                {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(jarai);
                }
            
        }

        public async Task<JsonResult> GetDetailsOwner(int? Id)
        {
            Id = Id ?? 0;
            var data = await _jaraidetailService.GetAllOwner(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.OwnerName,
                x.FatherName,
                x.Address
            }));
        }
        public async Task<JsonResult> GetDetailslesssee(int? Id)
        {
            Id = Id ?? 0;
            var data = await _jaraidetailService.GetAllJarailessee(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.LesseeName,
                x.FatherName,
                x.Address,
                x.MortgageDetails
            }));
        }
        public async Task<JsonResult> GetDetailsFarmer(int? Id)
        {
            Id = Id ?? 0;
            var data = await _jaraidetailService.GetAllFarmer(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.FarmerName,
                x.FatherName,
                x.Address
            }));
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _jaraidetailService.FetchSingleResult(id);

            Data.AcquiredlandvillageList = await _jaraidetailService.GetAllVillage();
            Data.KhasraList = await _jaraidetailService.GetAllKhasra(Data.VillageId);
            Jaraidetails jarai = new Jaraidetails();
            jarai.IsActive = jarai.IsActive;


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Jaraidetails jarai)
        {
            if (ModelState.IsValid)
            {
                
                    jarai.AcquiredlandvillageList = await _jaraidetailService.GetAllVillage();
                    jarai.KhasraList = await _jaraidetailService.GetAllKhasra(jarai.VillageId);
                    jarai.ModifiedBy = SiteContext.UserId;
                    var result = await _jaraidetailService.Update(id, jarai);
                    if (result == true)
                    {

                    //************ Save Owner  ************  
                    if (jarai.OwnerName != null &&
                        jarai.FatherName != null &&
                        jarai.Address != null)

                    {
                        if (jarai.FatherName.Count > 0 &&
                            jarai.FatherName.Count > 0 &&
                            jarai.Address.Count > 0
                           )

                        {
                            List<Jaraiowner> owner = new List<Jaraiowner>();
                            result = await _jaraidetailService.DeleteOwner(id);
                            for (int i = 0; i < jarai.OwnerName.Count; i++)
                            {
                                owner.Add(new Jaraiowner
                                {
                                    OwnerName = jarai.OwnerName.Count <= i ? string.Empty : jarai.OwnerName[i],
                                    FatherName = jarai.FatherName.Count <= i ? string.Empty : jarai.FatherName[i],
                                    Address = jarai.Address.Count <= i ? string.Empty : jarai.Address[i],
                                    JaraiDetailId = jarai.Id,
                                    CreatedBy = SiteContext.UserId
                                });
                            }
                            foreach (var item in owner)
                            {
                                result = await _jaraidetailService.SaveOwner(item);
                            }
                        }
                    }


                    //************ Save Lessee details  ************  

                    if (jarai.LesseeName != null &&
                        jarai.Father != null &&
                        jarai.LAddress != null &&
                        jarai.Mortgage != null
                        )

                    {
                        if (jarai.LesseeName.Count > 0 &&
                            jarai.Father.Count > 0 &&
                            jarai.LAddress.Count > 0 &&
                            jarai.Mortgage.Count > 0
                           )

                        {
                            List<Jarailessee> lessee = new List<Jarailessee>();
                            result = await _jaraidetailService.DeleteJarailessee(id);
                            for (int i = 0; i < jarai.LesseeName.Count; i++)
                            {
                                lessee.Add(new Jarailessee
                                {
                                    LesseeName = jarai.LesseeName.Count <= i ? string.Empty : jarai.LesseeName[i],
                                    FatherName = jarai.Father.Count <= i ? string.Empty : jarai.Father[i],
                                    Address = jarai.LAddress.Count <= i ? string.Empty : jarai.LAddress[i],
                                    MortgageDetails = jarai.Mortgage.Count <= i ? string.Empty : jarai.Mortgage[i],
                                    JaraiDetailId = jarai.Id,
                                    CreatedBy = SiteContext.UserId
                                });
                            }
                            foreach (var item in lessee)
                            {
                                result = await _jaraidetailService.SaveJarailessee(item);
                            }
                        }
                    }

                    //************ Save farmer  ************  

                    if (jarai.FarmerName != null &&
                        jarai.FFatherName != null &&
                        jarai.FAddress != null)

                    {
                        if (jarai.FarmerName.Count > 0 &&
                            jarai.FFatherName.Count > 0 &&
                            jarai.FAddress.Count > 0
                           )

                        {
                            List<Jaraifarmer> farmer = new List<Jaraifarmer>();
                            result = await _jaraidetailService.DeleteFarmer(id);
                            for (int i = 0; i < jarai.FarmerName.Count; i++)
                            {
                                farmer.Add(new Jaraifarmer
                                {
                                    FarmerName = jarai.FarmerName.Count <= i ? string.Empty : jarai.FarmerName[i],
                                    FatherName = jarai.FFatherName.Count <= i ? string.Empty : jarai.FFatherName[i],
                                    Address = jarai.FAddress.Count <= i ? string.Empty : jarai.FAddress[i],
                                    JaraiDetailId = jarai.Id,
                                    CreatedBy = SiteContext.UserId
                                });
                            }
                            foreach (var item in farmer)
                            {
                                result = await _jaraidetailService.Savefarmer(item);
                            }
                        }
                    }



                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _jaraidetailService.GetAllJaraidetail();
                        return View("Index", list);
                    }

                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(jarai);
                    }
                
            }
            else
            {
                return View(jarai);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
          
                var result = await _jaraidetailService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
          
               var list = await _jaraidetailService.GetAllJaraidetail();
               return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _jaraidetailService.FetchSingleResult(id);

            Data.AcquiredlandvillageList = await _jaraidetailService.GetAllVillage();
            Data.KhasraList = await _jaraidetailService.GetAllKhasra(Data.VillageId);

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
            return Json(await _jaraidetailService.GetAllKhasra(Convert.ToInt32(villageId)));
        }

    }
}