using Dto.Common;

namespace Dto.Master
{
    public class NewLandSchemeListDto
    {

        public int Id { get; set; }
        public string SchemeName { get; set; }
        public string SchemeCode { get; set; }       
        public string SchemeDate { get; set; }
        public string SchemeFileNo { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
