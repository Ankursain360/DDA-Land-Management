using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Casenature : AuditableEntity<int>
    {
        public Casenature()
        {                   }
        [Required(ErrorMessage = "Case Nature name is required")]
        [Remote(action: "Exist", controller: "CaseNature", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Status field is required")]
        public byte? IsActive { get; set; }
      
    }
}
