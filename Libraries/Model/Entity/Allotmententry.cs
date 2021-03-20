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
            //Requestforproceeding = new HashSet<Requestforproceeding>();
        }
        public int ApplicationId { get; set; }
        public decimal AllotedArea { get; set; }
        public DateTime AllotmentDate { get; set; }
        public string PhaseNo { get; set; }
        public string SectorNo { get; set; }
        public string PlotNo { get; set; }
        public string PocketNo { get; set; }
        public decimal PlayGroundArea { get; set; }
        public string IsPlayground { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }

        public decimal? PremiumRate { get; set; }
        public decimal? PremiumAmount { get; set; }
        public decimal? AmountLicFee { get; set; }
        public int? NoOfYears { get; set; }
        public decimal? GroundRent { get; set; }
        public decimal? DocumentCharges { get; set; }
        public int? LeasesTypeId { get; set; }
        public int? LeasePurposesTypeId { get; set; }
        public int? LeaseSubPurposeId { get; set; }
        [NotMapped]
        public List<Allotmententry> ApplicationList { get; set; }
        [NotMapped]
        public List<Leaseapplication> LeaseappList { get; set; }
        [NotMapped]
        public List<Leasetype> LeaseTypeList { get; set; }
        [NotMapped]
        public List<Leasepurpose> LeasePurposeList { get; set; }
        [NotMapped]
        public List<Leasesubpurpose> LeaseSubPurposeList { get; set; }
        [NotMapped]
        public int PurposeId { get; set; }


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
        //[NotMapped]
        //public decimal PremiumRate { get; set; }
        [NotMapped]
        public decimal TotalPremiumAmount { get; set; }
        public ICollection<Possesionplan> Possesionplan { get; set; }
        public Leaseapplication Application { get; set; }


        public Leasepurpose LeasePurposesType { get; set; }
        public Leasesubpurpose LeaseSubPurpose { get; set; }
        public Leasetype LeasesType { get; set; }

        //public ICollection<Requestforproceeding> Requestforproceeding { get; set; }

        public ICollection<Requestforproceeding> Requestforproceeding { get; set; }

    }
}
