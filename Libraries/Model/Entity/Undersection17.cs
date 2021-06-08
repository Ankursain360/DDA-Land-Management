using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Undersection17 : AuditableEntity<int>
    {
        public Undersection17()
        {
            

            Awardmasterdetail = new HashSet<Awardmasterdetail>();
            Newlandawardmasterdetail = new HashSet<Newlandawardmasterdetail>();
        }
        [Required(ErrorMessage = "Notification 6 is mandatory", AllowEmptyStrings = false)]
        public int? UnderSection6Id { get; set; }
        [Required(ErrorMessage = "Notification 17 is mandatory")]
        public string Number { get; set; }
        public DateTime? NotificationDate { get; set; }
        public string DocumentName { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }
        [NotMapped]
        public List<Undersection6> Undersection6List { get; set; }
        public virtual Undersection6 UnderSection6 { get; set; }



      
      
       
        public ICollection<Undersection17plotdetail> Undersection17plotdetail { get; set; }
        public ICollection<Awardmasterdetail> Awardmasterdetail { get; set; }
        public ICollection<Newlandawardmasterdetail> Newlandawardmasterdetail { get; set; }
        public ICollection<Newlandpossessiondetails> Newlandpossessiondetails { get; set; }

        [NotMapped]
        public IFormFile DocumentIFormFile { get; set; }

    }
}
