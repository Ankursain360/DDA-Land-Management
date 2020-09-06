using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IDesignationService : IEntityService<Designation>
    {
        Task<List<Designation>> GetAllDesignation(); // To Get all data added by renu
        Task<List<Designation>> GetDesignationUsingRepo();

        Task<bool> Update(int id, Designation designation); // To Upadte Particular data added by renu

        Task<bool> Create(Designation designation);

        Task<Designation> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        bool CheckUniqueName(int id, Designation designation);   // To check Unique Value  for designation
    }
}
