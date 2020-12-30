using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

using FileDataLoading.Helper;

namespace FileDataLoading.Controllers
{
    public abstract class BaseController : Controller
    {
        private ISiteContext _siteContext;
        protected ISiteContext SiteContext => _siteContext ?? (_siteContext = HttpContext.RequestServices.GetService<ISiteContext>());
    }
}
