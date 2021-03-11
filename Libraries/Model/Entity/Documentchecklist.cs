using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Documentchecklist : AuditableEntity<int>
    {

        [Required(ErrorMessage = "Service Type is Mandatory")]
        public int ServiceTypeId { get; set; }

        [Required(ErrorMessage = "Name is Mandatory")]
        [Remote(action: "ExistName", controller: "DocumentChecklist", AdditionalFields = "Id,ServiceTypeId")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "IsMandatory is Mandatory")]
        public int IsMandatory { get; set; }
        public byte IsActive { get; set; }

        public Servicetype ServiceType { get; set; }

        [NotMapped]
        public List<Servicetype> ServiceTypeList { get; set; }
    }
}
