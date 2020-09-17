using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class User : AuditableEntity<int>
    {
        public int? DistrictId { get; set; }
        [Required]
        [Remote(action: "ExistLoginName", controller: "UserManagement", AdditionalFields = "Id")]

        public string LoginName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }
        public string ChangePassword { get; set; }
        public string PrevPassword1 { get; set; }
        public string PrevPassword2 { get; set; }
        public string PrevPassword3 { get; set; }
        public string PrevPassword4 { get; set; }
        public string PrevPassword5 { get; set; }
        public DateTime? PasswordSetDate { get; set; }
        public string LoginStatus { get; set; }
        public string Locked { get; set; }
        public byte IsActive { get; set; }
        public int? UserHitCount { get; set; }
        public int? LoginFailCount { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public DateTime? LastLogoutDateTime { get; set; }
        public string LastActivity { get; set; }
        public string ContactNo { get; set; }
        public int? LockedCount { get; set; }
        public int AadharcardNo { get; set; }
      //  public string Password { get; set; }
        public virtual ICollection<PageRole> PageRole { get; set; }
        [NotMapped]

        [Compare("Password", ErrorMessage = "Password and confirmation password must match.")]
        public string ConfirmPassword { get; set; }

       
        [NotMapped]
        public bool defaultpassword { get; set; }

        [NotMapped]
        public bool ChangePasswordA { get; set; }

        [NotMapped]
        public bool LockedA { get; set; }


        [NotMapped]
        public string DistrictName { get; set; }
        [NotMapped]
        public List<District> DistrictList { get; set; }
        public virtual District District { get; set; }



        [NotMapped]
        public string RoleName { get; set; }
        [NotMapped]
        public List<Role> RoleList { get; set; }
        public virtual Role Role { get; set; }


    }
}
