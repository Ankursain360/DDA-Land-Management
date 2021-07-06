using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
   public class StatusOfVacantLandListDataDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int TotalNoPlots { get; set; }
        public int NoPlotsPhotosUploaded { get; set; }
        public int NoPlotsPhotosNotUploaded { get; set; }
        public int NoOfPhotosUploaded { get; set; }
        


    }
}
