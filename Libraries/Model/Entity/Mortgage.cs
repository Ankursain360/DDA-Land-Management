using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Mortgage : AuditableEntity<int>
    {
        public int ServiceTypeId { get; set; }
        public int AllottmentId { get; set; }
        public int LeaseApplicationId { get; set; }
        public DateTime? LeaseDeedDate { get; set; }
        public DateTime? MortgageDate { get; set; }
        public string Remarks { get; set; }
        public int? UserId { get; set; }
        public byte? IsActive { get; set; }
        public int? ApprovalStatus { get; set; }
        public string PendingAt { get; set; }

        public Allotmententry Allottment { get; set; }
        public Leaseapplication LeaseApplication { get; set; }
        public Servicetype ServiceType { get; set; }
    }
}
