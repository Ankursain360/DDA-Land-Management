using Dto.Master;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class InsertVacantLandImagesRepository : GenericRepository<Vacantlandimage>, IInsertVacantLandImagesRepository
    {
        public InsertVacantLandImagesRepository(DataContext dbContext) : base(dbContext)
        {

        }

       
    }
}
