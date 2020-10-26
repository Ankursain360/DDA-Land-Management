using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;

namespace Libraries.Model.Entity
{
    public partial class Fixingdemolition : AuditableEntity<int>
    {


     
        public int EncroachmentId { get; set; }
        public byte IsActive { get; set; }
      

    }
}
