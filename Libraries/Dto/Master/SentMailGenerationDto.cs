using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class SentMailGenerationDto
    {
        public string strMailSubject { get; set; }
        public string strBodyMsg { get; set; }
        public string strMailTo { get; set; }
        public string strMailCC { get; set; }
        public string strMailBCC { get; set; }
        public string strAttachPath { get; set; }
        public string defaultPswd { get; set; }
        public string fromMail { get; set; }
        public string fromMailPwd { get; set; }
        public string mailHost { get; set; }
        public int port { get; set; }
    }
}
