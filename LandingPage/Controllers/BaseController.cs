using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using LandingPage.Helper;


namespace LandingPage.Controllers
{
    public abstract class BaseController : Controller
    {
        private ISiteContext _siteContext;
        protected ISiteContext SiteContext => _siteContext ?? (_siteContext = HttpContext.RequestServices.GetService<ISiteContext>());
    }
}
