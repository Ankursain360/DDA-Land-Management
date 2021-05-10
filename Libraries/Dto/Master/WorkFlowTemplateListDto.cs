using Dto.Common;

namespace Dto.Master
{
     public  class WorkFlowTemplateListDto
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public string Module { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SLATimeLine { get; set; }
        public string IsActive { get; set; }
    }
}
