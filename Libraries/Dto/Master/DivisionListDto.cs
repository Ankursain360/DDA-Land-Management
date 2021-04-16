using Dto.Common;
namespace Dto.Master
{
    public class DivisionListDto
    {
        public int Id { get; set; }
        public string Department { get;set;}
        public string Zone { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string Status { get; set; }
        public string IsActive { get; set; }
    }
}
