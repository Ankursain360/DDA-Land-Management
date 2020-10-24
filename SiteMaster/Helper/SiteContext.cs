using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace SiteMaster.Helper
{
    public class SiteContext : ISiteContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SiteContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId { 
            get {
                string userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(a=>a.Type=="sub").Value;
                return Convert.ToInt32(userId);
            }
            set {
                
            } 
        }
    }
}
