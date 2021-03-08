using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Undersection6 : AuditableEntity<int>
    {
        public Undersection6()
        {
            Undersection22plotdetails = new HashSet<Undersection22plotdetails>();
            Awardmasterdetail = new HashSet<Awardmasterdetail>();
            Newlandawardmasterdetail = new HashSet<Newlandawardmasterdetail>();
        }
        [Required(ErrorMessage = "The Notification Number is mandatory")]
        public string Number { get; set; }
        [Required(ErrorMessage = "Notification Date is mandatory")]
        public DateTime? Ndate { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }
    

        [Required(ErrorMessage ="Notification field is mandatory")]
        public int? Undersection4Id { get; set; }
        [NotMapped]
        public List<Undersection4> NotificationList { get; set; }
        public Undersection4 Undersection4 { get; set; }
        public ICollection<Undersection22plotdetails> Undersection22plotdetails { get; set; }
        public ICollection<Undersection17> Undersection17 { get; set; }
        public ICollection<Undersection6plot> Undersection6plot { get; set; }
        public ICollection<Awardmasterdetail> Awardmasterdetail { get; set; }
        public ICollection<Newlandawardmasterdetail> Newlandawardmasterdetail { get; set; }
        public ICollection<Newlandpossessiondetails> NewlandPossessiondetails { get; set; }
    }
}