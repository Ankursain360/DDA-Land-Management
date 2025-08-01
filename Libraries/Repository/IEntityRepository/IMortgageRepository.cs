﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Repository.IEntityRepository
{
    public interface IMortgageRepository : IGenericRepository<Mortgage>
    {
        Task<PagedResult<Mortgage>> GetPagedMortgageDetails(MortgageSearchDto model);
        Task<List<Documentchecklist>> GetDocumentChecklistDetails(int servicetypeid);
        Task<bool> SaveAllotteeServiceDocuments(List<Allotteeservicesdocument> allotteeservicesdocuments);
        Task<Possesionplan> GetAllotteeDetails(int userId);
    }
}