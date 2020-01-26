using System.Collections.Generic;
using SoalJavab.Services.Models;

namespace SoalJavab.Services.Contracts
{
    public partial interface IServices
    {
        bool Creatjavab(JavabVM jv);
        TagVM CreatTag(TagVM tag);
        JavabVM getjavabforCreat(string IdSoal = "");
        IEnumerable<JavabVM> getJavabofsoalBysoalId(long IdSoal);
        SoalVM getSoalForCreat();
        SoalVM getSoalForEdit(long id);
        IList<SoalOfUserVM> getSoalsbyUserID(long Id);
        TagVM getTagForCreat(string ZirReshte);
        IEnumerable<UserListVM> getUsersList();
        IList<JsonVm> getZirReshteh_JsonVM(string Id);
        bool postforSoal(SoalVM soalVM, long UserId);
        bool EditforSoal(SoalVM soalVM);
        JavabCountVM getJavabCountOfSoal(long IdSoal);
        SoalOfUserVM getSoalbyID(long Id);
        IList<JsonVm> getTAGs(long IdZirreshte);
        IList<JsonVm> getTAGsofSoal(long Idsoal);
        IList<JsonVm> getOtherTAGsforSoal(long Idsoal);
    }
}