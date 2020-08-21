using System.Threading.Tasks;

namespace Libraries.Repository.Common
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}