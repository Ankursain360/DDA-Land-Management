using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Page : AuditableEntity<int>
    {
        [Required]
        //[Remote("IsAdvertisement_Exist", "RemotDataEx", AdditionalFields = "AdvertisementNo,AdvertisementID", ErrorMessage = "Entered Advertisement No Already exist in database. Please give unique Advertisement No.")]
        [Remote(action: "Exist", controller: "Page", AdditionalFields = "Id")]
      
        public int? Module { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public byte? DisplayPageOnMenu { get; set; }
        public int? Priority { get; set; }
        public byte? IsActive { get; set; }
       
    }
}
