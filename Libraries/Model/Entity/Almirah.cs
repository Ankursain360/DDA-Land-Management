using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Almirah : AuditableEntity<int>
    {
        public Almirah()
        {
            Datastoragedetails = new HashSet<Datastoragedetails>();
        }

        [Required(ErrorMessage = "Almirah No is Mandatory Field")]
        public string AlmirahNo { get; set; }        
        public byte? IsActive { get; set; }
        public ICollection<Datastoragedetails> Datastoragedetails { get; set; }
    }
}
