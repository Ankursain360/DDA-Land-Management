using Libraries.Model.Common;
using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Documentcategory : AuditableEntity<int>
    {
        public Documentcategory()
        {
            Dmsfileupload = new HashSet<Dmsfileupload>();
        }


        public string CategoryName { get; set; }
        public byte? IsActive { get; set; }
        public ICollection<Dmsfileupload> Dmsfileupload { get; set; }
    }
}
