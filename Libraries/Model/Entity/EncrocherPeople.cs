using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class EncrocherPeople : AuditableEntity<int>
    {

        [Required(ErrorMessage = " Encroacher Name is mandatory")]
        [Remote(action: "Exist", controller: "Actions", AdditionalFields = "Id")]
        public int EnchId { get; set; }
       
        public string FileNo { get; set; }
       
        public byte RecState { get; set; }
        public byte? IsActive { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
           

        public Enchroachment Enchroachment { get; set; }
    }
}
