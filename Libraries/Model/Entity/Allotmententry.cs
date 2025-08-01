﻿using System;
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
            Allotmentletter = new HashSet<Allotmentletter>();
            Cancellationentry = new HashSet<Cancellationentry>();
            Leasedeed = new HashSet<Leasedeed>();
            Possesionplan = new HashSet<Possesionplan>();
            Requestforproceeding = new HashSet<Requestforproceeding>();
            Leasepaymentdetails = new HashSet<Leasepaymentdetails>();
            Mortgage = new HashSet<Mortgage>();
            Extensionservice = new HashSet<Extension>();
            Payment = new HashSet<Payment>();
            Restorationentry = new HashSet<Restorationentry>();
        }
        [Required(ErrorMessage = "Society Name is Mandatory")]
        public int ApplicationId { get; set; }
        [Required(ErrorMessage = "Allotted Area is Mandatory")]
        public decimal TotalArea { get; set; }
        [Required(ErrorMessage = "Allotment Date is Mandatory")]
        public DateTime AllotmentDate { get; set; }
        public string PhaseNo { get; set; }
        public string SectorNo { get; set; }
        public string PlotNo { get; set; }
        public string PocketNo { get; set; }
        public decimal PlayGroundArea { get; set; }
        public decimal? BuildingArea { get; set; }

        public string Remarks { get; set; }
        public byte IsActive { get; set; }

        public decimal? PremiumRate { get; set; }
        public decimal? PremiumAmount { get; set; }
        public decimal? AmountLicFee { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? LicenceFees { get; set; }
        public decimal? AmountGroundRate { get; set; }
        public int? NoOfYears { get; set; }
        public decimal? GroundRate { get; set; }
        public decimal? DocumentCharge { get; set; }
        [Required(ErrorMessage = "Lease Type is Mandatory")]
        public int? LeasesTypeId { get; set; }
        [Required(ErrorMessage = "Purpose is Mandatory")]
        public int? LeasePurposesTypeId { get; set; }
        [Required(ErrorMessage = "SubPurpose is Mandatory")]
        public int? LeaseSubPurposeId { get; set; }
        public string OldNewEntry { get; set; }
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
        public DateTime? PossessionTakenDate { get; set; }

        [NotMapped]
        public DateTime Date { get; set; }
        public ICollection<Cancellationentry> Cancellationentry { get; set; }
        public ICollection<Restorationentry> Restorationentry { get; set; }
        public ICollection<Possesionplan> Possesionplan { get; set; }
        public Leaseapplication Application { get; set; }

        public Leasepurpose LeasePurposesType { get; set; }
        public Leasesubpurpose LeaseSubPurpose { get; set; }
        public Leasetype LeasesType { get; set; }


        public ICollection<Requestforproceeding> Requestforproceeding { get; set; }
        public ICollection<Leasepaymentdetails> Leasepaymentdetails { get; set; }

        public ICollection<Leasedeed> Leasedeed { get; set; }
        public ICollection<Mortgage> Mortgage { get; set; }
        public ICollection<Extension> Extensionservice { get; set; }
        public ICollection<Allotmentletter> Allotmentletter { get; set; }

        public ICollection<Payment> Payment { get; set; }




        //[NotMapped]
        //public string FullName
        //{
        //    get
        //    {
        //        return Application.RefNo + " (" + Application.Name + ")";
        //    }
        //}


        //[NotMapped]
        //public string LRefNo
        //{
        //    get
        //    {
        //        return Application.RefNo ;
        //    }
        //}

        //[NotMapped]
        //public string PurposeName
        //{
        //    get
        //    {
        //        return LeasePurposesType.PurposeUse ;
        //    }
        //}


        //[NotMapped]
        //public string LRefNo
        //{
        //    get
        //    {
        //        return Application.RefNo;
        //    }
        //}

        //[NotMapped]
        //public string SocietyName
        //{
        //    get
        //    {
        //        return Application.Name;
        //    }
        //}

        //[NotMapped]

        //public List<string> RefNo { get; set; }

    }
}
