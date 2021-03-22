using Dto.Common;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dto.Master
{
   public class UserBindDropdownDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
