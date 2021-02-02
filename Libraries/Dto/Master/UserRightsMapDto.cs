using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
  public  class UserRightsMapDto
    {
        public int UserId { get; set; }
        public byte Downloadright { get; set; }
        public byte Viewright { get; set; }
    }
}
