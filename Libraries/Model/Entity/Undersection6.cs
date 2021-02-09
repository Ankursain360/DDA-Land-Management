using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class Undersection6 : AuditableEntity<int>
    {
        public Undersection6()
        {
            Undersection22plotdetails = new HashSet<Undersection22plotdetails>();
        }
        public string UnderSectionNotificationNumber { get; set; }
        public DateTime? UnderSectionDate { get; set; }
        public byte? IsActive { get; set; }
        public ICollection<Undersection22plotdetails> Undersection22plotdetails { get; set; }
    }
}