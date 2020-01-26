using System.Collections.Generic;
using SoalJavab.Services.Models;

namespace SoalJavab.Services.Contracts
{
    public partial interface IServices
    {
        long CountofSoasToUser(long userId);
        SoalToUserVM getSoalTouserByStoUId(long Id,long userId);
        IEnumerable<SoalToUserVM> getSoalToUserByUserID(long IdUser);
        IList<SoalToUserVM> getSoalToUserByUserId_10(long IdUser);
        IList<SoalToUserVM> getSoalToUserByUserId(long IdUser);
        IList<SoalToUserVM> getVisitedSoalToUserByUserId(long IdUser);
        IList<SoalToUserVM> getAnsweredSoalToUserByUserId(long IdUser);

        bool DeleteSoalToUserById(long Id);
    }
}
