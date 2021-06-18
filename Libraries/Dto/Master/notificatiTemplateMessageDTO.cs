using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class notificatiTemplateMessageDTO
    {
        public string ProcessName { get; set; }
        public string FromUser { get; set; }
        public DateTime Datetime { get; set; }
        public string MessageContent { get; set; }
    }
}
