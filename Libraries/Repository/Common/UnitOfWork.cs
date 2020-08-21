using System.Threading.Tasks;
using Libraries.Model;

namespace Libraries.Repository.Common
{
    public class UnitOfWork : IUnitOfWork
    {
       private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
           _context = context;
        }
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // public void Dispose()
        // {
        //     Dispose(true);
        //     GC.SuppressFinalize(this);
        // }
    }
}