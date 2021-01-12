using Libraries.Model.Common;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Court : AuditableEntity<int>
    {

        [Required(ErrorMessage = "Court name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address field is required")]
        public string Address { get; set; }
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter a Valid 10 digit Mobile Number")]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "Status field is required")]
        public byte IsActive { get; set; }

    }
}
