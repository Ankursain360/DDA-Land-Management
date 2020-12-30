using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Libraries.Model.Entity
{
    public partial class Lawyer : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Lawyer Name is Mandatory Field")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lawyer Type is Mandatory Field")]
        public int Type { get; set; }

        [Required(ErrorMessage = "Court Name is Mandatory Field")]
        public int CourtId { get; set; }
        public string ChamberAddress { get; set; }
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter a Valid 10 digit Mobile Number")]

        public string CourtPhoneNo { get; set; }

        [Required(ErrorMessage = "Pan No is Mandatory Field")]
        public string PanNo { get; set; }
        public string ResidentailAddress { get; set; }

        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter a Valid 10 digit Mobile Number")]
        public string PhoneNo { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public byte IsActive { get; set; }

    }
}

