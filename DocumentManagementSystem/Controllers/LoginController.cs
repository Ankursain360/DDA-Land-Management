using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DocumentManagementSystem.Controllers
{
    public class LoginController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

      



    }
}
