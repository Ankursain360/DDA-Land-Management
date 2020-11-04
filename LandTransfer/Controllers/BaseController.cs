using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using LandTransfer.Helper;

namespace LandTransfer.Controllers
{
    public class BaseController : Controller
    {
        private ISiteContext _siteContext;
        protected ISiteContext SiteContext => _siteContext ?? (_siteContext = HttpContext.RequestServices.GetService<ISiteContext>());
    }
}