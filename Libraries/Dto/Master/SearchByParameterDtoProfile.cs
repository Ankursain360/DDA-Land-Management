using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto.Master
{
    public class SearchByParameterDtoProfile
    {
        public int FileNo { get; set; }
        public string FileName { get; set; }
        public int LocalityId { get; set; }
        public int DeptId { get; set; }
        public int RecordRoomId { get; set; }
        public int AlmirahId { get; set; }
        public int RowId { get; set; }
        public int ColId { get; set; }
        public int BundleId { get; set; }
    }
}
