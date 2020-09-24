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
        public int UserId { get; set; }
        [Required]
        public int ModuleId { get; set; }
        public int? PageId { get; set; }
        public byte RAdd { get; set; }
        public byte REdit { get; set; }
        public byte RDisplay { get; set; }
        public byte RDelete { get; set; }
        public byte RView { get; set; }
        public byte IsActive { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
        [NotMapped]
        public string OperationType { get; set; }
        [NotMapped]
        public List<int?> PageIdList { get; set; }
        [NotMapped]
        public List<string> RAddList { get; set; }
        [NotMapped]
        public List<string> RDisplayList { get; set; }
        [NotMapped] 
        public List<string> REditList { get; set; }
        [NotMapped] 
        public List<string> RDeleteList { get; set; }
        [NotMapped] 
        public List<string> RViewList { get; set; }
        [NotMapped] 
        public List<Role> RoleList { get; set; }
        [NotMapped]
        public List<Module> ModuleList { get; set; }
        [NotMapped]
        public List<User> UserList { get; set; }
        [NotMapped]
        public string ModuleName { get; set; }
        [NotMapped]
        public string PageName { get; set; }
    }
}