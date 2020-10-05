using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
namespace Libraries.Repository.IEntityRepository
{
    public interface IJointsurveyRepository : IGenericRepository<Jointsurvey>
    {
        Task<List<Jointsurvey>> GetAllJointSurvey();
       
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> BindKhasra();
        Task<PagedResult<Jointsurvey>> GetPagedJointsurvey(JointSurveySearchDto model);

    }
}
