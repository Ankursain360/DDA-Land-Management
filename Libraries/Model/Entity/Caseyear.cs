using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Caseyear : AuditableEntity<int>
    {
        public Caseyear()
        { }
        [Required(ErrorMessage = "Case Year is mandatory")]
        [Remote(action: "Exist", controller: "caseyear", AdditionalFields = "Id")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }

    }
}
