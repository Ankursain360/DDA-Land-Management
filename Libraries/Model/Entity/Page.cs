using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Page : AuditableEntity<int>
    {
        [Required]
        public int? ModuleId { get; set; }
        [Required]
        //[Remote("IsAdvertisement_Exist", "RemotDataEx", AdditionalFields = "AdvertisementNo,AdvertisementID", ErrorMessage = "Entered Advertisement No Already exist in database. Please give unique Advertisement No.")]
        [Remote(action: "Exist", controller: "Page", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public byte? DisplayPageOnMenu { get; set; }
        [Required]
        public int? Priority { get; set; }
        [Required]
        public byte? IsActive { get; set; }

        [NotMapped]
        public List<Module> ModuleList { get; set; }
        public virtual Module Module { get; set; }

    }
}
