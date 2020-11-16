using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMaster.Components
{
    public class PermissionViewComponent : ViewComponent
    {
        public PermissionViewComponent()
        {

        }
        public async Task<IViewComponentResult> InvokeAsync(string pageName)
        {
            return View("Permission");
        }
    }
}
