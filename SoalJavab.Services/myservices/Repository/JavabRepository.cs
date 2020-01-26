using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data.Entity;
using SoalJavab.DomainClasses;
using SoalJavab.DataLayer;
using SoalJavab.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SoalJavab.Services
{
    public class JavabRepository :  IJavabRepository
    {
        private IUnitOfWork db;
        private DbSet<Javab> _Javabs;
        public JavabRepository(IUnitOfWork Context) 
        {
            this.db = Context;
            _Javabs = db.Set<Javab>();
         
        }

        public bool Delete(Javab entity)
        {
            try {

                entity.IsDeleted = true;
                _Javabs.Attach(entity);
                db.MarkAsChanged<Javab>(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(long entityId)
        {
            try
            {
                var q = GetById(entityId);
                if (q != null)
                {
                    Delete(q);
                    return true;
                }
                return false;
            }
            catch
            {
                throw new InvalidOperationException();
            }
        }

        public Task<bool> DeleteASync(Javab entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteASync(long entityId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Javab> Get(Expression<Func<Javab, bool>> filter = null,
            Func<IQueryable<Javab>, IOrderedQueryable<Javab>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<Javab> query = _Javabs;

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

        public IEnumerable<Javab> Get()
        {
            throw new NotImplementedException();
        }

        public IList<Javab> GetAllByUserID(long UserID)
        {
            return _Javabs.Where(x => x.User.Id.Equals(UserID)).ToList();
        }

        public Javab GetById(long Id) => _Javabs.Where(x => x.Id == Id).FirstOrDefault();

        public Task<Javab> GetByIdAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Javab entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertASync(Javab entity)
        {
            throw new NotImplementedException();
        }

        public Javab Insert_ReturnObject(Javab entity)
        {
            throw new NotImplementedException();
        }

        public Task<Javab> Insert_ReturnObjectASync(Javab entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Javab entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateASync(Javab entity)
        {
            throw new NotImplementedException();
        }
    }
}
