using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
namespace Libraries.Repository.EntityRepository
{
    public class ApplicationNotificationTemplateRepository : GenericRepository<ApplicationNotificationTemplate>, IApplicationNotificationTemplateRepository
    {
        public ApplicationNotificationTemplateRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagedResult<ApplicationNotificationTemplate>> GetPagedTemplate(ApplicationNotificationTemplateSearchDto model)
        {
            var data = await _dbContext.ApplicationNotificationTemplate
                             .Where(x => (string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name)))
                             .GetPaged<ApplicationNotificationTemplate>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.ApplicationNotificationTemplate
                              .Where(x => (string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name)))
                                        .OrderByDescending(s => s.IsActive)
                                        .GetPaged<ApplicationNotificationTemplate>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = await _dbContext.ApplicationNotificationTemplate
                              .Where(x => (string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name)))
                                       .OrderBy(s =>
                                       (model.SortBy.ToUpper() == "NAME" ? s.Name : s.Name)
                                       )
                                        .GetPaged<ApplicationNotificationTemplate>(model.PageNumber, model.PageSize);
                }


            }
            else if (SortOrder == 2)
            {
                data = null;
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.ApplicationNotificationTemplate
                             .Where(x => (string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name)))
                                       .OrderBy(s => s.IsActive)
                                       .GetPaged<ApplicationNotificationTemplate>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = await _dbContext.ApplicationNotificationTemplate
                             .Where(x => (string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name)))
                                      .OrderByDescending(s =>
                                      (model.SortBy.ToUpper() == "NAME" ? s.Name : s.Name)
                                      )
                                       .GetPaged<ApplicationNotificationTemplate>(model.PageNumber, model.PageSize);
                }
            }
            return data;
        }

        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Actions.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }
    }
}
