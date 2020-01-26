using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoalJavab.DomainClasses;

namespace SoalJavab.Services.Contracts
{
    public interface IZirReshteh : IGenericRepository<ZirReshteh>
    {
        IList<ZirReshteh> Get(long ReshtehId);
        Task<IEnumerable<ZirReshteh>> GetAsync(long ReshtehId);
    }
}
