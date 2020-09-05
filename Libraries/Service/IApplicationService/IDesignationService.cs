using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IDesignationService : IEntityService<Designation>
    {
        Task<List<Designation>> GetAllDesignation();
        Task<List<Designation>> GetDesignationUsingRepo();

        Task<bool> Update(int id, Designation designation);
    }
    //class interface IDesignationService : IEntityService<TblMasterDesignation>
    //{
    //    Task<List<TblMasterDesignation>> GetAllDesignation();
    //    Task<List<TblMasterDesignation>> GetDesignationUsingRepo();
    //}

}
