using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DamagePayee.Filters;
using Core.Enum;
namespace DamagePayee.Controllers
{
    public class PaymentController : Controller
    {
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
