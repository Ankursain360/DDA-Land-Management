using Microsoft.AspNetCore.Identity;

namespace IdentityServerAspNetIdentity.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string ChangePasswordStatus { get; set; }
    }
}
