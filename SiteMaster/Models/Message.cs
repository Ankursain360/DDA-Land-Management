using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMaster.Models
{
    public class Message
    {
        public string Msg { get; set; }
        public string Status { get; set; }
        public string SamePageAction { get; set; }
        public string SamePageController { get; set; }
        public string BackPageAction { get; set; }
        public string BackPageController { get; set; }

    }
}
