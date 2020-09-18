using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace AcquiredLandInformationManagement.Controllers
{
  
    public class DisposalLandController : Controller
    {
        private readonly IDisposallandService _disposallandService;

        public DisposalLandController(IDisposallandService disposallandService)
        {
            _disposallandService = disposallandService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _disposallandService.GetAllDisposalland();
            return View(result);
        }
        public async Task<IActionResult> Create()
        {
            Disposalland disposalland = new Disposalland();
            //proposalplotdetails.IsActive = 1;

            disposalland.UtilizationtypeList = await _disposallandService.GetAllUtilizationtype();
            disposalland.VillageList = await _disposallandService.GetAllVillage();
            disposalland.KhasraList = await _disposallandService.GetAllKhasra();

            return View(disposalland);
        }


        [HttpPost]

        public async Task<IActionResult> Create(Disposalland disposalland)
        {
            try
            {
                disposalland.UtilizationtypeList = await _disposallandService.GetAllUtilizationtype();
                disposalland.VillageList = await _disposallandService.GetAllVillage();
                disposalland.KhasraList = await _disposallandService.GetAllKhasra();
                if (ModelState.IsValid)
                {


                    var result = await _disposallandService.Create(disposalland);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _disposallandService.GetAllDisposalland();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(disposalland);

                    }
                }
                else
                {
                    return View(disposalland);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(disposalland);
            }
        }
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _disposallandService.FetchSingleResult(id);

            Data.UtilizationtypeList = await _disposallandService.GetAllUtilizationtype();
            Data.VillageList = await _disposallandService.GetAllVillage();
            Data.KhasraList = await _disposallandService.GetAllKhasra();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, Disposalland disposalland)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _disposallandService.Update(id, disposalland);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _disposallandService.GetAllDisposalland();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(disposalland);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(disposalland);
        }

       

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _disposallandService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }
            var result = await _disposallandService.GetAllDisposalland();
            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View("Index", result);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _disposallandService.FetchSingleResult(id);

            Data.UtilizationtypeList = await _disposallandService.GetAllUtilizationtype();
            Data.VillageList = await _disposallandService.GetAllVillage();
            Data.KhasraList = await _disposallandService.GetAllKhasra();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }
}