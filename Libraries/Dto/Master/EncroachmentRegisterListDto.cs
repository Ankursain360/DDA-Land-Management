using Dto.Common;

namespace Dto.Master
{
   public class EncroachmentRegisterListDto
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Loaclity { get; set; }
        public string KhasraNo { get; set; }
        public string PrimaryListNo { get; set; }
        public string Encroachment { get; set; }
        public string StatusOfLand { get; set; }
        public string Status { get; set; }

        public string Department { get; set; }
        public string Zone { get; set; }
        public string Division { get; set; }
        public string Area { get; set; }
        public string PoliceStation { get; set; }

        public string OfficerOnDuty { get; set; }
        public string Remarks { get; set; }


    }
}

