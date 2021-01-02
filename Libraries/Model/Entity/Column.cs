using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Libraries.Model.Entity
{
    public class Column : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Column No is Mandatory Field")]
        public string ColumnNo { get; set; }

        public byte? IsActive { get; set; }
    }
}
