using Libraries.Model.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Model.Entity
{
    public partial class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole()
        {
            Userprofile = new HashSet<Userprofile>();
            Rolemodulemap = new HashSet<Rolemodulemap>();
            Menuactionrolemap = new HashSet<Menuactionrolemap>();
        }
        public short IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<Userprofile> Userprofile { get; set; }
        public virtual ICollection<Rolemodulemap> Rolemodulemap { get; set; }
        public virtual ICollection<Menuactionrolemap> Menuactionrolemap { get; set; }
    }
}
