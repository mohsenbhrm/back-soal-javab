using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SoalJavab.DomainClasses;

namespace SoalJavab.Services.Contracts
{
   public interface IJavabRepository 
    {
        bool Delete(Javab entity);
        bool Delete(long entityId);
        IEnumerable<Javab> Get(Expression<Func<Javab, bool>> filter = null, Func<IQueryable<Javab>, IOrderedQueryable<Javab>> orderBy = null, string includeProperties = "");
        IList<Javab> GetAllByUserID(long UserID);
        Javab GetById(long Id);
        bool Update(Javab entity);
    }
}
