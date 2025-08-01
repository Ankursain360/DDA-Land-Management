﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IDivisionService : IEntityService<Division>
    {
        Task<List<Division>> GetAllDivision();
        Task<List<Zone>> GetAllZone(int departmentId); 
        Task<List<Department>> GetAllDepartment(); 


        Task<List<Division>> GetDivisionUsingRepo();

        Task<bool> Update(int id, Division division);
        Task<bool> Create(Division division);
        Task<Division> FetchSingleResult(int id);
        Task<bool> Delete(int id);

        Task<bool> CheckUniqueName(int id, string division);


        Task<PagedResult<Division>> GetPagedDivision(DivisionSearchDto model);

    }
}
