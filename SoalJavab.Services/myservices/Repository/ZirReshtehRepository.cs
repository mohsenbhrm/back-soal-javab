using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data.Entity;
using SoalJavab.DomainClasses;
using SoalJavab.DataLayer;
using SoalJavab.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SoalJavab.Services
{
    public class ZirReshtehRepository : IZirReshteh
    {
        IUnitOfWork db;
        DbSet<ZirReshteh> _ZirReshteh;
        public ZirReshtehRepository(IUnitOfWork Context)
        {
            this.db = Context;
            _ZirReshteh = db.Set<ZirReshteh>();
        }

        public bool Delete(long entityId)
        {
            try
            {
                _ZirReshteh.Find(entityId).IsDeleted = true;
                return true;
            }
            catch { return false; }
        }

        public bool Delete(ZirReshteh entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteASync(ZirReshteh entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteASync(long entityId)
        {
            throw new NotImplementedException();
        }

        public IList<ZirReshteh> Get(long ReshtehId)
        {
            var q =  _ZirReshteh
               .Where(x => !x.IsDeleted && x.IsVisited && x.Reshteh.Id ==  ReshtehId)
               .ToList();
            return q;
        }

        public IEnumerable<ZirReshteh> Get(Expression<Func<ZirReshteh, bool>> filter = null,
            Func<IQueryable<ZirReshteh>, IOrderedQueryable<ZirReshteh>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<ZirReshteh> query = _ZirReshteh;

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

        public IEnumerable<ZirReshteh> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ZirReshteh>> GetAsync(long ReshtehId)
        {
            var q = await _ZirReshteh
                .Where(x => !x.IsDeleted && x.IsVisited && x.Reshteh.Id == ReshtehId)
                .ToListAsync();
            return q;
        }

        public ZirReshteh GetById(long ZirReshtehId)
        {
            return _ZirReshteh.Where(e => e.Id == ZirReshtehId).FirstOrDefault();
        }
        public  async Task<ZirReshteh> GetByIdAsync(long ZirReshtehId) => 
            await _ZirReshteh.Where(e => e.Id == ZirReshtehId).FirstOrDefaultAsync();

        public bool Insert(ZirReshteh entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertASync(ZirReshteh entity)
        {
            throw new NotImplementedException();
        }

        public ZirReshteh Insert_ReturnObject(ZirReshteh entity)
        {
            throw new NotImplementedException();
        }

        public Task<ZirReshteh> Insert_ReturnObjectASync(ZirReshteh entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(ZirReshteh entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateASync(ZirReshteh entity)
        {
            throw new NotImplementedException();
        }
    }
}
