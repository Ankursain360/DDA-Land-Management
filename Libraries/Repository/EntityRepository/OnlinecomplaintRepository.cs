using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.EntityRepository
{
    public class OnlinecomplaintRepository : GenericRepository<Onlinecomplaint>, IOnlinecomplaintRepository
    {
        public OnlinecomplaintRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<List<ComplaintType>> GetAllComplaintType()
        {
            List<ComplaintType> ComplaintList = await _dbContext.ComplaintType.Where(x => x.IsActive == 1).ToListAsync();
            return ComplaintList;
        }
        public async Task<List<Location>> GetAllLocation()
        {
            List<Location> LocationList = await _dbContext.Location.Where(x => x.IsActive == 1).ToListAsync();
            return LocationList;
        }


        public async Task<List<Onlinecomplaint>> GetAllOnlinecomplaint()
        {
            return await _dbContext.Onlinecomplaint
                .Where(x=>x.IsActive==1)
                .Include(x=>x.ApprovedStatusNavigation)
                .Include(x => x.Location)
                .Include(x => x.ComplaintType)
                .OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<PagedResult<Onlinecomplaint>> GetPagedOnlinecomplaint(OnlinecomplaintSearchDto model)
        {
            var data = await _dbContext.Onlinecomplaint
                                        .Include(x => x.Location)
                                        .Include(x => x.ComplaintType)
                                        .OrderByDescending(x => x.Id)
                                        .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                           && (string.IsNullOrEmpty(model.contact) || x.Contact.Contains(model.contact))
                                           && (string.IsNullOrEmpty(model.email) || x.Email.Contains(model.email))
                                           )
                                            .GetPaged<Onlinecomplaint>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.orderby;
            if (SortOrder == 1)
            {
                switch (model.colname.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;
                    case ("CONTACT"):
                        data.Results = data.Results.OrderBy(x => x.Contact).ToList();
                        break;
                    case ("EMAIL"):
                        data.Results = data.Results.OrderBy(x => x.Email).ToList();
                        break;
                    case ("ADDRESS"):
                        data.Results = data.Results.OrderBy(x => x.AddressOfComplaint).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderBy(x => x.IsActive).ToList();
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.colname.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.Name).ToList();
                        break;
                    case ("CONTACT"):
                        data.Results = data.Results.OrderByDescending(x => x.Contact).ToList();
                        break;
                    case ("EMAIL"):
                        data.Results = data.Results.OrderByDescending(x => x.Email).ToList();
                        break;
                    case ("ADDRESS"):
                        data.Results = data.Results.OrderByDescending(x => x.AddressOfComplaint).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderByDescending(x => x.IsActive).ToList();
                        break;

                }
            }
            return data;



        }


    }
    }

