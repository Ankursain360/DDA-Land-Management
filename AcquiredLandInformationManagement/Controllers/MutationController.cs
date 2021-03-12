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
    public class MutationController : BaseController
    {
        private readonly IMutationService _mutationService;
        private readonly IKhasraService _khasraService;
        public IConfiguration _Configuration;
        string UploadFilePath = "";
        string targetPathGeo = "";
        public MutationController(IMutationService mutationService, IConfiguration configuration, IKhasraService khasraService)
        {
            _mutationService = mutationService;
            _Configuration = configuration;
            _khasraService = khasraService;
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            ViewBag.VillageList = await _mutationService.GetVillageList();
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemandListDetailsSearchDto model)
        {
            var result = await _mutationService.GetPagedDMSFileUploadList(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Mutation mutation)
        {
            mutation.VillageList = await _mutationService.GetVillageList();
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Mutation mutation = new Mutation();
            mutation.IsActive = 1;
            await BindDropDown(mutation);
            return View(mutation);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create(Mutation mutation)
        {
            await BindDropDown(mutation);
            mutation.VillageList = await _mutationService.GetVillageList();

            if (ModelState.IsValid)
            {
                mutation.CreatedBy = SiteContext.UserId;
                var result = await _mutationService.Create(mutation);

                if (result)
                {
                    //****** code for saving  Mutation Particulars *****
                    if (mutation.Name[0] == null && mutation.FatherName[0] == null && mutation.Address[0] == null && mutation.Share[0] == null)
                    {
                    }
                    else
                    {
                        List<Mutationparticulars> mutationparticulars = new List<Mutationparticulars>();
                        for (int i = 0; i < mutation.Name.Count; i++)
                        {
                            mutationparticulars.Add(new Mutationparticulars
                            {
                                Name = mutation.Name.Count <= i ? string.Empty : mutation.Name[i],
                                FatherName = mutation.FatherName.Count <= i ? string.Empty : mutation.FatherName[i],
                                Share = mutation.Address.Count <= i ? string.Empty : mutation.Share[i],
                                Address = mutation.Share.Count <= i ? string.Empty : mutation.Address[i],
                                CreatedBy = SiteContext.UserId,
                                MutationId = mutation.Id
                            });
                        }
                        result = await _mutationService.SaveMutationParticulars(mutationparticulars);
                    }

                }
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    ViewBag.VillageList = await _mutationService.GetVillageList();
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    await BindDropDown(mutation);
                    return View(mutation);

                }
            }
            else
            {
                return View(mutation);
            }

        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _mutationService.FetchSingleResult(id);
            await BindDropDown(Data);
            Data.KhasraNoList = await _mutationService.GetKhasraList(Data.AcquiredVillageId);
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
        public async Task<IActionResult> Edit(int id, Mutation mutation)
        {
            await BindDropDown(mutation);
            mutation.VillageList = await _mutationService.GetVillageList();

            if (ModelState.IsValid)
            {
                mutation.ModifiedBy = SiteContext.UserId;
                var result = await _mutationService.Update(id, mutation);

                if (result)
                {
                    //****** code for saving  Mutation Particulars *****
                    if (mutation.Name[0] == null && mutation.FatherName[0] == null && mutation.Address[0] == null && mutation.Share[0] == null)
                    {
                    }
                    else
                    {
                        List<Mutationparticulars> mutationparticulars = new List<Mutationparticulars>();
                        for (int i = 0; i < mutation.Name.Count; i++)
                        {
                            mutationparticulars.Add(new Mutationparticulars
                            {
                                Name = mutation.Name.Count <= i ? string.Empty : mutation.Name[i],
                                FatherName = mutation.FatherName.Count <= i ? string.Empty : mutation.FatherName[i],
                                Share = mutation.Address.Count <= i ? string.Empty : mutation.Share[i],
                                Address = mutation.Share.Count <= i ? string.Empty : mutation.Address[i],
                                CreatedBy = SiteContext.UserId,
                                MutationId = mutation.Id
                            });
                        }

                        result = await _mutationService.DeleteMutationParticulars(mutation.Id);
                        result = await _mutationService.SaveMutationParticulars(mutationparticulars);
                    }

                }
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    ViewBag.VillageList = await _mutationService.GetVillageList();
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    await BindDropDown(mutation);
                    return View(mutation);

                }
            }
            else
            {
                return View(mutation);
            }
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _mutationService.FetchSingleResult(id);
            await BindDropDown(Data);
            Data.KhasraNoList = await _mutationService.GetKhasraList(Data.AcquiredVillageId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mutationService.Delete(id, SiteContext.UserId);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            ViewBag.VillageList = await _mutationService.GetVillageList();
            return View("Index");
        }
        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? Id)
        {
            Id = Id ?? 0;
            return Json(await _mutationService.GetKhasraList(Convert.ToInt32(Id)));
        }

        public async Task<JsonResult> GetDetailsMutationParticulars(int? Id)
        {
            Id = Id ?? 0;
            var data = await _mutationService.GetMutationParticulars(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.Name,
                x.FatherName,
                x.Address,
                x.Share
            }));
        }

        public async Task<PartialViewResult> KhasraView(int id)
        {
            var Data = await _khasraService.FetchSingleResult(id);
            if (Data != null)
            {
                Data.LandCategoryList = await _khasraService.GetAllLandCategory();
                Data.VillageList = await _khasraService.GetAllVillageList();
            }
            return PartialView("_KhasraView", Data);
        }

    }

}
