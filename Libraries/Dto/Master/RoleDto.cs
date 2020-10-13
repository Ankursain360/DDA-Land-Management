using Dto.Common;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dto.Master
{
    public class RoleDto : AuditableDto<int>
    {
        [Required]
        [Remote(action: "Exist", controller: "Role", AdditionalFields = "Id")]
        public string Name { get; set; }
        public short IsActive { get; set; }
    }
}
