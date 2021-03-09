using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class Groundrent : AuditableEntity<int>
    {
        public int PropertyTypeId { get; set; }
        public decimal GroundRate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public byte? IsActive { get; set; }
       

        public PropertyType PropertyType { get; set; }
        [NotMapped]
        public List<PropertyType> PropertyTypeList { get; set; }
    }
}

