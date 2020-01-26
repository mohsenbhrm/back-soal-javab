using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoalJavab.DomainClasses;
using SoalJavab.Services.Models;
using SoalJavab.Services.Models.old;

namespace SoalJavab.Services.Contracts
{
    public interface IManageServices
    {
        long Count_All_Soals_of_User(long IdUser);
        long Count_All_Soals_of_User_GetJavab(long IdUser);
        long Count_Soals_of_User_GetJavab_notVisited(long IdUser);

        long Count_All_Soals_To_User(long IdUser);
        long Count_Soals_To_User_notVisited(long IdUser);
        long Count_Soals_To_User_getJavab(long IdUser);

        void Check_visited_Soals_To_User(long[] IdSoals);
        //bool Delete_soal_To_User(long IdSoal);
        bool Delete_soal_To_User(long[] IdSoal);
        IList<Soal> List_soals_To_user(long IdUser);
        IList<Javab> List_Javab_Of_soal_To_User(long IdUser);
        IList<Soal> List_soals_Of_user(long IdUser);
        IList<Soal> List_Deleted_Soals_Of_user(long IdUser);
        bool Delete_soal_Of_user(long IdSoal);


        //ReshtehVm Creat_ModelView_For_Manage_Reshteh();
        List<ZirReshteVm> list_zirReshte_By_reshtehId(long ReshteId);
        bool Post_zirreshteh_for_user(long[] ZRId, long UserId,long reshtehId);

        IList<Models.JsonVm> getZirReshteh_JsonVM(string Id, long userId);
        IList<Models.JsonVm> getuserZirReshteh_JsonVM(string Id, long UserId);
    }
}
