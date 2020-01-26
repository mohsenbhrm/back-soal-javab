using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SoalJavab.DataLayer;
using System.Linq.Expressions;
using SoalJavab.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SoalJavab.Services
{
    public class GenericRepository<TEntity>  : IGenericRepository<TEntity> where TEntity : class
    {
        internal IUnitOfWork  context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(IUnitOfWork context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public IEnumerable<TEntity> Get()
        {
            throw new NotImplementedException();
        }
        public virtual TEntity GetById(long id)
        {
            return dbSet.Find(id);
        }

        public virtual bool Insert(TEntity entity)
        {
            try
            {
                context.Addnew(entity);
                return true;
            }
            catch { return false; }
        }
        public virtual bool Delete(long id)
        {
            try
            {
                TEntity entityToDelete = dbSet.Find(id);
                Delete(entityToDelete);
                return true;
            }
            catch { return false; }
        }
        public virtual bool Delete(TEntity entityToDelete)
        {
            try
            {
                if (context.Delete(entityToDelete))
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
                return true;
            }
            catch { return false; }
        }

        public virtual bool Update(TEntity entityToUpdate)
        {
            try
            {
                dbSet.Attach(entityToUpdate);
                context.MarkAsChanged<TEntity>(entityToUpdate);
                return true;
            }
            catch { return false; }
        }

        public virtual TEntity Insert_ReturnObject(TEntity entityToAdd)
        {
            try
            {
                context.Addnew<TEntity>(entityToAdd);
                return entityToAdd;
            }
            catch { return null; }
        }

        public virtual async Task<TEntity> GetByIdAsync(long Id)
        {
            return await dbSet.FindAsync(Id);
        }

        public virtual Task<bool> InsertASync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> Insert_ReturnObjectASync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> UpdateASync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> DeleteASync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual  Task<bool> DeleteASync(long entityId)
        {
            throw new NotImplementedException();
        }

       
    }
}