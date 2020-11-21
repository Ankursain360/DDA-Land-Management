using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using EncroachmentDemolition.Helper;


namespace EncroachmentDemolition.Controllers
{
    public abstract class BaseController:Controller
    {
        private ISiteContext _siteContext;
        protected ISiteContext SiteContext => _siteContext ?? (_siteContext = HttpContext.RequestServices.GetService<ISiteContext>());
    }
}
