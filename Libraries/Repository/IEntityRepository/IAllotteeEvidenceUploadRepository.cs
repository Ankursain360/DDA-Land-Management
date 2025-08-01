﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Repository.IEntityRepository
{
    public interface IAllotteeEvidenceUploadRepository : IGenericRepository<Allotteeevidenceupload>
    {
        Task<PagedResult<Requestforproceeding>> GetPagedRequestLetterDetails(AllotteeEvidenceSearchDto model);
        Task<List<Allotteeevidenceupload>> GetAllotteeEvidenceHistoryDetails(int id);
        Task<Allotteeevidenceupload> FetchAllotteeEvidenceUploadDetails(int id);
        Task<List<Requestforproceeding>> GetAllotteeEvidenceDetails();
    }
}