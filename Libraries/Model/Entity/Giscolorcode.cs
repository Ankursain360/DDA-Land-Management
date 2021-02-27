using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Giscolorcode : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Color { get; set; }
        public byte? IsActive { get; set; }
    }
}
