﻿using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
namespace Libraries.Repository.EntityRepository
{
    public class JointsurveyRepository : GenericRepository<Jointsurvey>, IJointsurveyRepository
    {
        public JointsurveyRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Jointsurvey>> GetAllJointSurvey()
        {
            return await _dbContext.Jointsurvey.Include(x => x.Village).Include(x => x.Khasra).OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<List<Jointsurvey>> GetAllJointsurveyList(JointSurveySearchDto model) 
        {
            var data = await _dbContext.Jointsurvey
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.Village.Name == (model.VillageName == "" ? x.Village.Name : model.VillageName)
                                         && (x.Khasra.Name == (model.KhasraName == "" ? x.Khasra.Name : model.KhasraName))).ToListAsync();
            return data;
        }

        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }



        public async Task<List<Khasra>> BindKhasra(int villageid)
        {
            List<Khasra> result = await _dbContext.Khasra.Where(x => x.IsActive == 1 && x.AcquiredlandvillageId== villageid).ToListAsync();
            List<Khasra> KhasraList = result
                .Select(o => new Khasra
                {
                    Id = o.Id,
                    Name = o.Name
                }).ToList();
            return KhasraList;
        }


        public async Task<PagedResult<Jointsurvey>> GetPagedJointsurvey(JointSurveySearchDto model)
        {
            var data = await _dbContext.Jointsurvey
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.Village.Name == (model.VillageName == "" ? x.Village.Name : model.VillageName)
                                         && (x.Khasra.Name == (model.KhasraName == "" ? x.Khasra.Name : model.KhasraName))

                                        ).GetPaged<Jointsurvey>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Jointsurvey
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.Village.Name == (model.VillageName == "" ? x.Village.Name : model.VillageName)
                                        && (x.Khasra.Name == (model.KhasraName == "" ? x.Khasra.Name : model.KhasraName))
                                        )
                                .OrderBy(s =>
                                (
                                  model.SortBy.ToUpper() == "VILLAGENAME" ? (s.Village != null ? s.Village.Name : null)
                                : model.SortBy.ToUpper() == "KHASRANAME" ? (s.Khasra != null ? s.Khasra.Name : null) : s.Village.Name)
                                )
                                .GetPaged<Jointsurvey>(model.PageNumber, model.PageSize);
            }

            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Jointsurvey
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.Village.Name == (model.VillageName == "" ? x.Village.Name : model.VillageName)
                                        && (x.Khasra.Name == (model.KhasraName == "" ? x.Khasra.Name : model.KhasraName))
                                        )
                                .OrderByDescending(s =>
                                (
                                model.SortBy.ToUpper() == "villagename" ? (s.Village != null ? s.Village.Name : null)
                                : model.SortBy.ToUpper() == "khasraname" ? (s.Khasra != null ? s.Khasra.Name : null) : s.Village.Name)
                                )
                                .GetPaged<Jointsurvey>(model.PageNumber, model.PageSize);
            }

            return data;



            //return await _dbContext.Jointsurvey.Include(x => x.Village).Include(x => x.Khasra).OrderByDescending(x => x.Id).GetPaged<Jointsurvey>(model.PageNumber, model.PageSize);
        }

        
    }
}
