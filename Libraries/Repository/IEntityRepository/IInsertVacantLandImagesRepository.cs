using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;


namespace Libraries.Repository.IEntityRepository
{
    public interface IInsertVacantLandImagesRepository : IGenericRepository<Vacantlandimage>
    {
        Task<bool> SaveVacantlandlistimage(vacantlandlistimage vacantlandlistimage);
    }
}