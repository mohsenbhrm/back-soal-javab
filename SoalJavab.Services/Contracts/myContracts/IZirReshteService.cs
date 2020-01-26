using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoalJavab.DomainClasses;
using SoalJavab.Services.Models;


namespace SoalJavab.Services.Contracts
{
    public interface IZirReshtehServices
    {
        IList<ZirReshtehVm> GetByReshteh(long id);
        IList<ZirReshtehVm> GetByUser(long id);
        bool ValidateZirreshteh(long[] id);
        bool ValidateZirreshteh(long id);
    }
}
