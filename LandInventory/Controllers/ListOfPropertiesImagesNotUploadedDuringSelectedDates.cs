﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LandInventory.Filters;
using Core.Enum;
namespace LandInventory.Controllers
{
    public class ListOfPropertiesImagesNotUploadedDuringSelectedDates : Controller
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
