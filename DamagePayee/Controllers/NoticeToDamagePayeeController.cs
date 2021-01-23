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
using DamagePayee.Filters;
using Core.Enum;
namespace DamagePayee.Controllers
{
    public class NoticeToDamagePayeeController : Controller
    {
        private readonly INoticeToDamagePayeeService _noticeToDamagePayeeService;

        public NoticeToDamagePayeeController(INoticeToDamagePayeeService noticeToDamagePayeeService)
        {
            _noticeToDamagePayeeService = noticeToDamagePayeeService;
        }


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _noticeToDamagePayeeService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Noticetodamagepayee noticetodamagepayee)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _noticeToDamagePayeeService.Update(id, noticetodamagepayee);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        return View("_List", noticetodamagepayee);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(noticetodamagepayee);

                    }
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }
            return View(noticetodamagepayee);
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Noticetodamagepayee noticetodamagepayee)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _noticeToDamagePayeeService.Create(noticetodamagepayee);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //var list = await _noticeToDamagePayeeService.GetAllNoticetoDamagePayee();
                        // var F = noticetodamagepayee.FileNo;
                        return View("_List", noticetodamagepayee);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(noticetodamagepayee);
                    }
                }
                else
                {
                    return View(noticetodamagepayee);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(noticetodamagepayee);
            }
        }



        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NoticetodamagepayeeSearchDto model)
        {
            var result = await _noticeToDamagePayeeService.GetPagedNoticetodamagepayee(model);

            return PartialView("_List1", result);
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _noticeToDamagePayeeService.FetchSingleResult(id);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }



    }
}
