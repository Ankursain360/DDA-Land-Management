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
            Datastoragepartfilenodetails = new HashSet<Datastoragepartfilenodetails>();
            Proposaldetails = new HashSet<Proposaldetails>();
        }

     
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? SchemeDate { get; set; }
        public string FileNo { get; set; }
        public string Description { get; set; }
        public byte IsActive { get; set; }
       

        public ICollection<Datastoragepartfilenodetails> Datastoragepartfilenodetails { get; set; }
        public ICollection<Proposaldetails> Proposaldetails { get; set; }
    }
}
