using Dto.Common;
using System;

namespace Dto.Search
{
   public class InterestIndexDataDetails
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }        
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Percentage { get; set; }
        public System.SByte IsActive { get; set; }
        public string PropertyName { get; set; }
    }
}