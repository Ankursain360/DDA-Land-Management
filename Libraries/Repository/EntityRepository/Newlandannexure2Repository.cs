using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Libraries.Repository.EntityRepository
{
    public class Newlandannexure2Repository : GenericRepository<Newlandannexure2>, INewlandannexure2Repository
    {
        public Newlandannexure2Repository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Newlandannexure2>> GetPagedNewlandannexure2(Newlandannexure1SearchDto model)
        {
            var data = await _dbContext.Newlandannexure2
                             .GetPaged<Newlandannexure2>(model.PageNumber, model.PageSize);

            return data;
        }
        public string GetS7Download(int id)
        {
            var File = (from f in _dbContext.Newlandannexure2
                        where f.Id == id
                        select f.Sn7File).First();

            return File;
        }
        public string GetS8Download(int id)
        {
            var File = (from f in _dbContext.Newlandannexure2
                        where f.Id == id
                        select f.Sn8filePath).First();

            return File;
        }
        public string GetS9Download(int id)
        {
            var File = (from f in _dbContext.Newlandannexure2
                        where f.Id == id
                        select f.Sn9filePath).First();

            return File;
        }
        public string GetS12Download(int id)
        {
            var File = (from f in _dbContext.Newlandannexure2
                        where f.Id == id
                        select f.Sn12filePath).First();

            return File;
        }
    }
}
