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
    public class NewdamagepayeeregistrationRepository : GenericRepository<Newdamagepayeeregistration>, INewdamagepayeeregistrationRepository
    {
        public NewdamagepayeeregistrationRepository(DataContext dbContext) : base(dbContext)
        {
        }

        //public async Task<bool> Any(int id, string Regid)
        //{
        //    return await _dbContext.newdamagepayeeregistration.AnyAsync(t => t.Id != id && t.FileNo.ToLower() == Regid.ToLower());
        //}

        public async Task<List<District>> GetAllDistrict()
        {
            List<District> districtList = await _dbContext.District.Where(x => x.IsActive == 1).ToListAsync();
            return districtList;
        }

        public async Task<List<Acquiredlandvillage>> GetAllVillage(int districtId)
        {
            var result = await _dbContext.Acquiredlandvillage.Where(x => x.DistrictId == districtId && x.IsActive == 1).ToListAsync();

            List<Acquiredlandvillage> villageList = result
                        .Select(o => new Acquiredlandvillage
                        {
                            Id = o.Id,
                            Name = o.Name
                        }).ToList();

            return villageList;
        }
        public async Task<List<New_Damage_Colony>> GetAllColony(int villageId)
        {
            List<New_Damage_Colony> result = await _dbContext.new_damage_colony.Where(//x => x.NewDamageVillageId == villageId &&
                                                                                       x => x.IsActive == 1).ToListAsync();

            List<New_Damage_Colony> colonyList = result
            .Select(o => new New_Damage_Colony
            {
                Id = o.Id,
                Name = o.Name
            }).ToList();
            return colonyList;
        }
        public async Task<List<Newdamagepayeeregistration>> GetAllDamagePayee()
        {
            return await _dbContext.newdamagepayeeregistration.Include(x => x.GetVillage).Include(x => x.GetDistrict).Include(x => x.GetColony).ToListAsync();
        }

        public async Task<PagedResult<Newdamagepayeeregistration>> GetPagedDamagePayee(DamagePayeeSearchDto model)
        {
            var data = await _dbContext.newdamagepayeeregistration
                                           .Include(x => x.GetDistrict)
                                           .Include(x => x.GetVillage)
                                           .Include(x => x.GetColony)
                                           .Include(x => x.GetApprovedStatusNavigation)
                                           .Where(x => (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                           //&& (x.DistrictId == (model.district == 0 ? x.DistrictId : model.district))
                                           //&& (x.VillageId == (model.village == 0 ? x.VillageId : model.village))
                                           )
                                           .GetPaged<Newdamagepayeeregistration>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("FILE"):
                        data = null;
                        data = await _dbContext.newdamagepayeeregistration
                                               .Where(x => (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno)))
                                               //&& (x.DistrictId == (model.district == 0 ? x.DistrictId : model.district))
                                               //&& (x.VillageId == (model.village == 0 ? x.VillageId : model.village)))
                                               .OrderBy(x => x.FileNo)
                                               .GetPaged<Newdamagepayeeregistration>(model.PageNumber, model.PageSize);
                        break;


                    //case ("DISTRICT"):
                    //    data = null;
                    //    data = await _dbContext.newdamagepayeeregistration
                    //                           .Where(x => (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                    //                           && (x.DistrictId == (model.district == 0 ? x.DistrictId : model.district))
                    //                           && (x.VillageId == (model.village == 0 ? x.VillageId : model.village)))
                    //                           .OrderBy(x => x.District.Name)
                    //                           .GetPaged<Newdamagepayeeregistration>(model.PageNumber, model.PageSize);
                    //    break;
                    //case ("VILLAGE"):
                    //    data = null;
                    //    data = await _dbContext.newdamagepayeeregistration
                    //                           .Where(x => (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                    //                           && (x.DistrictId == (model.district == 0 ? x.DistrictId : model.district))
                    //                           && (x.VillageId == (model.village == 0 ? x.VillageId : model.village)))
                    //                           .OrderBy(x => x.Village.Name)
                    //                           .GetPaged<Newdamagepayeeregistration>(model.PageNumber, model.PageSize);
                    //    break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.newdamagepayeeregistration
                                               .Where(x => (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                               //&& (x.DistrictId == (model.district == 0 ? x.DistrictId : model.district))
                                               //&& (x.VillageId == (model.village == 0 ? x.VillageId : model.village)))
                                               )
                                               .OrderByDescending(x => x.IsActive)
                                               .GetPaged<Newdamagepayeeregistration>(model.PageNumber, model.PageSize);
                        break;

                }

            }
            else if (SortOrder == 2)
            {

                switch (model.SortBy.ToUpper())
                {
                    case ("FILE"):
                        data = null;
                        data = await _dbContext.newdamagepayeeregistration
                                               .Where(x => (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno)))
                                               .OrderByDescending(x => x.FileNo)
                                               .GetPaged<Newdamagepayeeregistration>(model.PageNumber, model.PageSize);
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.newdamagepayeeregistration
                                               .Where(x => (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                               //&& (x.DistrictId == (model.district == 0 ? x.DistrictId : model.district))
                                               //&& (x.VillageId == (model.village == 0 ? x.VillageId : model.village)))
                                               )
                                               .OrderBy(x => x.IsActive)
                                               .GetPaged<Newdamagepayeeregistration>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }

        public async Task<List<Approvalstatus>> GetAllApprovalStatus()
        {
            List<Approvalstatus> approvalList = await _dbContext.Approvalstatus.Where(e => e.IsActive == 1).ToListAsync();
            return approvalList;
        }

       
    }
}
