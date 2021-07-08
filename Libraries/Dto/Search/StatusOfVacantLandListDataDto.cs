using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
   public class StatusOfVacantLandListDataDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public long TotalNoPlots { get; set; }
        public long NoPlotsPhotosUploaded { get; set; }
        public long NoPlotsPhotosNotUploaded { get; set; }
        public long NoOfPhotosUploaded { get; set; }
        


    }
}
