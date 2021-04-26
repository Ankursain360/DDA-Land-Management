using Dto.Common;

namespace Dto.Master
{
     public  class NewlandJoinSurveyListDto
    {
        public int Id { get; set; }
        public string VillageName { get; set; }
        public string KhasraName { get; set; }
        public string Address { get; set; }       
        public string Area { get; set; }
        
        public string jointSurveyDate { get; set; }
        public string IsActive { get; set; }
    }
}
