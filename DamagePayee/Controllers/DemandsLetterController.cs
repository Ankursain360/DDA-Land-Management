using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Threading.Tasks;

namespace DamagePayee.Controllers
{
    public class DemandsLetterController : Controller
    {
        private readonly IDemandLetterService _demandLetterService;
        public DemandsLetterController(IDemandLetterService demandLetterService)
        {
            _demandLetterService = demandLetterService;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Demandletters demandletter)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _demandLetterService.Create(demandletter);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //var list = await _noticeToDamagePayeeService.GetAllNoticetoDamagePayee();
                        // var F = noticetodamagepayee.FileNo;
                        return View("_List", demandletter);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(demandletter);
                    }
                }
                else
                {
                    return View(demandletter);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(demandletter);
            }
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemandletterSearchDto model)
        {
            var result = await _demandLetterService.GetPagedDemandletter(model);

            return PartialView("_List1", result);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _demandLetterService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Demandletters demandletter)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _demandLetterService.Update(id, demandletter);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        return View("_List", demandletter);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(demandletter);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(demandletter);
        }



        public async Task<IActionResult> View(int id)
        {
            var Data = await _demandLetterService.FetchSingleResult(id);
           

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }



    }
}
