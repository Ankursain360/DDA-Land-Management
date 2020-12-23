using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Threading.Tasks;

namespace DamagePayee.Controllers
{
    public class NoticeToDamagePayeeController : Controller
    {
        private readonly INoticeToDamagePayeeService _noticeToDamagePayeeService;

        public NoticeToDamagePayeeController(INoticeToDamagePayeeService noticeToDamagePayeeService)
        {
            _noticeToDamagePayeeService = noticeToDamagePayeeService;
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




    }
}
