using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public partial class Scheme : AuditableEntity<int>
    {
        public Scheme()
        {
           
            Proposaldetails = new HashSet<Proposaldetails>();
        }

        [Required (ErrorMessage = "Name is Mandatory")]
        public string Name { get; set; }
        [Required (ErrorMessage = "Code is Mandatory")]
        public string Code { get; set; }
        [Required (ErrorMessage = "Scheme Date is Mandatory")]
        public DateTime? SchemeDate { get; set; }
        [Required(ErrorMessage = "File No is Mandatory")]
        public string FileNo { get; set; }
        public string Description { get; set; }
        public byte IsActive { get; set; }


       
        public ICollection<Proposaldetails> Proposaldetails { get; set; }
    }
}
