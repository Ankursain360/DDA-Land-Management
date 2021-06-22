
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class SurveyuserdetailRepository : GenericRepository<Surveyuserdetail>, ISurveyuserdetailRepository
    {
        public SurveyuserdetailRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Surveyuserdetail>> GetPagedSurveyuserdetail(SurveyuserdetailSearchDto model)
        {
            var data = await _dbContext.Surveyuserdetail

                            .Where(x => (string.IsNullOrEmpty(model.name) || x.UserName.Contains(model.name)))
                            .GetPaged<Surveyuserdetail>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Surveyuserdetail
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.UserName.Contains(model.name)))
                            .OrderBy(x => x.UserName)
                            .GetPaged<Surveyuserdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Surveyuserdetail
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.UserName.Contains(model.name)))
                            .OrderByDescending(x => x.IsActive)
                            .GetPaged<Surveyuserdetail>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Surveyuserdetail
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.UserName.Contains(model.name)))
                            .OrderByDescending(x => x.UserName)
                            .GetPaged<Surveyuserdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Surveyuserdetail
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.UserName.Contains(model.name)))
                            .OrderBy(x => x.IsActive)
                            .GetPaged<Surveyuserdetail>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
            // return await _dbContext.Structure.GetPaged<Structure>(model.PageNumber, model.PageSize);
        }

       
       
        public async Task<bool> AnyUser(int id, string username)
        {
            var data =  await _dbContext.Surveyuserdetail.AnyAsync(t => t.Id != id && t.UserName.ToLower() == username.ToLower());
            return data;
        }
        public async Task<bool> anyPhone(int id, string phone)
        {
            var data = await _dbContext.Surveyuserdetail.AnyAsync(t => t.Id != id && t.PhoneNo == phone);
            return data;
        }
        public async Task<bool> anyEmail(int id, string email)
        {
            var data = await _dbContext.Surveyuserdetail.AnyAsync(t => t.Id != id && t.EmailId.ToLower() == email.ToLower());
            return data;
        }


        public async Task<List<Surveyuserdetail>> GetAllSurveyuserdetail()
        {
            return await _dbContext.Surveyuserdetail.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Surveyuserrole>> GetUserDropDownList()
        {
            var List = await _dbContext.Surveyuserrole.Where(x => x.IsActive == 1).ToListAsync();
            return List;
        }
    }
}
