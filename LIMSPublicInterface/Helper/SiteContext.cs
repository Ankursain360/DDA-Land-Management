﻿using Microsoft.AspNetCore.Http;
using Service.IApplicationService;
using System;
using System.Linq;

namespace LIMSPublicInterface.Helper
{
    public class SiteContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserProfileService _userProfileService;
        
        public SiteContext(IHttpContextAccessor httpContextAccessor,
            IUserProfileService userProfileService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userProfileService = userProfileService;
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
        public int? DepartmentId
        {
            get
            {
                string userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "sub").Value;
                var user = _userProfileService.GetUserById(Convert.ToInt32(userId)).GetAwaiter().GetResult();
                return user.DepartmentId;
            }
            set => throw new NotImplementedException();
        }
        public int? BranchId
        {
            get
            {
                string userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "sub").Value;
                var user = _userProfileService.GetUserById(Convert.ToInt32(userId)).GetAwaiter().GetResult();
                return user.BranchId;
            }
            set => throw new NotImplementedException();
        }
    }
}
