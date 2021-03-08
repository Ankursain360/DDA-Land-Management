using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Newlandpossessiondetails : AuditableEntity<int>
    {

        [Required]
        public int? VillageId { get; set; }
        [Required]
        public int? KhasraId { get; set; }
        [Required]
        public string PossType { get; set; }
        [Required]
        public string ReasonNonPoss { get; set; }
        [Required]
        public DateTime PossDate { get; set; }
        public int PossKhasraId { get; set; }
        [Required]
        public decimal Bigha { get; set; }
        [Required]
        public decimal Biswa { get; set; }
        [Required]
        public decimal Biswanshi { get; set; }
        public string Remarks { get; set; }
        public int Us4id { get; set; }
        public int Us6id { get; set; }
        public int Us17id { get; set; }

        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }
        [NotMapped]
        public List<Newlandkhasra> KhasraList { get; set; }
        public Newlandkhasra Khasra { get; set; }
        [NotMapped]
        public List<Newlandvillage> VillageList { get; set; }
        public Newlandvillage Village { get; set; }
        [NotMapped]
        public List<Newlandkhasra> PossKhasraList { get; set; }
        public Newlandkhasra PossKhasra { get; set; }
        [NotMapped]
        public List<Undersection17> us17List { get; set; }
        public Undersection17 Us17 { get; set; }
        [NotMapped]
        public List<Undersection4> us4List { get; set; }
        public Undersection4 Us4 { get; set; }
        [NotMapped]
        public List<Undersection6> us6List { get; set; }
        public Undersection6 Us6 { get; set; }
    }
}
