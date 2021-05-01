
using Dto.Common;

namespace Dto.Master
{
    public class IssuereturnfileListDto
    {
        public int Id { get; set; }
        public string FileNo { get; set; }
        public string FileName { get; set; }
        public string RecordRoomNo { get; set; }
        public string AlNoCompactorNo { get; set; }

        public string RowNo { get; set; }
        public string ColNo { get; set; }
        public string BnNo { get; set; }

        public string Status { get; set; }
    }
}
