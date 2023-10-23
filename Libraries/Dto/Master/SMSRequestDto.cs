using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
	public class SMSRequestDto
	{
		public string AuthId { get; set; }
		public string EncodedMessage { get; set; }
		public string TemplateId { get; set; }
		public string MobileNumber { get; set; }
		public string CheckSum { get; set; }
	}
}
