using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Newlandscheme : AuditableEntity<int>
    {
        public Newlandscheme()
        {
            Newlandacquistionproposaldetails = new HashSet<Newlandacquistionproposaldetails>();
        }
        [Required(ErrorMessage = " Scheme is mandatory")]
        public string Name { get; set; }
        [Required(ErrorMessage = " Code name is mandatory")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Scheme Date is mandatory")]
        public DateTime? SchemeDate { get; set; }
        [Required(ErrorMessage = " File No is mandatory")]
        public string FileNo { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }
        public ICollection<Newlandacquistionproposaldetails> Newlandacquistionproposaldetails { get; set; }


    }
}
