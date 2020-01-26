using System;
using System.Data;
using System.Collections.Generic;
using SoalJavab.DomainClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SoalJavab.Services.Contracts
{
   public  interface IGenericRepository<TEntity>
    {

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        IEnumerable<TEntity> Get();
        TEntity GetById(long Id);
        bool Insert(TEntity entity);
        TEntity Insert_ReturnObject(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        bool Delete(long entityId);

        // async
        Task<TEntity> GetByIdAsync(long Id);
        Task<bool> InsertASync(TEntity entity);
        Task<TEntity> Insert_ReturnObjectASync(TEntity entity);
        Task<bool> UpdateASync(TEntity entity);
        Task<bool> DeleteASync(TEntity entity);
        Task<bool> DeleteASync(long entityId);

    }
}
