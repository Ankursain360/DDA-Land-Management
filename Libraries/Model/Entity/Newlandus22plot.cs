using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public  class Newlandus22plot : AuditableEntity<int>
    {
        [Required(ErrorMessage = " Notification no is mandatory")]
        public int NotificationId { get; set; }
        [Required(ErrorMessage = " Village name is mandatory")]
        public int VillageId { get; set; }
        [Required(ErrorMessage = " Khasra no is mandatory")]
        public int KhasraId { get; set; }
        [Required(ErrorMessage = " Bigha is mandatory")]
        //[RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        //[Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public int? Bigha { get; set; }
        [Required(ErrorMessage = " Biswa is mandatory")]
        //[RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        //[Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]

        public int? Biswa { get; set; }
        [Required(ErrorMessage = " Biswanshi is mandatory")]
        //[RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        //[Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]

        public int? Biswanshi { get; set; }
        [NotMapped]
        //[Required(ErrorMessage = " Bigha is mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? ABigha { get; set; }
        [NotMapped]
        //[Required(ErrorMessage = " Biswa is mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? ABiswa { get; set; }
        [NotMapped]
        //[Required(ErrorMessage = " Biswanshi is mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? ABiswanshi { get; set; }
        public int? Us4Id { get; set; }
        public int? Us6Id { get; set; }
        public int? Us17Id { get; set; }
        public string Remarks { get; set; }
        [Required(ErrorMessage = " Status is mandatory")]
        public byte? IsActive { get; set; }

        [NotMapped]
        public List<Newlandnotification> NotificationList { get; set; }
        [NotMapped]
        public List<Newlandvillage> VillageList { get; set; }
        [NotMapped]
        public List<Newlandkhasra> KhasraList { get; set; }

        [NotMapped]
        public List<Newlandus4plot> Us4List { get; set; }

        [NotMapped]
        public List<Newlandus6plot> Us6List { get; set; }

        [NotMapped]
        public List<Newlandus17plot> Us17List { get; set; }
        //[NotMapped]
        //public List<LandNotification> Name { get; set; }

        public Newlandkhasra Khasra { get; set; }
        public Newlandnotification Notification { get; set; }
        public Newlandus17plot Us17 { get; set; }
        public Newlandus4plot Us4 { get; set; }
        public Newlandus6plot Us6 { get; set; }
        public Newlandvillage Village { get; set; }
    }
}
