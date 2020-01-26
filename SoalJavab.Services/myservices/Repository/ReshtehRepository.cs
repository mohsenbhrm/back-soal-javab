
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoalJavab.DataLayer;
using SoalJavab.DomainClasses;
using SoalJavab.Services.Contracts;

namespace SoalJavab.Services
{
    public class ReshtehRepository : IReshteh
    {
        private readonly IUnitOfWork db;
        private readonly DbSet<Reshteh> _Reshtehs;
        public ReshtehRepository(IUnitOfWork Context)
        {
            this.db = Context;
            _Reshtehs = db.Set<Reshteh>();

        }
        
        public bool Delete(int entityId)
        {
            try
            {
                return Delete(GetById(entityId));
            }
            catch { return false; }

        }

        public bool Delete(Reshteh entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long entityId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteASync(Reshteh entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteASync(long entityId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Reshteh> Get()
        {
            return _Reshtehs.Where(e => !e.IsDeleted);
        }

        public IEnumerable<Reshteh> Get(Expression<Func<Reshteh, bool>> filter = null, 
            Func<IQueryable<Reshteh>, IOrderedQueryable<Reshteh>> orderBy = null, 
            string includeProperties = "")
        {
            IQueryable<Reshteh> query = _Reshtehs;

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

        public async Task<IEnumerable<Reshteh>> GetAsync()
        {
            return await _Reshtehs.Where(e => !e.IsDeleted && e.IsVisited).ToListAsync();
        }
        public Reshteh GetById(int Id)
        {
            return _Reshtehs.Where(e => e.Id == Id).SingleOrDefault();
        }

        public Reshteh GetById(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<Reshteh> GetByIdAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Reshteh entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertASync(Reshteh entity)
        {
            throw new NotImplementedException();
        }

        public Reshteh Insert_ReturnObject(Reshteh entity)
        {
            throw new NotImplementedException();
        }

        public Task<Reshteh> Insert_ReturnObjectASync(Reshteh entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Reshteh entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateASync(Reshteh entity)
        {
            throw new NotImplementedException();
        }
    }
}
