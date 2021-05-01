
using Dto.Common;

namespace Dto.Master
{
  public  class UpdatePlanningDetailsListDto
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public string Division { get; set; }

        public string Zone { get; set; }
        public string UnplannedProperty { get; set; }
        public string plannedProperty { get; set; }
    }
}
