using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Allotmententry : AuditableEntity<int>
   
    {
        public Allotmententry()
        {
            Possesionplan = new HashSet<Possesionplan>();
        }
        [Required(ErrorMessage = "Applicant name is mandatory ")]
        public int ApplicationId { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = "AllotedArea is mandatory")]
        public decimal? AllotedArea { get; set; }
        [Required(ErrorMessage = "Allotment Date is mandatory ")]
        public DateTime AllotmentDate { get; set; }
        public string PhaseNo { get; set; }
        public string SectorNo { get; set; }
        public string PlotNo { get; set; }
        public string PocketNo { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = "PlayGroundArea is mandatory")]
        public decimal? PlayGroundArea { get; set; }
        //public decimal? BuildingArea { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }
        [NotMapped]
        public List<Allotmententry> ApplicationList { get; set; }
        [NotMapped]
        public List<Leaseapplication> LeaseappList { get; set; }
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string Address { get; set; }
        [NotMapped]
        public string ContactNo { get; set; }
        [NotMapped]
        public decimal? LandAreaSqMt { get; set; }


        [NotMapped]
        public DateTime Date { get; set; }
        [NotMapped]
        public decimal PremiumRate{ get; set; }
        [NotMapped]
        public decimal TotalPremiumAmount{ get; set; }
        public ICollection<Possesionplan> Possesionplan { get; set; }
        public Leaseapplication Application { get; set; }
    }
}
