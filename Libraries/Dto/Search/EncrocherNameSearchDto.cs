using Dto.Common;

namespace Dto.Search
{
  public  class EncrocherNameSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string address { get; set; }
        public string fileno { get; set; }
        public string Recstate { get; set; }
    }
}
