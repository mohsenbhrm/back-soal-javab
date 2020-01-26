using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoalJavab.DomainClasses;


namespace SoalJavab.Services.Contracts
{
    public interface IUserRepository :IGenericRepository<ApplicationUser>
    {
        long Tedad(DateTime dt);
        long Tedad(DateTime dt1, DateTime dt2);
        IEnumerable<ApplicationUser> GetallbyDate(DateTime dt);
        IEnumerable<ApplicationUser> GetallbyReshteh(ZirReshteh ZR);
        long[] Get_ZirReshtehId_by_UserId(long UserId);

    }
}
