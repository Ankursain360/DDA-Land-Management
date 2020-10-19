using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
   public class InterestIndexDataDetails : BaseSearchDto
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }        
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Percentage { get; set; }
        public byte IsActive { get; set; }
        public string PropertyName { get; set; }

    }
}