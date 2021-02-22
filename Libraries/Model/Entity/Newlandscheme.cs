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
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public DateTime? SchemeDate { get; set; }
        [Required]
        public string FileNo { get; set; }
        public string Description { get; set; }
        public byte IsActive { get; set; }
        public ICollection<Newlandacquistionproposaldetails> Newlandacquistionproposaldetails { get; set; }


    }
}
