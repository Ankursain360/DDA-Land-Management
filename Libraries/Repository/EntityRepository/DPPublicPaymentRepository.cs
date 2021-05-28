
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
    public class DPPublicPaymentRepository : GenericRepository<Demandletters>, IDPPublicPaymentRepository
    {
        public DPPublicPaymentRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregistertempSearchDto model)
        {
            var data = await _dbContext.Damagepayeeregister
                                        .Include(x => x.Locality)
                                        .Include(x => x.District)
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Where(x => x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality)
                                         && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                         && (string.IsNullOrEmpty(model.propertyno) || x.PropertyNo.Contains(model.propertyno))
                                        )
                                        .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.Damagepayeeregister
                                            .Include(x => x.Locality)
                                            .Include(x => x.District)
                                            .Include(x => x.ApprovedStatusNavigation)
                                            .Where(x => x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality)
                                             && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                             && (string.IsNullOrEmpty(model.propertyno) || x.PropertyNo.Contains(model.propertyno))
                                            )
                                            .OrderByDescending(s => s.IsActive)
                                            .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = null;
                    data = await _dbContext.Damagepayeeregister
                                            .Include(x => x.Locality)
                                            .Include(x => x.District)
                                            .Include(x => x.ApprovedStatusNavigation)
                                            .Where(x => x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality)
                                             && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                             && (string.IsNullOrEmpty(model.propertyno) || x.PropertyNo.Contains(model.propertyno))
                                            )
                                           .OrderBy(s =>
                                           (model.SortBy.ToUpper() == "FILENO" ? s.FileNo
                                           : model.SortBy.ToUpper() == "REGISTRATIONNO" ? s.TypeOfDamageAssessee
                                           : model.SortBy.ToUpper() == "PROPERTYNO" ? s.PropertyNo
                                           : model.SortBy.ToUpper() == "LOCALITY" ? (s.Locality == null ? null : s.Locality.Name)
                                           : model.SortBy.ToUpper() == "ISDDADAMAGEPAYEE" ? s.IsDdadamagePayee
                                           : s.FileNo)
                                           )
                                            .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
                }


            }
            else if (SortOrder == 2)
            {
                data = null;
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.Damagepayeeregister
                                            .Include(x => x.Locality)
                                            .Include(x => x.District)
                                            .Include(x => x.ApprovedStatusNavigation)
                                            .Where(x => x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality)
                                             && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                             && (string.IsNullOrEmpty(model.propertyno) || x.PropertyNo.Contains(model.propertyno))
                                            )
                                            .OrderBy(s => s.IsActive)
                                            .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = null;
                    data = await _dbContext.Damagepayeeregister
                                            .Include(x => x.Locality)
                                            .Include(x => x.District)
                                            .Include(x => x.ApprovedStatusNavigation)
                                            .Where(x => x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality)
                                             && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                             && (string.IsNullOrEmpty(model.propertyno) || x.PropertyNo.Contains(model.propertyno))
                                            )
                                           .OrderByDescending(s =>
                                           (model.SortBy.ToUpper() == "FILENO" ? s.FileNo
                                           : model.SortBy.ToUpper() == "REGISTRATIONNO" ? s.TypeOfDamageAssessee
                                           : model.SortBy.ToUpper() == "PROPERTYNO" ? s.PropertyNo
                                           : model.SortBy.ToUpper() == "LOCALITY" ? (s.Locality == null ? null : s.Locality.Name)
                                           : model.SortBy.ToUpper() == "ISDDADAMAGEPAYEE" ? s.IsDdadamagePayee
                                           : s.FileNo)
                                           )
                                            .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
                }

            }

            return data;
        }

        public async Task<Damagepayeeregister> FetchDamagePayeeRegisterDetails(int userId)
        {
            return await _dbContext.Damagepayeeregister
                                    .Include(x => x.Damagepayeepersonelinfo)
                                    .Include(x => x.Damagepaymenthistory)
                                    .Include(x => x.Allottetype)
                                    .Where(x => x.UserId == userId)
                                    .FirstOrDefaultAsync();
        }
        public async Task<List<Demandletters>> GetDemandDetails(string FileNo)
        {
            return await _dbContext.Demandletters
                                   .Include(x => x.Locality)
                                   .Where(x => x.FileNo == FileNo)
                                   .ToListAsync();
        }

    }
}
