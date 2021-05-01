
using Dto.Common;

namespace Dto.Master
{
    public class DisposallandListDto
    {
       
        public int Id { get; set; }
        public string VillageName { get; set; }
        public string KhasraNo { get; set; }

        public decimal AreaDisposed { get; set; }
        public string DateofDisposed { get; set; }

        public string Transferto { get; set; }

        public string TransferBy { get; set; }
        public string FileNoRefNo { get; set; }

        public string Status { get; set; }
    }
}
