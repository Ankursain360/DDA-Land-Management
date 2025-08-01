﻿using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Row : AuditableEntity<int>
    {

        public Row()
        {
            Datastoragedetails = new HashSet<Datastoragedetails>();
        }

        [Required(ErrorMessage = "Row No is Mandatory Field")]
        public string RowNo { get; set; }
        public byte? IsActive { get; set; }
        public ICollection<Datastoragedetails> Datastoragedetails { get; set; }
    }
}
