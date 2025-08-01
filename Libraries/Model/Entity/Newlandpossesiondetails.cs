﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Libraries.Model.Entity
{
    public class Newlandpossessiondetails : AuditableEntity<int>
    {

        [Required(ErrorMessage = " Village name is mandatory")]
        public int? VillageId { get; set; }
        [Required(ErrorMessage = " Khasra No is mandatory")]
        public int? KhasraId { get; set; }
        [Required(ErrorMessage = " Type Of Possession is mandatory")]
        public string PossType { get; set; }
        [Required(ErrorMessage = " Reason  is mandatory")]
        public string ReasonNonPoss { get; set; }
        public string Reason { get; set; }
        public string PossessionTake { get; set; }
        //[Required(ErrorMessage = " Possesssion date  is mandatory")]
        //public DateTime PossDate { get; set; }
        //[Required(ErrorMessage = " Possesssion Khasra No  is mandatory")]
        //public int PossKhasraId { get; set; }
        //[RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        //[Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = " Bigha is mandatory")]
        
        public int Bigha { get; set; }
        //[RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        //[Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = "Biswa is mandatory")]
        public int Biswa { get; set; }
        //[RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        //[Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = "Biswanshi is mandatory")]
        public int Biswanshi { get; set; }
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Undersection-4 is mandatory")]
        public int Us4id { get; set; }
        [Required(ErrorMessage = "Undersection-6 is mandatory")]
        public int Us6id { get; set; }
        [Required(ErrorMessage = "Undersection-17 is mandatory")]
        public int Us17id { get; set; }

        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }
        public string DocumentName { get; set; }
        [NotMapped]
        public List<Newlandkhasra> KhasraList { get; set; }
        public Newlandkhasra Khasra { get; set; }
        [NotMapped]
        public List<Newlandvillage> VillageList { get; set; }
        public Newlandvillage Village { get; set; }
        [NotMapped]
        public List<Newlandkhasra> PossKhasraList { get; set; }
        //public Newlandkhasra PossKhasra { get; set; }
        [NotMapped]
        public List<Undersection17> us17List { get; set; }
        public Undersection17 Us17 { get; set; }
        [NotMapped]
        public List<Undersection4> us4List { get; set; }
        public Undersection4 Us4 { get; set; }
        [NotMapped]
        public List<Undersection6> us6List { get; set; }
        public Undersection6 Us6 { get; set; }

        [NotMapped]
        public bool IsVacant { get; set; }
        [NotMapped]
        public bool IsBuiltup { get; set; }
        [NotMapped]
        public IFormFile DocumentIFormFile { get; set; }
    }
}
