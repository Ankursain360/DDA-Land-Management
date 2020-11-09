using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;

namespace Libraries.Model.Entity
{
    public class Menuactionrolemap : AuditableEntity<int>
    {
        public int SubMenuId { get; set; }
        public int ActionId { get; set; }
        public int RoleId { get; set; }
        public Actions Action { get; set; }
        public ApplicationRole Role { get; set; }
        public Page SubMenu { get; set; }
    }
}