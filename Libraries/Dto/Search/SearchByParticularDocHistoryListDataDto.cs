using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
  public  class SearchByParticularDocHistoryListDataDto
    {
        public int Id { get; set; }
        public string IssuedToEmployee { get; set; }
        public string IssuedDate { get; set; }
        public string IssuedBy { get; set; }
        public string ReturnedDate { get; set; }
        public string ReturnedBy { get; set; }
        public string DeptId { get; set; }
    }
}
