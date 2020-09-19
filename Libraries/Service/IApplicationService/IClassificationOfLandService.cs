using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IClassificationOfLandService : IEntityService<Classificationofland>
    {
        Task<List<Classificationofland>> GetAllLandUse(); // To Get all data added by renu

        Task<bool> Update(int id, Classificationofland classificationofland); // To Upadte Particular data added by renu

        Task<bool> Create(Classificationofland classificationofland);

        Task<Classificationofland> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        Task<bool> CheckUniqueName(int id, string classificationofland);   // To check Unique Value  for classificationofland
    }
}
