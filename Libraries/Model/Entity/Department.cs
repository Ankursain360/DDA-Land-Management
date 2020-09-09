using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Department : AuditableEntity<int>
    {
        [Required]
        //[Remote("IsAdvertisement_Exist", "RemotDataEx", AdditionalFields = "AdvertisementNo,AdvertisementID", ErrorMessage = "Entered Advertisement No Already exist in database. Please give unique Advertisement No.")]
        [Remote(action: "Exist", controller: "Department", AdditionalFields = "Id")]
        
        public string Name { get; set; }
        public byte? IsActive { get; set; }

        public virtual ICollection<Zone> Zone { get; set; }
    }
}
