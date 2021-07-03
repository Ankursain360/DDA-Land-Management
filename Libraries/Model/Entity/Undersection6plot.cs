using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Undersection6plot : AuditableEntity<int>
    {

        [Required(ErrorMessage = "Notification6 is mandatory", AllowEmptyStrings = false)]
        public int? UnderSection6Id { get; set; }
        [Required(ErrorMessage = "Village is mandatory", AllowEmptyStrings = false)]
        public int? VillageId { get; set; }
        [Required(ErrorMessage = "Khasra No field is mandatory", AllowEmptyStrings = false)]
        public int? KhasraId { get; set; }

       

        //[RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        //[Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = "Bigha field is mandatory")]
        public int Bigha { get; set; }
        //[RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        //[Range(0, 9999999999999999.99, ErrorMessage = "Invalid  Biswa; Max 18 digits")]
        [Required(ErrorMessage = "Biswa field is mandatory")]
        public int Biswa { get; set; }
        //[RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        //[Range(0, 9999999999999999.99, ErrorMessage = "Invalid  Biswanshi; Max 18 digits")]
        [Required(ErrorMessage = "Biswanshi field is mandatory")]

        public int Biswanshi { get; set; }
        [Required(ErrorMessage = "Status field is mandatory")]
        public byte IsActive { get; set; }
        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public Khasra Khasra { get; set; }

        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        public Acquiredlandvillage Village { get; set; }

        [NotMapped]
        public List<Undersection6> NotificationList { get; set; }
        public Undersection6 Undersection6 { get; set; }
    }
}
