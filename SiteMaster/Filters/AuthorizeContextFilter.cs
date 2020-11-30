using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Enum;
using Libraries.Repository.IEntityRepository;

namespace SiteMaster.Filters
{
    public class AuthorizeContextFilter : IAsyncAuthorizationFilter
    {
        private readonly IPermissionsRepository _permissionsRepository;
        public AuthorizeContextFilter(IPermissionsRepository permissionsRepository)
        {
            _permissionsRepository = permissionsRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            
        }
    }

    public class AuthorizeContextAttribute: TypeFilterAttribute
    {
        public AuthorizeContextAttribute(ViewAction viewAction) : base(typeof(AuthorizeContextFilter))
        {
            Arguments = new object[] { viewAction };
        }
    }
}
