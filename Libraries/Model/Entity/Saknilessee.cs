using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public partial class Saknilessee : AuditableEntity<int>
    {
       
        public int SakniDetailId { get; set; }
        public string LesseeName { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public string LesseeShare { get; set; }
        public string LesseeMortgage { get; set; }
        public byte? IsActive { get; set; }
        

        public Saknidetails SakniDetail { get; set; }
    }
}
