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
    public class NewDamageSelfAssessmentRepository : GenericRepository<NewDamageSelfAssessment>, INewDamageSelfAssessmentRepository
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
            List<District> districtList = await _dbContext.District.Where(x => x.IsActive == 1).ToListAsync();
            return districtList;
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
                                                                                       x=> x.IsActive == 1).ToListAsync();

            List<New_Damage_Colony> colonyList = result
            .Select(o => new New_Damage_Colony
            {
                Id = o.Id,
                Name = o.Name
            }).ToList();
            return colonyList;
        }
        public async Task<List<NewDamageSelfAssessment>> GetAllDamageSelfAssessments()
        {
            return await _dbContext.newdamage_selfassessment.Include(x => x.GetAcquiredLandVillage)
                                                            .Include(x => x.GetDistrict)
                                                            .Include(x => x.GetLocality)
                                                            .Include(x => x.GetNew_Damage_Colony).ToListAsync();
        }


        //********* Ats Self Assessment Details **********

        public async Task<bool> SaveAttendance(NewDamageSelfAssessmentAtsDetails atsDetails)
        {
            _dbContext.newdamage_selfassessment_atsdetail.Add(atsDetails);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<NewDamageSelfAssessmentAtsDetails>> GetAllAtsDetails(int id)
        {
            return await _dbContext.newdamage_selfassessment_atsdetail.Where(x => x.NewDamageSelfAssessmentId == id).ToListAsync();
        }
        public async Task<bool> DeleteAts(int Id)
        {
            _dbContext.RemoveRange(_dbContext.newdamage_selfassessment_atsdetail.Where(x => x.NewDamageSelfAssessmentId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        //********* Gpa Self Assessment Details **********

        public async Task<bool> SaveFloorDetails(NewdamageAddfloor attendance)
        {
            _dbContext.newdamage_addfloor.Add(attendance);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }


        public async Task<bool> SaveGPADetails(NewDamageSelfAssessmentGpaDetails gpaDetails)
        {
            _dbContext.newdamage_selfassessment_gpadetail.Add(gpaDetails);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> SaveATSDetails(NewDamageSelfAssessmentAtsDetails atsDetails)
        {
            _dbContext.newdamage_selfassessment_atsdetail.Add(atsDetails);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }


        public async Task<List<NewDamageSelfAssessmentGpaDetails>> GetAllGpaDetails(int id)
        {
            return await _dbContext.newdamage_selfassessment_gpadetail.Where(x => x.NewDamageSelfAssessmentId == id).ToListAsync();
        }
        public async Task<bool> DeleteGpa(int Id)
        {
            _dbContext.RemoveRange(_dbContext.newdamage_selfassessment_gpadetail.Where(x => x.NewDamageSelfAssessmentId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        //********* Add Floor ! Damage Details ***********
        public async Task<bool> SaveSurveyReport(NewdamageAddfloor addDloorDetails)
        {
            _dbContext.newdamage_addfloor.Add(addDloorDetails);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<NewdamageAddfloor>> GetAddfloorsDetails(int id)
        {
            return await _dbContext.newdamage_addfloor.Where(x => x.NewDamageSelfAssessmentId == id).ToListAsync();
        }
        public async Task<NewdamageAddfloor> GetAddFloorFilePath(int Id)
        {
            return await _dbContext.newdamage_addfloor.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteAddFloor(int Id)
        {
            _dbContext.RemoveRange(_dbContext.newdamage_addfloor.Where(x => x.NewDamageSelfAssessmentId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }



        public async Task<NewDamageSelfAssessment> GetUploadDocumentFilePath(int Id)
        {
            return await _dbContext.newdamage_selfassessment.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }


    }
}
