using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Licencefees : AuditableEntity<int>
    {   
        public int PropertyTypeId { get; set; }
        public decimal LicenceFees1 { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public byte? IsActive { get; set; }
        

        //public Propertytype PropertyType { get; set; }
    }
}
