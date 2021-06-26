using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class ApiSurveyUserDetailsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }

        public int? RoleId { get; set; }

        public byte? IsActive { get; set; }

        public string RoleName { get; set; }
    }

    public class ApiSurveyUserDetailsResponseDetails
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ApiSurveyUserDetailsDto> ApiSurveyUserDetailsDto { get; set; }
    }
}
