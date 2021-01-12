using Libraries.Model.Common;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Caseyear : AuditableEntity<int>
    {
        [Required(ErrorMessage = "CaseYear is required")]

        public string Name { get; set; }
       
        public byte IsActive { get; set; }

    }
}
