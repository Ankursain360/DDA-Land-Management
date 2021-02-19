using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public class Saknikhasra : AuditableEntity<int>
    {
       
        public int SakniDetailId { get; set; }
        public int KhasraId { get; set; }
        public string PlotNo { get; set; }
        public decimal? AreaSqYard { get; set; }
        public string Category { get; set; }
        public decimal? LeaseAmount { get; set; }
        public DateTime? RenewalDate { get; set; }
        public byte? IsActive { get; set; }
        public Khasra Khasra { get; set; }
        public Saknidetails SakniDetail { get; set; }
    }
}
