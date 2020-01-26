using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoalJavab.DomainClasses;


namespace SoalJavab.Services.Contracts
{
    public interface IReshteh 
    {
        bool Delete(int entityId);

        IQueryable<Reshteh> Get();
        Task<IEnumerable<Reshteh>> GetAsync();
        Reshteh GetById(int Id);
        
    }
}
