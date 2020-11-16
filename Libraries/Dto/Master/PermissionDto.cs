using System.Collections.Generic;

namespace Dto.Master
{
    public class PermissionDto
    {
        public PermissionDto()
        {
            Actions = new List<MappedMenuActionDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public IList<PermissionDto> Children { get; set; }
        public List<MappedMenuActionDto> Actions { get; set; }
    }
}
