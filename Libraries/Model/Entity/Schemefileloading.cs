using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{ 
    public class Schemefileloading : AuditableEntity<int>
{

    public Schemefileloading()
    {
        Datastoragedetails = new HashSet<Datastoragedetails>();
    }



        [Required(ErrorMessage = "Scheme name is Mandatory")]
        [Remote(action: "Exist", controller: "Schemefileloading", AdditionalFields = "Id")]
        public string SchemeName { get; set; }
        [Required(ErrorMessage = "Scheme code is Mandatory ")]
        public string SchemeCode { get; set; }


        public byte? IsActive { get; set; }

    public ICollection<Datastoragedetails> Datastoragedetails { get; set; }
}
}

