using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class RegisterationBodyDTO
    {
        public string displayName { get; set; }
        public string loginName { get; set; }
        public string password { get; set; }
        public string link { get; set; }
        public string action { get; set; }
        public string path { get; set; }
    }
}
