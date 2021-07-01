using Dto.Common;

namespace Dto.Master
{
  public  class AllotmentletterListDto
    {
        public int Id { get; set; }
        public string LeaseType { get; set; }
        public string ReferenceNo { get; set; }
        public string AllotmentReferenceNo { get; set; }
        public string SocietyName { get; set; }
        public string AllotmentDate { get; set; }
        public string Area { get; set; }
        public string LetterGenerateDate { get; set; }
    }
}
