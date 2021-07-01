using Microsoft.AspNetCore.Mvc;
using EncroachmentDemolition.Filters;
using Core.Enum;
using System.Data;
namespace EncroachmentDemolition.Controllers
{
    public class AnnexureB : Controller
    {



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
    }
}
