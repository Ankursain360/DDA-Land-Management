using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class DamageRateListRepository : GenericRepository<Resratelisttypea>, IDamageRateListRepository
    {

        public DamageRateListRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<DamageRateListDataDto>> GetSearchResultPagedData(DamageRateListSearchDto model, List<DamageRateListDataDto> DamageRateListDataDto)
        {
            dynamic result1 = null;
            Encrochmenttype encrochmenttype = new Encrochmenttype();
            if (model.propertyid == 1)
            {
                var data = await _dbContext.Ressubencroacherstype
                                   .Where(a => a.IsActive == 1 && a.Id == model.daterangeid)
                                   .FirstOrDefaultAsync();
                encrochmenttype = await _dbContext.Encrochmenttype
                                .Where(x => x.EncroachStartDate <= data.EncroachStartDate && x.EncroachEndDate >= data.EncroachStartDate)
                                .FirstOrDefaultAsync();
            }
            else
            {
                var data =await _dbContext.Comsubencroacherstype
                                   .Where(a => a.IsActive == 1 && a.Id == model.daterangeid)
                                   .FirstOrDefaultAsync();

                encrochmenttype = await _dbContext.Encrochmenttype
                                .Where(x => x.EncroachStartDate <= data.EncroachStartDate && x.EncroachEndDate >= data.EncroachStartDate)
                                .FirstOrDefaultAsync();
            }

            if (encrochmenttype.Id == 1)
            {
                if (model.propertyid == 1)
                {
                    result1 =  await _dbContext.Resratelisttypea
                                            .Where(x => x.EncroachId == encrochmenttype.Id
                                            && x.SubEncroachId == model.daterangeid
                                            && x.ColonyId == model.localityid
                                            )
                                            .ToListAsync();
                }
                else
                {
                    result1 =  await _dbContext.Comratelisttypea
                                            .Where(x => x.EncroachId == encrochmenttype.Id
                                            && x.SubEncroachId == model.daterangeid
                                            && x.ColonyId == model.localityid
                                            )
                                            .ToListAsync();
                }
            }
            else if (encrochmenttype.Id == 2)
            {
                if (model.propertyid == 1)
                {
                    result1 = await _dbContext.Resratelisttypeb
                                            .Where(x => x.EncroachId == encrochmenttype.Id
                                            && x.SubEncroachId == model.daterangeid
                                            && x.ColonyId == model.localityid
                                            )
                                            .ToListAsync();
                }
                else
                {
                    result1 = await _dbContext.Comratelisttypeb
                                            .Where(x => x.EncroachId == encrochmenttype.Id
                                            && x.SubEncroachId == model.daterangeid
                                            && x.ColonyId == model.localityid
                                            )
                                            .ToListAsync();
                }
            }
            else if(encrochmenttype.Id == 3)
            {
                if (model.propertyid == 1)
                {
                    result1 = await _dbContext.Resratelisttypec
                                            .Where(x => x.EncroachId == encrochmenttype.Id
                                            && x.SubEncroachId == model.daterangeid
                                            && x.ColonyId == model.localityid
                                            )
                                            .ToListAsync();
                }
                else
                {
                    result1 = await _dbContext.Comratelisttypec
                                            .Where(x => x.EncroachId == encrochmenttype.Id
                                            && x.SubEncroachId == model.daterangeid
                                            && x.ColonyId == model.localityid
                                            )
                                            .ToListAsync();
                }
            }

            if (result1 != null)
            {
                for (int i = 0; i < result1.Count; i++)
                {
                    DamageRateListDataDto.Add(new DamageRateListDataDto
                    {
                        Id = result1[i].Id,
                        FromDate = result1[i].StartDate,
                        ToDate = result1[i].EndDate,
                        Rate = result1[i].Rate
                    });
                }
            }

            return DamageRateListDataDto;
        }
        public async Task<List<DateRangeConcatedListDto>> GetDateRangeDropdownListCommercial()
        {
            List<DateRangeConcatedListDto> listData = new List<DateRangeConcatedListDto>();

            var Data = await _dbContext.Comsubencroacherstype
                                   .Where(a => a.IsActive == 1)
                                   .ToListAsync();

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new DateRangeConcatedListDto()
                    {
                        Id = Data[i].Id,
                        StartDate = Data[i].EncroachStartDate,
                        EndDate = Data[i].EncroachEndDate,
                        Name = string.Format("{0} To {1}", Data[i].EncroachStartDate.ToString("dd/MMM/yyyy"), Data[i].EncroachEndDate.ToString("dd/MMM/yyyy"))
                    });
                }
            }

            return listData;
        }

        public async Task<List<DateRangeConcatedListDto>> GetDateRangeDropdownListResidential()
        {
            List<DateRangeConcatedListDto> listData = new List<DateRangeConcatedListDto>();

            var Data = await _dbContext.Ressubencroacherstype
                                   .Where(a => a.IsActive == 1)
                                   .ToListAsync();

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new DateRangeConcatedListDto()
                    {
                        Id = Data[i].Id,
                        StartDate = Data[i].EncroachStartDate,
                        EndDate = Data[i].EncroachEndDate,
                        Name = string.Format("{0} To {1}", Data[i].EncroachStartDate.ToString("dd/MMM/yyyy"), Data[i].EncroachEndDate.ToString("dd/MMM/yyyy"))
                    });
                }
            }

            return listData;
        }

        public async Task<List<Locality>> GetLocalities()
        {
            var localityList = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
        }

        public async Task<List<PropertyType>> GetPropertyTypes()
        {
            var propertytypeList = await _dbContext.PropertyType.Where(x => x.IsActive == 1).ToListAsync();
            return propertytypeList;
        }
    }
}
