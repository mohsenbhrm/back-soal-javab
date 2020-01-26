using System.Collections.Generic;
using SoalJavab.DomainClasses;
using SoalJavab.Services.Models;

namespace SoalJavab.Services.Contracts
{
    public interface ISoalToUserServices
    {
        long CountofSoasToUser(long userId);
        long Count_soal_To_User(long Iduser);
        bool DeleteSoalToUserById(long StoUId);
        IList<SoalToUserVM> GetnewFeedSoalTouserByIdUser(long Iduser);
        IList<SoalToUserVM> GetnewSoalTouserByIdUser(long Iduser);
        SoalToUserVM getSoalTouserByStoUId(long Id, long userId);
        // IEnumerable<SoalToUserVM> getSoalToUserByUserID(long IdUser);
        IList<SoalToUserVM> getSoalToUserByUserID(long LastIdsoal, long IdUser);
        IList<SoalToUserVM> getSoalToUserByUserId_10(long IdUser);
        long lastSoalToUserId(long Id);
        IList<Soal> List_soal_To_User(long Iduser);
        IList<Soal> Post_Soal_To_user_by_UserId_10(long userId);
    }
}