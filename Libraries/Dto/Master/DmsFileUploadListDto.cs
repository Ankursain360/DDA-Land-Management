using Dto.Common;

namespace Dto.Master
{
  public  class DmsFileUploadListDto
    {
        public int Id { get; set; }
        public string FileNo { get; set; }
       // public string AlloteeName { get; set; }
        public string Department { get; set; }
        // public string Locality { get; set; }
        //public string KhasraNo { get; set; }
        //public string Zone { get; set; }
        //public string Village { get; set; }
        public string Category { get; set; }
        public string Title_Subject { get; set; }
        
        public string Status { get; set; }
    }
}
