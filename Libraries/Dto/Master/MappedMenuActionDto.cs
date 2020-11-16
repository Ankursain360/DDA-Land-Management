
namespace Dto.Master
{
    public class MappedMenuActionDto
    {
        public int MenuId { get; set; }
        public int ActionId { get; set; }
        public string ActionName { get; set; }
        public bool? IsAvailable { get; set; }
        public byte IsActive { get; set; }
    }
}
