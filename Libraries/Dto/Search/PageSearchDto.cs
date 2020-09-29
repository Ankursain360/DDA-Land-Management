using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace Dto.Search
{
   
  public class PageSearchDto : BaseSearchDto
    {
        public string name { get; set; }
      
    }
}
