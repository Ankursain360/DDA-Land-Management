using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public  class Newlandnotificationdetails : AuditableEntity<int>
    {
        [Required(ErrorMessage = " Notification type is mandatory")]
        public int NotificationTypeId { get; set; }
        [Required(ErrorMessage = " Notification no is mandatory")]
        public string NotificationNo { get; set; }
        [Required(ErrorMessage = " Village name is mandatory")]
        public int VillageId { get; set; }
        [Required(ErrorMessage = " Khasra no is mandatory")]
        public int KhasraId { get; set; }
        [Required(ErrorMessage = " Bigha is mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? Bigha { get; set; }
        [Required(ErrorMessage = " Biswa is mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? Biswa { get; set; }
        [Required(ErrorMessage = " Biswanshi is mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? Biswanshi { get; set; }
        public string Remarks { get; set; }
        [Required(ErrorMessage = " Status is mandatory")]
        public byte? IsActive { get; set; }
      

        public Newlandkhasra Khasra { get; set; }
       
        public NewlandNotificationtype NotificationType { get; set; }
        public Newlandvillage Village { get; set; }

        [NotMapped]
        public List<NewlandNotificationtype> NotificationTypeList { get; set; }

       
        [NotMapped]
        public List<Newlandvillage> VillageList { get; set; }
        [NotMapped]
        public List<Newlandkhasra> KhasraList { get; set; }
    }
}
