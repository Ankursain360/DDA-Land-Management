
namespace LeaseForPublic.Helper
{
    public interface ISiteContext
    {
        int UserId { get; set; }
        int ProfileId { get; set; }
        int? RoleId { get; set; }
        int? DepartmentId { get; set; }
        int? BranchId { get; set; }
        int? ZoneId { get; set; }


    }
}
