using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Disposallandtype : AuditableEntity<int>

    {
        [Required(ErrorMessage = "The New Land mandatory")]
        //[Remote("IsAdvertisement_Exist", "RemotDataEx", AdditionalFields = "AdvertisementNo,AdvertisementID", ErrorMessage = "Entered Advertisement No Already exist in database. Please give unique Advertisement No.")]
        [Remote(action: "Exist", controller: "Disposallandtype", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required]
        public string Remarks { get; set; }

        [Required(ErrorMessage = "Status is mandatory")]

        

        public byte? IsActive { get; set; }
        [Required]
        public string LandCode { get; set; }
        [Required]

        public string RecState { get; set; }

    }
}
