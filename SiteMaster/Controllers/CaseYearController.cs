using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Threading.Tasks;

namespace SiteMaster.Controllers
{
    public class CaseYearController : BaseController
    {
        private readonly ICaseyearService _courtService;

        public CaseYearController(ICaseyearService caseyearService)
        {
            _courtService = caseyearService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> List([FromBody] CaseyearSearchDto model)
        {
            var result = await _courtService.GetPagedCaseyear(model);
            return PartialView("_List", result);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Caseyear court)
        {
            try
            {

                if (ModelState.IsValid)
                {


                    var result = await _courtService.Create(court);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //return View();
                        var list = await _courtService.GetAllCaseyear();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(court);

                    }
                }
                else
                {
                    return View(court);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(court);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _courtService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Caseyear court)
        {
            if (ModelState.IsValid)
            {
                try
                {



                    var result = await _courtService.Update(id, court);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _courtService.GetAllCaseyear();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(court);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(court);

                }
            }
            return View(court);
        }






    }
}
