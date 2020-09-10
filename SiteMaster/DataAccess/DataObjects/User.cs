using System;
using System.Collections.Generic;

namespace SiteMaster.DataAccess.DataObjects
{
    public partial class User
    {
        public int Id { get; set; }
        public int? DistrictId { get; set; }
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
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int AadharcardNo { get; set; }
    }
}
