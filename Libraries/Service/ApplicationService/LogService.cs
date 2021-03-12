using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.EntityFrameworkCore;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{
    public class LogService : EntityService<Log>, ILogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogRepository _logRepository;
        public LogService(IUnitOfWork unitOfWork, ILogRepository logRepository)
      : base(unitOfWork, logRepository)
        {
            _unitOfWork = unitOfWork;
            _logRepository = logRepository;
        }





      

        public async Task<Log> FetchSingleResult(int id)
        {
            var result = await _logRepository.FindBy(a => a.Id == id);
            Log model = result.FirstOrDefault();
            return model;
        }

       

        public async Task<List<Log>> GetLog()
        {

            return await _logRepository.GetLog();
        }

       

      

        public async Task<List<Log>> GetLogUsingRepo()
        {
            return await _logRepository.GetLog();
        }

        


        public async Task<PagedResult<Log>> GetPagedLog(LogSearchDto model)
        {
            return await _logRepository.GetPagedLog(model);
        }
       


    }
}
