using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto.Master
{
    public class DMSRetriveFileReportDtoProfile
    {
        public int DepartmentId { get; set; }

        public int LocalityId { get; set; }

        public int KhasraNoId { get; set; }
        public string FileNo { get; set; }
        public string PropertyNo { get; set; }
        public string AlmirahNo { get; set; }
        public string Title { get; set; }
    }
}
