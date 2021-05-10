using Dto.Common;


namespace Dto.Master
{
    public class LawerMasterListDto
    {
        public int Id { get; set; }
        public string LawyerType { get; set; }

        public string CourtName { get; set; }

        public string LawyerName { get; set; }
        public string PhoneNo { get; set; }

        public string ChamberAddress { get; set; }

        public string CourtPhoneNo { get; set; }
        public string ValidForm { get; set; }
        public string ValidTo { get; set; }

        public string Status { get; set; }
    }
}
