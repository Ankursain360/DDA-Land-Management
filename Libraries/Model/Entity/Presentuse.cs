using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;
namespace Libraries.Model.Entity
{
    public class Presentuse : AuditableEntity<int>
    {
        public Presentuse()
        {
            Doortodoorsurvey = new HashSet<Doortodoorsurvey>();
        }

        public string Name { get; set; }
        public byte IsActive { get; set; }

        public ICollection<Doortodoorsurvey> Doortodoorsurvey { get; set; }

        [NotMapped]
        public List<Presentuse> presentuse { get; set; }
        [NotMapped]
        public int? Presentid { get; set; }


    }
}
