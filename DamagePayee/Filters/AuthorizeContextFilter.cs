using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Enum;
using Libraries.Service.IApplicationService;
using DamagePayee.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;

namespace DamagePayee.Filters
{
    public class AuthorizeContextFilter : IAsyncAuthorizationFilter
    {
        private readonly IPermissionsService _permissionsService;
        private readonly ViewAction _viewAction;
        private readonly ISiteContext _siteContext;
        private readonly IConfiguration _configuration;
        public AuthorizeContextFilter(ViewAction viewAction,
            IPermissionsService permissionsService,
            ISiteContext siteContext,
            IConfiguration configuration)
        {
            _permissionsService = permissionsService;
            _viewAction = viewAction;
            _siteContext = siteContext;
            _configuration = configuration;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var controllerName = context.ActionDescriptor.RouteValues["controller"];
            var actionName = context.ActionDescriptor.RouteValues["action"];
            string url = "/" + controllerName + "/";
            bool hasPermission = await _permissionsService.ValidatePermission(_viewAction,
                                _siteContext.RoleId.Value, _configuration.GetSection("ModuleId").Value, url);
            if (!hasPermission)
            {
                context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "UnAuthorized",
                        }));
            }
        }
    }

    public class AuthorizeContextAttribute : TypeFilterAttribute
    {
        public AuthorizeContextAttribute(ViewAction viewAction)
            : base(typeof(AuthorizeContextFilter))
        {
            Arguments = new object[] { viewAction };
        }
    }
}
