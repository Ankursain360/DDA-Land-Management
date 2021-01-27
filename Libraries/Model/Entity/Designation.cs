using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Designation : AuditableEntity<int>
    {
        public Designation()
        {
            Issuereturnfile = new HashSet<Issuereturnfile>();
        }
        [Required]
        //[Remote("IsAdvertisement_Exist", "RemotDataEx", AdditionalFields = "AdvertisementNo,AdvertisementID", ErrorMessage = "Entered Advertisement No Already exist in database. Please give unique Advertisement No.")]
        [Remote(action: "Exist", controller: "Designation", AdditionalFields="Id")]
        public string Name { get; set; }
        public byte IsActive { get; set; }
        public ICollection<Issuereturnfile> Issuereturnfile { get; set; }

    }
}
