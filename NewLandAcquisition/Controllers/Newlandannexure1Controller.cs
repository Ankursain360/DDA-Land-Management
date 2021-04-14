using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using NewLandAcquisition.Filters;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Utility.Helper;

namespace NewLandAcquisition.Controllers
{
     public class Newlandannexure1Controller : BaseController
     {
          private readonly INewlandannexure1Service _newlandannexure1Service;
          private readonly IRequestService _requestService;
        public Newlandannexure1Controller(INewlandannexure1Service newlandannexure1Service, IRequestService requestService)
          {
            _newlandannexure1Service = newlandannexure1Service;
            _requestService = requestService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
          {
            return View();
          }
        public async Task<PartialViewResult> RequestView(int id)
        {
            var Data = await _requestService.FetchSingleResult(id);
             return PartialView("_Request", Data);
        }

        
       public async Task<JsonResult> GetDetailsKhasra(int? Id)
        {
            Id = Id ?? 0;
            var data = await _newlandannexure1Service.GetAllKhasraRpt(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.KhasraNo,
                x.Bigha,
                x.Biswa,
                x.Biswanshi,
                x.OwnershipStatus,
                x.OwnerName
            }));
        }
        async Task BindDropDown(Newlandannexure1 Annexure1)
        {
            Annexure1.MunicipalityList = await _newlandannexure1Service.GetAllMunicipality();
            Annexure1.DistrictList = await _newlandannexure1Service.GetAllDistrict();
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            Newlandannexure1 Annexure1 = new Newlandannexure1();
            Annexure1.RequestId = id;
            Annexure1.IsActive = 1;
           
            var Data = await _newlandannexure1Service.FetchSingleResult(id);
            if (Data != null)
            {
                ViewBag.Anexx1Id = Data.Id;
                await BindDropDown(Data);
                return View(Data);
            }
            else
            {
               
                ViewBag.Anexx1Id = 0;
                await BindDropDown(Annexure1);
                return View(Annexure1);
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id ,Newlandannexure1 Annexure1)
        {

            await BindDropDown(Annexure1);
           
            if (ModelState.IsValid)
            {
                
                if (Annexure1.Id == 0)
                {
                   
                    Annexure1.CreatedBy = SiteContext.UserId;
                    
                    var result = await _newlandannexure1Service.Create(Annexure1);

                    if (result == true)
                    {
                        //************ Save Khasra  ************  
                        if (Annexure1.KhasraNo != null &&
                            Annexure1.Bigha != null &&
                            Annexure1.Biswa != null)

                        {
                            if (Annexure1.KhasraNo.Count > 0 &&
                                Annexure1.Bigha.Count > 0 &&
                                Annexure1.Biswa.Count > 0
                               )

                            {
                                List<Newlandannexure1khasrarpt> khasra = new List<Newlandannexure1khasrarpt>();
                                for (int i = 0; i < Annexure1.OwnerName.Count; i++)
                                {
                                    khasra.Add(new Newlandannexure1khasrarpt
                                    {
                                        KhasraNo = Annexure1.KhasraNo.Count <= i ? string.Empty : Annexure1.KhasraNo[i],
                                        Bigha = Annexure1.Bigha.Count <= i ? 0 : Annexure1.Bigha[i],
                                        Biswa = Annexure1.Biswa.Count <= i ? 0 : Annexure1.Biswa[i],
                                        Biswanshi = Annexure1.Biswanshi.Count <= i ? 0 : Annexure1.Biswanshi[i],
                                        OwnershipStatus = Annexure1.OwnershipStatus.Count <= i ? string.Empty : Annexure1.OwnershipStatus[i],
                                        OwnerName = Annexure1.OwnerName.Count <= i ? string.Empty : Annexure1.OwnerName[i],
                                        NewLandAnnexure1Id = Annexure1.Id,
                                        CreatedBy = SiteContext.UserId,
                                        IsActive = 1
                                    });
                                }
                                foreach (var item in khasra)
                                {
                                    result = await _newlandannexure1Service.SaveKhasraRpt(item);
                                }
                            }
                        }

                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);

                        return RedirectToAction("Index", "RequestApprovalProcess");

                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Annexure1);
                    }
                }
                else
                {

                    Annexure1.ModifiedBy = SiteContext.UserId;
                    var result = await _newlandannexure1Service.Update(Annexure1.Id, Annexure1);
                    if (result == true)
                    {

                        //************ Save Khasra  ************  
                        if (Annexure1.KhasraNo != null &&
                             Annexure1.Bigha != null &&
                             Annexure1.Biswa != null)

                        {
                            if (Annexure1.KhasraNo.Count > 0 &&
                                Annexure1.Bigha.Count > 0 &&
                                Annexure1.Biswa.Count > 0
                               )
                            {
                                List<Newlandannexure1khasrarpt> khasra = new List<Newlandannexure1khasrarpt>();
                                result = await _newlandannexure1Service.DeleteKhasraRpt(Annexure1.Id);
                                for (int i = 0; i < Annexure1.OwnerName.Count; i++)
                              
                                {
                                    khasra.Add(new Newlandannexure1khasrarpt
                                    {
                                        KhasraNo = Annexure1.KhasraNo.Count <= i ? string.Empty : Annexure1.KhasraNo[i],
                                        Bigha = Annexure1.Bigha.Count <= i ? 0 : Annexure1.Bigha[i],
                                        Biswa = Annexure1.Biswa.Count <= i ? 0 : Annexure1.Biswa[i],
                                        Biswanshi = Annexure1.Biswanshi.Count <= i ? 0 : Annexure1.Biswanshi[i],
                                        OwnershipStatus = Annexure1.OwnershipStatus.Count <= i ? string.Empty : Annexure1.OwnershipStatus[i],
                                        OwnerName = Annexure1.OwnerName.Count <= i ? string.Empty : Annexure1.OwnerName[i],
                                        NewLandAnnexure1Id = Annexure1.Id,
                                        CreatedBy = SiteContext.UserId,
                                        IsActive = 1
                                    });
                                }
                                foreach (var item in khasra)
                                {
                                    result = await _newlandannexure1Service.SaveKhasraRpt(item);
                                }
                            }
                        }
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        return RedirectToAction("Index", "RequestApprovalProcess");
                    }

                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Annexure1);
                    }
                }
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(Annexure1);
            }

        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            Newlandannexure1 Annexure1 = new Newlandannexure1();
            Annexure1.RequestId = id;
            Annexure1.IsActive = 1;

            var Data = await _newlandannexure1Service.FetchSingleResult(id);
            if (Data != null)
            {
                ViewBag.Anexx1Id = Data.Id;
                await BindDropDown(Data);
                return View(Data);
            }
            else
            {
                ViewBag.Anexx1Id = 0;
                await BindDropDown(Annexure1);
                return View(Annexure1);
            }

        }
    }
}
