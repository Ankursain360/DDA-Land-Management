using Dto.Common;

namespace Dto.Master
{
  public  class DocumentChecklistListDto
    {
        public int Id { get; set; }

      

        public string ServiceType { get; set; }

        public string DocumentName { get; set; }
        public string IsMandatory { get; set; }

        public string Status { get; set; }
    }
}
