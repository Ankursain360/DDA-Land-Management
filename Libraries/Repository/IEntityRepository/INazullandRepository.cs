using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
   
     public interface INazullandRepository : IGenericRepository<Nazulland>
    {
        Task<List<Nazulland>> GetNazulland();
        Task<List<Division>> GetAllDivision();
        //Task<bool> Any(int id, string name);
        Task<List<Nazulland>> GetAllNazulland();
    }
}
