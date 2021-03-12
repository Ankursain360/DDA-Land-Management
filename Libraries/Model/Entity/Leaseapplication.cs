using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Leaseapplication : AuditableEntity<int>
    {
        public Leaseapplication()
        {
            Leaseapplicationdocuments = new HashSet<Leaseapplicationdocuments>();
        }
        public string RefNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string RegistrationNo { get; set; }
        public string Description { get; set; }
        public string LandPurpose { get; set; }
        public string LandDetailsArea { get; set; }
        public string IncomeTaxDescription { get; set; }
        public DateTime? SponsorshipDate { get; set; }
        public string SponsorshipDescription { get; set; }
        public string RecommendationDescription { get; set; }
        public decimal? LandAreaSqMt { get; set; }
        public string NotarizedUndertakingDescription { get; set; }
        public string IndemnityDescription { get; set; }
        public string Location1 { get; set; }
        public string Location2 { get; set; }
        public string Location3 { get; set; }
        public string LandAuthorisingDescription { get; set; }
        public string FinancialPositionDescription { get; set; }
        public string ProposedDescription { get; set; }
        public string EstablishmentNameAddress { get; set; }
        public string FunctioningSinceWhen { get; set; }
        public string FunctioningActivityUndertaken { get; set; }
        public decimal? FunctioningAreaSqMt { get; set; }
        public string RefNoOfAllotmentLetterDate { get; set; }
        public decimal? AreaSqlMt { get; set; }
        public string Locality { get; set; }
        public string Purpose { get; set; }
        public decimal? Rate { get; set; }
        public byte IsActive { get; set; }
        public int? ApprovedSataus { get; set; }
        public int? PendingAt { get; set; }
        public ICollection<Leaseapplicationdocuments> Leaseapplicationdocuments { get; set; }
    }
}
