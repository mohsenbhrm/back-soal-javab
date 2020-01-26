using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data.Entity;
using SoalJavab.DomainClasses;
using SoalJavab.DataLayer;
using SoalJavab.Services.Contracts;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SoalJavab.Services
{
    public partial class UserRepository : IUserRepository
    {
        private IUnitOfWork db;
        private DbSet<ApplicationUser> _user;
        public UserRepository(IUnitOfWork db)
        {
            this.db = db;
            _user = db.Set<ApplicationUser>();
        }
        public long Tedad(DateTime dt)
        {
            return 0;
            //return db.Users.Where(x => (x.Regdate >= dt)).LongCount();
        }
        public long Tedad(DateTime dt1, DateTime dt2)
        {
            return 0;
            //return db.Users.Where(x => x.Regdate >= dt1 && x.Regdate <= dt2).LongCount();
        }

        public IEnumerable<ApplicationUser> GetallbyDate(DateTime dt)
        {
            return null;
            //return db.Users.Where(x => (x.Regdate >= dt)).ToList();
        }
        public IEnumerable<ApplicationUser> GetallbyReshteh(ZirReshteh ZR)
        {
            try
            {


                var w = db.Set<ReshtehUser>()
                            .Where(x => x.ZirReshtehId.Equals(ZR.Id) && !x.IsDeleted)
                            .Select(c => c.User).ToList();
                return w;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<ApplicationUser> GetallbyReshtehId(long Id = 0)
        {
            try
            {
                if (Id > 0)
                {
                    var zr = db.Set<ReshtehUser>()
                        .Where(x => !x.IsDeleted && x.ZirReshtehId.Equals(Id))
                        .Select(c => c.User).ToList();
                    return zr;
                }
                else { return null; }
            }
            catch { return null; }
        }

        public long[] Get_ZirReshtehId_by_UserId(long UserId)
        {
            return db.Set<ReshtehUser>()
                .Where(x => x.User.Id.Equals(UserId) && !x.IsDeleted)
                .Select(c => c.ZirReshtehId)
                .ToArray();
        }

        public IEnumerable<ApplicationUser> Get(Expression<Func<ApplicationUser, bool>> filter = null, Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> Get()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetById(long Id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Insert_ReturnObject(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long entityId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetByIdAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertASync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> Insert_ReturnObjectASync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateASync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteASync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteASync(long entityId)
        {
            throw new NotImplementedException();
        }
    }
}