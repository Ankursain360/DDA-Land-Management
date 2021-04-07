using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Allotmentletter : AuditableEntity<int>
    
    {
        public int AllotmentId { get; set; }
        public string ReferenceNumber { get; set; }
        public int? FeeTypeId { get; set; }
        public DateTime? DemandDate { get; set; }
        public decimal DemandAmount { get; set; }
        public DateTime? DemandPeriodStart { get; set; }
        public DateTime? DemandPeriodEnd { get; set; }
        public string FilePath { get; set; }
        public byte? IsActive { get; set; }
         public Allotmententry Allotment { get; set; }
        [NotMapped]
        public List<Allotmententry> RefNoList { get; set; }
    }
}
