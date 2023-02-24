using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class vacantlandlistimage: AuditableEntity<int>
    {
        public string ImagePath { get; set; }
        public int vacantlandimageId { get; set; }
        public Vacantlandimage vacantlandimage { get; set; }
    }
}
