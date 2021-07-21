using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
  public  class SendOtpGetDetailsListDto : BaseSearchDto
    {
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}
