
namespace SiteMaster.Helper
{
    public interface ISiteContext
    {
        int UserId { get; set; }
        int ProfileId { get; set; }
        int? RoleId { get; set; }
    }
}
