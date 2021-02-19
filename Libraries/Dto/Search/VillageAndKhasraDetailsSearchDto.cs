using Dto.Common;

using System.ComponentModel.DataAnnotations;


namespace Dto.Search
{
  public  class VillageAndKhasraDetailsSearchDto : BaseSearchDto
    {
        [Required(ErrorMessage = "Select Khasra")]
        public int Khasraid { get; set; }
    }
}





