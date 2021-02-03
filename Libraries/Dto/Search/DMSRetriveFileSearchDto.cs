using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class DMSRetriveFileSearchDto : BaseSearchDto
    {
        public int Department { get; set; }
        public int Khasra { get; set; }
        public string FileNo { get; set; }
        public int Locality { get; set; }
        public string PropertyNo { get; set; }
        public string AlmirahNo { get; set; }
        public string Title { get; set; }
    }
}
