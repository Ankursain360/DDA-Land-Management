using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Actions : AuditableEntity<int>
    {
        public Actions()
        {
            Menuactionrolemap = new HashSet<Menuactionrolemap>();
        }
        [Required]
        //[Remote("IsAdvertisement_Exist", "RemotDataEx", AdditionalFields = "AdvertisementNo,AdvertisementID", ErrorMessage = "Entered Advertisement No Already exist in database. Please give unique Advertisement No.")]
        [Remote(action: "Exist", controller: "Actions", AdditionalFields = "Id")]
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public byte IsActive { get; set; }
        public ICollection<Menuactionrolemap> Menuactionrolemap { get; set; }

    }
}