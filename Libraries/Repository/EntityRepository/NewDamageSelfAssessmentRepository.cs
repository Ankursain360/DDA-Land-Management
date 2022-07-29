using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
using Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class NewDamageSelfAssessmentRepository : GenericRepository<Newdamagepayeeregistration>, INewDamageSelfAssessmentRepository
    {
        public NewDamageSelfAssessmentRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> Any(int id, string Regid)
        {
            return await _dbContext.newdamage_selfassessment.AnyAsync(t => t.Id != id && t.RegId.ToLower() == Regid.ToLower());
        }

        public async Task<List<District>> GetAllDistrict()
        {
            List<District> districtList = await _dbContext.District.Where(x => x.IsActive == 1 && x.Code == "09").ToListAsync();
            return districtList;
        }
        public async Task<List<Floors>> GetFloors()
        {
            List<Floors> floorlist = await _dbContext.Floors.Where(x => x.IsActive == 1).ToListAsync();
            return floorlist;
        }
        public async Task<List<Locality>> GetLocalityList()
        {
            var localityList = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
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
        public async Task<List<Newdamagepayeeregistration>> GetAllDamageSelfAssessments()
        {
            return await _dbContext.newdamagepayeeregistration.Include(x => x.GetVillage)
                                                            .Include(x => x.GetDistrict)
                                                            .Include(x => x.GetColony).ToListAsync();
        }

        public async Task<PagedResult<Newdamagepayeeregistration>> GetPagedDamagePayee(DamagePayeeSearchDto model, int id)
        {
            // var result = await _dbContext.newdamagepayeeregistration.Where(x => x.Id == id).SingleOrDefaultAsync();
            var data = await _dbContext.newdamagepayeeregistration
                                           .Include(x => x.GetDistrict)
                                           .Include(x => x.GetVillage)
                                           .Include(x => x.GetColony)
                                           .Include(x => x.GetApprovedStatusNavigation)
                                           .Where(x => (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                           && (x.CreatedBy == (id == 0 ? x.CreatedBy : id))
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
                                               .Where(x => (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                               && (x.CreatedBy == id)
                                               )
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
                                               && (x.CreatedBy == id)
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
                                               .Where(x => (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                               && (x.CreatedBy == id)
                                               )
                                               .OrderByDescending(x => x.FileNo)
                                               .GetPaged<Newdamagepayeeregistration>(model.PageNumber, model.PageSize);
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.newdamagepayeeregistration
                                               .Where(x => (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                               && (x.CreatedBy == id)
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
        public async Task<Newdamagepayeeregistration> GetUploadDocumentFilePath(int Id)
        {
            return await _dbContext.newdamagepayeeregistration.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }


        //********* Ats Self Assessment Details **********
        public async Task<bool> SaveATSDetails(Newdamageselfassessmentatsdetail atsDetails)
        {
            _dbContext.newdamageselfassessmentatsdetail.Add(atsDetails);

            var Result = await _dbContext.SaveChangesAsync();
            _dbContext.Entry(atsDetails).State = EntityState.Detached;
            return Result > 0 ? true : false;
        }

        //public async Task<bool> SaveAttendance(NewDamageSelfAssessmentAtsDetails atsDetails)
        //{
        //    _dbContext.newdamage_selfassessment_atsdetail.Add(atsDetails);
        //    var Result = await _dbContext.SaveChangesAsync();
        //    return Result > 0 ? true : false;
        //}

        public async Task<List<Newdamageselfassessmentatsdetail>> GetAllAtsDetails(int id)
        {
            return await _dbContext.newdamageselfassessmentatsdetail.Where(x => x.NewDamageSelfAssessmentId == id).ToListAsync();
        }
        public async Task<bool> DeleteAts(int Id)
        {
            _dbContext.RemoveRange(_dbContext.newdamage_selfassessment_atsdetail.Where(x => x.NewDamageSelfAssessmentId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<Newdamageselfassessmentatsdetail> GetAtsFilePath(int id)
        {
            return await _dbContext.newdamageselfassessmentatsdetail.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //********* Gpa Self Assessment Details **********

        public async Task<bool> SaveGPADetails(Newdamageselfassessmentgpadetail gpaDetails)
        {
            _dbContext.newdamageselfassessmentgpadetail.Add(gpaDetails);

            var Result = await _dbContext.SaveChangesAsync();
            _dbContext.Entry(gpaDetails).State = EntityState.Detached;
            return Result > 0 ? true : false;
        }

        public async Task<List<Newdamageselfassessmentgpadetail>> GetAllGpaDetails(int id)
        {
            return await _dbContext.newdamageselfassessmentgpadetail.Where(x => x.NewDamageSelfAssessmentId == id).ToListAsync();
        }
        public async Task<bool> DeleteGpa(int Id)
        {
            _dbContext.RemoveRange(_dbContext.newdamage_selfassessment_gpadetail.Where(x => x.NewDamageSelfAssessmentId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<Newdamageselfassessmentgpadetail> GetGpaFilePath(int Id)
        {
            return await _dbContext.newdamageselfassessmentgpadetail.Where(x => x.Id == Id ).FirstOrDefaultAsync();
        }

        //public async Task<bool> SaveHolderdetails(Newdamageselfassessmentholderdetail holderDetails)
        //{
        //    _dbContext.newdamageselfassessmentholderdetail.Add(holderDetails);

        //    var Result = await _dbContext.SaveChangesAsync();
        //    _dbContext.Entry(holderDetails).State = EntityState.Detached;
        //    return Result > 0 ? true : false;
        //}

        //******* damage payment details ******

        public async Task<bool> SavePaymentdetails(Newdamagepaymenthistory paymentDetails)
        {
            _dbContext.newdamagepaymenthistory.Add(paymentDetails);
            var Result = await _dbContext.SaveChangesAsync();
            _dbContext.Entry(paymentDetails).State = EntityState.Detached;
            return Result > 0 ? true : false;
        }

       public async Task<List<Newdamagepaymenthistory>> Getpaymentdetail(int id)
        {
            return await _dbContext.newdamagepaymenthistory.Where(x => x.NewDamageSelfAssessmentId == id).ToListAsync();
        }
       public async Task<Newdamagepaymenthistory> GetpaymentFile(int id)
        {
            return await _dbContext.newdamagepaymenthistory.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        //********* Add Floor ! Damage Details ***********

        public async Task<bool> SaveFloorDetails(Newdamageselfassessmentfloordetail floordetails)
        {
            _dbContext.newdamageselfassessmentfloordetail.Add(floordetails);
            var Result = await _dbContext.SaveChangesAsync();
            _dbContext.Entry(floordetails).State = EntityState.Detached;
            return Result > 0 ? true : false;
        }
        public async Task<bool> SaveSurveyReport(Newdamageselfassessmentfloordetail addDloorDetails)
        {
            _dbContext.newdamageselfassessmentfloordetail.Add(addDloorDetails);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Newdamageselfassessmentfloordetail>> GetAddfloorsDetails(int id)
        {
            return await _dbContext.newdamageselfassessmentfloordetail.Where(x => x.NewDamageSelfAssessmentId == id).ToListAsync();
        }
        public async Task<Newdamageselfassessmentfloordetail> GetAddFloorFilePath(int Id)
        {
            return await _dbContext.newdamageselfassessmentfloordetail.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteAddFloor(int Id)
        {
            _dbContext.RemoveRange(_dbContext.newdamage_addfloor.Where(x => x.NewDamageSelfAssessmentId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        //********occupant details *******
        public async Task<bool> SaveOccupantDetails(Newdamagepayeeoccupantinfo occupantdetails)
        {
            _dbContext.newdamagepayeeoccupantinfo.Add(occupantdetails);

            var Result = await _dbContext.SaveChangesAsync();
            _dbContext.Entry(occupantdetails).State = EntityState.Detached;
            return Result > 0 ? true : false;
        }
        public async Task<List<Newdamagepayeeoccupantinfo>> GetOccupantDetails(int id)
        {
            return await _dbContext.newdamagepayeeoccupantinfo.Where(x => x.NewDamageSelfAssessmentId == id).ToListAsync();
        }
        public async Task<Newdamagepayeeoccupantinfo> GetOccupantFile(int id)
        {
            return await _dbContext.newdamagepayeeoccupantinfo.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

    }
}
