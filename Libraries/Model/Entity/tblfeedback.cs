using Libraries.Model.Common;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class tblfeedback : AuditableEntity<int>
    {
        [Required (ErrorMessage ="Name is mandatory")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is mandatory")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is mandatory")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(com|in|gov\.in|nic\.in)$",ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Feedback is mandatory")]
        public string Feedback { get; set; } = string.Empty;

        public byte IsActive { get; set; }

    }
}
