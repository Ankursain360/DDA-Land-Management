using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Libraries.Model.Entity
{
    public class RefreshToken
    {
        //public int Id { get; set; }
        //public string UserId { get; set; }
        //public string Token { get; set; }
        //public string JwtId { get; set; }
        //public bool IsUsed { get; set; }
        //public bool IsRevorked { get; set; }
        //public DateTime AddedDate { get; set; }
        //public DateTime ExpiryDate { get; set; }

        //[ForeignKey(nameof(UserId))]
        //public IdentityUser User {get;set;}


        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevorked { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}