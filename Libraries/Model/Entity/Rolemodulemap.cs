using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;

namespace Libraries.Model.Entity
{
    public class Rolemodulemap : AuditableEntity<int>
    {
        public int ModuleId { get; set; }
        public int RoleId { get; set; }
        public Module Module { get; set; }
        public  ApplicationRole Role { get; set; }
    }
}