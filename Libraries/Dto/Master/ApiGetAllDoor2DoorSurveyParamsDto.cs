using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class ApiGetAllDoor2DoorSurveyParamsDto
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string OccupantName { get; set; }
        public string OccupantContactNo { get; set; }
        public string PropertyAddress { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

    }
}
