using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<PartialViewResult> GetDetails(int? fileNo)
        {
            try
            {
                fileNo = fileNo ?? 0;
                // return id;
                var result = await _noticeToDamagePayeeService.GetAllDamagepayeeregister(Convert.ToInt32(fileNo));
                return PartialView("_List", result);
            }
            catch (Exception ex)
            {
                return PartialView("_List");
            }
            //fileNo = fileNo ?? 0;
            //// return id;
            //var result = await _noticeToDamagePayeeService.GetAllDamagepayeeregister(Convert.ToInt32(fileNo));
            //return PartialView("_List", result);
        }
    }
}
