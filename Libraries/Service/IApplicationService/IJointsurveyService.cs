using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IJointsurveyService
    {
        Task<List<Jointsurvey>> GetAllJointSurvey();
      
        Task<List<Khasra>> BindKhasra();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Jointsurvey>> GetJointSurveyUsingRepo();
        Task<bool> Update(int id, Jointsurvey jointsurvey);
        Task<bool> Create(Jointsurvey jointsurvey);
        Task<Jointsurvey> FetchSingleResult(int id);
        Task<bool> Delete(int id);
    }
}
