using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IDivisionService : IEntityService<Division>
    {
        Task<List<Division>> GetAllDivision();
        Task<List<Division>> GetDivisionUsingRepo();

        Task<bool> Update(int id, Division division);
        Task<bool> Create(Division division);
        Task<Division> FetchSingleResult(int id);
        Task<bool> Delete(int id);

        Task<bool> CheckUniqueName(int id, string division);


        //Task<bool> Create();

    }
}
