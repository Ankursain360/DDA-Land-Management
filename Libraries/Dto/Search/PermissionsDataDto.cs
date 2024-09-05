using Dto.Common;
using Dto.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class PermissionsDataDto: BaseSearchDto
    {
        public PermissionsDataDto()
        {
            Actions = new List<MappedMenuActionDtoList>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public IList<PermissionsDataDto> Children { get; set; }
        public List<MappedMenuActionDtoList> Actions { get; set; }
    }
}
