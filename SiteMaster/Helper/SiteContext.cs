using Microsoft.AspNetCore.Http;
using Service.IApplicationService;
using System;
using System.Linq;

namespace SiteMaster.Helper
{
    public class SiteContext : ISiteContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserProfileService _userProfileService;
        public SiteContext(IHttpContextAccessor httpContextAccessor,
            IUserProfileService userProfileService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userProfileService = userProfileService;
        }

        public int UserId { 
            get {
                string userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(a=>a.Type=="sub").Value;
                return Convert.ToInt32(userId);
            }
            set {
                
            } 
        }

        public int ProfileId { 
            get {
                string userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "sub").Value;
                var user =  _userProfileService.GetUserById(Convert.ToInt32(userId)).GetAwaiter().GetResult();
                return user.Id;
            } set => throw new NotImplementedException(); 
        }

        public int? RoleId { 
            get {
                string userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "sub").Value;
                var user = _userProfileService.GetUserById(Convert.ToInt32(userId)).GetAwaiter().GetResult();
                return user.RoleId;
            } set => throw new NotImplementedException(); 
        }
    }
}
