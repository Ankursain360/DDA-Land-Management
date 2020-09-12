using System;
using System.Text;
using Libraries.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class PageRole:AuditableEntity<int>
    {
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ModuleId { get; set; }
        public int? PageId { get; set; }
        public byte RAdd { get; set; }
        public byte REdit { get; set; }
        public byte RDelete { get; set; }
        public byte RView { get; set; }
        public byte IsActive { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
        [NotMapped]
        public List<int?> PageIdList { get; set; }
        [NotMapped]
        public List<byte> RAddList { get; set; }
        [NotMapped]
        public List<byte> REditList { get; set; }
        [NotMapped]
        public List<byte> RDeleteList { get; set; }
        [NotMapped]
        public List<byte> RViewList { get; set; }
        [NotMapped]
        public List<Role> RoleList { get; set; }
        [NotMapped]
        public List<Module> ModuleList { get; set; }
        [NotMapped]
        public List<User> UserList { get; set; }
   }
}