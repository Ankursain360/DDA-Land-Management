using Dto.Common;
namespace Dto.Master
{
   public class DoorToDoorSurveyListDto
    {
        public int Id { get; set; }
        public string LocationAddressofProperty { get; set; }
        public string MunicipalNumberifany { get; set; }
        public string GeoReferencing { get; set; }
        public string ApproxAreaoftheProperty { get; set; }
        public string NumberofFloors { get; set; }
        public string Status { get; set; }
      
    }
}
