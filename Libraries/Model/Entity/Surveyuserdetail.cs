using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public  class Surveyuserdetail : AuditableEntity<int>
    {
        public Surveyuserdetail()
        {
            Doortodoorsurvey = new HashSet<Doortodoorsurvey>();
        }


        [Required(ErrorMessage = "First Name is mandatory")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "User Name is mandatory")]
        [Remote(action: "Exist", controller: "Surveyuserdetail", AdditionalFields = "Id")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Phone Number is mandatory")]
        [Remote(action: "PhoneNoExist", controller: "Surveyuserdetail", AdditionalFields = "Id")]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "Email id is mandatory")]
        [Remote(action: "EmailExist", controller: "Surveyuserdetail", AdditionalFields = "Id")]
        public string EmailId { get; set; }
        public string Password { get; set; }

        [Required(ErrorMessage = "User Role is mandatory")]
        public int? RoleId { get; set; }
        public byte? IsActive { get; set; }
       

        public Surveyuserrole Role { get; set; }
        [NotMapped]
        public List<Surveyuserrole> SurveyUserRoleList { get; set; }
        public ICollection<Doortodoorsurvey> Doortodoorsurvey { get; set; }
    }
}
