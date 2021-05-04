using Dto.Common;

namespace Dto.Master
{
  public  class DemandLetterListDto
    {
        public int Id { get; set; }
        public string Locality { get; set; }
        public string DemandNo { get; set; }
        public string DueAmount { get; set; }
        public string ReliefAmount { get; set; }
        public string FileNo { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}
