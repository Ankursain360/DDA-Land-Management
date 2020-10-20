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
        [Required(ErrorMessage = "Module name is required")]
        public int? ModuleId { get; set; }
        [Required (ErrorMessage ="Page name is required")]
        //[Remote("IsAdvertisement_Exist", "RemotDataEx", AdditionalFields = "AdvertisementNo,AdvertisementID", ErrorMessage = "Entered Advertisement No Already exist in database. Please give unique Advertisement No.")]
        [Remote(action: "Exist", controller: "Page", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please fill Address ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please fill Display Page On Menu feild ")]
        public byte? DisplayPageOnMenu { get; set; }
        [Required]
        public int? Priority { get; set; }
        [Required(ErrorMessage = "Please fill Status feild")]
        public byte? IsActive { get; set; }

        [NotMapped]
        public List<Module> ModuleList { get; set; }
        public virtual Module Module { get; set; }

    }
}
