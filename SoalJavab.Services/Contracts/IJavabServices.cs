using System.Collections.Generic;
using SoalJavab.Services.Models;

namespace SoalJavab.Services.Contracts
{ 
    public interface IJavabServices
    {
        bool Creatjavab(JavabVM jv);
        JavabCountVM getJavabCountOfSoal(long IdSoal);
        long getCountJavabCountOfSoal(long IdSoal);
        JavabVM getjavabforCreat(string IdSoal = "");
        IEnumerable<JavabVM> getJavabofsoalBysoalId(long IdSoal);
        bool EditeJavab(JavabVM javab);
        bool isJavabOfuser(long javabId, long userId);
        IList<JavabVM> GetAllJavabsByuserId(long userId);
        bool Delete(long JavabId);
    }
}