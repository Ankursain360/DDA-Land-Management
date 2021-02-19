using Dto.Common;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dto.Master
{
    public class VillageDto : AuditableDto<int>
    {
        public string Name { get; set; }
    }
}
