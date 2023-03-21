using Dto.Common;
namespace Dto.Master
{
    public class DoorToDoorSurveyListDto
    {
        //public int Id { get; set; }
        //public string LocationAddressofProperty { get; set; }
        //public string MunicipalNumberifany { get; set; }
        //public string GeoReferencing { get; set; }
        //public string ApproxAreaoftheProperty { get; set; }
        //public string NumberofFloors { get; set; }
        //public string Status { get; set; }

        public int Id { get; set; }
        public string LocationAddressofProperty { get; set; }
        // public string MunicipalNumberifany { get; set; }
        public string lattitude { get; set; }
        public string longitude { get; set; }
        public string presentUse { get; set; }
        public string OccupantName { get; set; }
        public string ApproxAreaoftheProperty { get; set; }
        public string AreaUnit { get; set; }
        public string NumberofFloors { get; set; }
        public string FileNo { get; set; }
        public string CA_NumberOfElectricityConnection { get; set; }
        public string k_NumberOfWaterConnection { get; set; }
        public string HouseTaxNumberIssueBy_MCD { get; set; }
        public string NameOfOccupant { get; set; }
        public string email { get; set; }
        public string Mobile { get; set; }
        public string AadharNumberOfOccupant { get; set; }
        public string VoterIdNumber { get; set; }
        public string DemagePaidInThePast { get; set; }
        public string Status { get; set; }
        public string remarks { get; set; }
        public string CreatedDate { get; set; }


    }
}
