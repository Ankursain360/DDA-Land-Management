﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IClassificationOfLandService : IEntityService<Classificationofland>
    {
        Task<List<Classificationofland>> GetAllClassificationOfLand(); // To Get all data added by renu

        Task<bool> Update(int id, Classificationofland classificationofland); // To Upadte Particular data added by renu

        Task<bool> Create(Classificationofland classificationofland);

        Task<Classificationofland> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        Task<bool> CheckUniqueName(int id, string classificationofland);   // To check Unique Value  for classificationofland
        Task<PagedResult<Classificationofland>> GetPagedClassificationOfLand(ClassificationOfLandSearchDto model);
    }
}
