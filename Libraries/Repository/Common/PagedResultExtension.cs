using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.Common
{
    public static class PagedResultExtension
    {
        public static async Task<PagedResult<T>> GetPaged<T>(this IQueryable<T> query,
                                        int page, int pageSize) where T : class
        {
            if (pageSize > 1000)
            {
                throw new ArgumentOutOfRangeException("ArgumentOutOfRangeException", new Exception("PageSize should not be more than 1000."));
            }
            if (page > 0 && pageSize > 0)
            {
                var result = new PagedResult<T>();

                result.RowCount = await query.CountAsync();
                result.CurrentPage = page;
                int skip = (page - 1) * pageSize;
                if (result.RowCount < skip)
                    skip = 0;
                result.Results = await query.AsNoTracking().Skip(skip).Take(pageSize).ToListAsync();
                result.PazeSize = pageSize;
                return result;
            }
            else
            {
                throw new ArgumentException("Page and PageSize must be non negative.");
            }
        }
    }
}