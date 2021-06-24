using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class DMSCSVTableDTO
    {
        public string FileNo { get; set; }
        public string AlloteeName { get; set; }
        public int? LocalityId { get; set; }
        public int? KhasraNoId { get; set; }
        public int? VillageId { get; set; }
        public int? ZoneId { get; set; }
        public string PropertyNoAddress { get; set; }
        public string Title { get; set; }
        public string AlmirahNo { get; set; }
        public string FileName { get; set; }
    }
}
