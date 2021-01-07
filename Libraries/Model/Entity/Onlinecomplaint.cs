using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Onlinecomplaint : AuditableEntity<int>
    {

      
        public string Name { get; set; }
        public string Contact { get; set; }
        public int? ComplaintTypeId { get; set; }
        public string AddressOfComplaint { get; set; }
        public int? LocationId { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public byte? IsActive { get; set; }
      
        public string PhotoPath { get; set; }
        public string Email { get; set; }
        public string ReferenceNo { get; set; }
      


        [NotMapped]
        public List<ComplaintType> ComplaintList { get; set; }
        public virtual ComplaintType ComplaintType { get; set; }



        [NotMapped]
        public List<Location> LocationList { get; set; }
        public virtual Location Location { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
