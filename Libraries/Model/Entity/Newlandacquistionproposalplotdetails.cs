using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Newlandacquistionproposalplotdetails : AuditableEntity<int>
    {
        

        public int? ProposaldetailsId { get; set; }
        public int? AcquiredlandvillageId { get; set; }
        public int? KhasraId { get; set; }
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }
       
        public byte? IsActive { get; set; }

        public Newlandvillage Acquiredlandvillage { get; set; }
        public Newlandkhasra Khasra { get; set; }
        public Newlandacquistionproposaldetails Proposaldetails { get; set; }
    }
}
