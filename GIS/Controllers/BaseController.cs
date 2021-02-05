using GIS.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace GIS.Controllers
{
    public abstract class BaseController : Controller
    {
        private ISiteContext _siteContext;
        protected ISiteContext SiteContext => _siteContext ?? (_siteContext = HttpContext.RequestServices.GetService<ISiteContext>());
    }
}
