using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface ILandUseService : IEntityService<Landuse>
    {
        Task<List<Landuse>> GetAllLandUse(); // To Get all data added by renu

        Task<bool> Update(int id, Landuse landuse); // To Upadte Particular data added by renu

        Task<bool> Create(Landuse landuse);

        Task<Landuse> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        Task<bool> CheckUniqueName(int id, string landuse);   // To check Unique Value  for landuse
    }
}
