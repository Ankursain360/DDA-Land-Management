using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using DamagePayeePublicInterface.Helper;

namespace DamagePayeePublicInterface.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public abstract class BaseController : Controller
    {
        private ISiteContext _siteContext;
        protected ISiteContext SiteContext => _siteContext ?? (_siteContext = HttpContext.RequestServices.GetService<ISiteContext>());
    }
}
