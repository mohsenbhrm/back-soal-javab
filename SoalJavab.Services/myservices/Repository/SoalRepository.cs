using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data.Entity;
using System.Threading.Tasks;
using SoalJavab.DomainClasses;
using SoalJavab.DataLayer;
using SoalJavab.Services.Contracts;
using SoalJavab.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SoalJavab.Services
{
    public class SoalRepository : ISoalRepository
    {
        private IUnitOfWork db;
        ITagRepository _Tag;
        DbSet<Soal> _SoalDbSet;
        public SoalRepository(IUnitOfWork db, ITagRepository tag)
        {
            this.db = db;
            _Tag = tag;
            _SoalDbSet = db.Set<Soal>();
        }
        public bool Insert(Soal entity, long[] tag)
        {
            try
            {
                IList<TagSoal> ts = new List<TagSoal>();
                foreach (var n in tag)
                {
                    var s = _Tag.GetById(n);
                    ts.Add(new TagSoal
                    {
                        Tag = s
                    });
                }
                entity.TagSoal = ts;
                entity.Regdat = DateTime.Now;
                if (Insert(entity))
                {
                   var q = db.Set<TagSoal>();
                   q.AddRange(entity.TagSoal);
                   db.SaveAllChanges();
                    // soalToUser(entity, user);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public bool Insert(Soal entity, long[] tag, IList<ApplicationUser> user)
        {
            try
            {
                // var us = db.Set<ApplicationUser>().Find(1);
                //  entity.User = us;
                IList<TagSoal> ts = new List<TagSoal>();
                foreach (var n in tag)
                {
                    var s = _Tag.GetById(n);
                    ts.Add(new TagSoal
                    {
                        Tag = s
                    });
                }
                entity.TagSoal = ts;
                entity.Regdat = DateTime.Now;
                if (Insert(entity))
                {
                    SoalToUser(entity, user);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }
        public bool newInsert(Soal entity, long[] tag, IList<ApplicationUser> user)
        {
            try
            {
                // var us = db.Set<ApplicationUser>().Find(1);
                //  entity.User = us;
                IList<TagSoal> ts = new List<TagSoal>();
                foreach (var n in tag)
                {
                    var s = _Tag.GetById(n);
                    ts.Add(new TagSoal
                    {
                        Tag = s
                    });
                }
                entity.TagSoal = ts;
                entity.Regdat = DateTime.Now;
                if (Insert(entity))
                {
                    SoalToUser(entity, user);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public bool SoalToUser(Soal soal, IList<ApplicationUser> user)
        {
            foreach (var e in user)
            {
                db.Set<SoalToUser>().Add(new SoalToUser { User = e, Soal = soal });
            }
            return true;
        }

        public bool Follow(long UserID, long SoalID)
        {
            try
            {
                var u = db.Set<ApplicationUser>().Find(UserID); ///  new UserRepository(db).GetById(UserID);
                //db.Users.FirstOrDefault(e => e.Id.Equals(UserID));
                var s = GetById(SoalID);
                if (s != null && u != null)
                {
                    var f = new SoalFollower();
                    f.ApplicationUser = u;
                    f.SoalId = s.Id;
                    //db.SoalFollowers.Add(f);
                    db.Addnew<SoalFollower>(f);
                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Soal> GetAllByTag(long TagId)
        {
            var q = from c in _SoalDbSet.Where(e => !e.IsDeleted)
                    join a in db.Set<TagSoal>() on c.Id equals a.Soal.Id
                    where a.Tag.Id == TagId
                    select c;
            return q.ToList();
        }

        public IEnumerable<Soal> GetAllByUserID(long UserID)
        {
            return _SoalDbSet.Where(x => x.User.Id == UserID && !x.IsDeleted);
        }
        public IEnumerable<Soal> GetAllDeletedByUserID(long UserID)
        {
            return _SoalDbSet.Where(x => x.User.Id == UserID);
        }
        public async Task<IEnumerable<Soal>> GetAllByUserIdAsync(long UserID)
        {
            return await _SoalDbSet.Where(x => x.User.Id == UserID && !x.IsDeleted).ToListAsync();
        }

        public IEnumerable<Soal> GetAllByzirreshteh(long ZirReshtehID)
        {
            try
            {

                var w = db.Set<Tag>()
                    .Where(c => !c.IsDeleted && c.ZirReshtehId == ZirReshtehID)
                    .Select(c => c.Id).ToArray();
                var e = _SoalDbSet.Where(x =>
                                w.Contains(x.TagSoal.FirstOrDefault().TagId)
                                && !x.IsDeleted);
                return e.ToList();
            }
            catch { return null; }
        }

        public IEnumerable<Soal> GetAllByzirreshteh(long[] ZirReshtehID)
        {
            try
            {
                var q = _SoalDbSet.Where(x => ZirReshtehID.Contains(x.ZirReshtehId) && !x.IsDeleted);
                return q.ToList();
            }
            catch { return null; }
        }

        public async Task<IEnumerable<Soal>> GetAllByzirreshtehAsync(long[] ZirReshtehID)
        {
            try
            {
                var q = await _SoalDbSet.Where(x => ZirReshtehID.Contains(x.ZirReshtehId) && !x.IsDeleted).ToListAsync();
                return q;
            }
            catch { return null; }
        }

        public long lastSoalToUserId(long Id)
        {
            try
            {
                var q = db.Set<SoalToUser>().Where(x => x.User.Id.Equals(Id)).ToList().LastOrDefault();

                var w1 = q.Soal.Id;
                return w1;
            }
            catch
            {
                return 0;
            }
        }

        public IEnumerable<ApplicationUser> GetAllFollowers(long SoalID)
        {
            try
            {
                var q = from s in db.Set<ApplicationUser>().Where(e => !e.Ban)
                        join a in db.Set<SoalFollower>().Where(e => !e.IsDeleted && e.Soal.Id.Equals(SoalID))
                        on s.Id equals a.ApplicationUser.Id
                        select s;
                return q.ToList();
            }
            catch { return null; }

        }

        public bool DeletebyId(long Id)
        {
            var s = _SoalDbSet.Where(x => x.Id == Id).FirstOrDefault();
            if (s != null)
            {
                s.IsDeleted = true;
                if (Update(s))
                {
                    db.SaveAllChanges();
                    return true;
                }
            }
            return false;
        }
        
#region Async

        public Task<bool> FollowAsync(long UserID, long SoalID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetAllFollowersAsync(long SoalID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Soal>> GetAllByzirreshtehAsync(long ZirReshtehID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Soal>> GetAllByTagAsync(long TagId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Soal>> GetAllByUserIDAsync(long UserID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Soal>> GetAllByUserIdAsyncAsync(long UserID)
        {
            throw new NotImplementedException();
        }

        public Task<long> LastSoalToUserIdAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Soal entity, long[] tag, IList<ApplicationUser> user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(Soal entity, long[] tag)
        {
            try
            {
                IList<TagSoal> ts = new List<TagSoal>();
                foreach (var n in tag)
                {
                    var s = await _Tag.GetByIdAsync(n);
                    ts.Add(new TagSoal
                    {
                        Tag = s
                    });
                }
                entity.TagSoal = ts;
                entity.Regdat = DateTime.Now;
                if (Insert(entity))
                {
                    // soalToUser(entity, user);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public async Task<bool> DeletebyIdAsync(long Id)
        {
            var s = await _SoalDbSet.Where(x => x.Id == Id).FirstOrDefaultAsync();
            s.IsDeleted = true;
            if (Update(s))
            {
                await db.SaveAllChangesAsync();
                return true;
            }
            return false;
        }

        public IEnumerable<Soal> Get(Expression<Func<Soal, bool>> filter = null,
            Func<IQueryable<Soal>, IOrderedQueryable<Soal>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<Soal> query = _SoalDbSet;

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

        public IEnumerable<Soal> Get()
        {
            throw new NotImplementedException();
        }

        public Soal GetById(long Id)
        {
            return _SoalDbSet.Find(Id);
        }

        public bool Insert(Soal entity)
        {
            db.Addnew(entity);
            db.SaveAllChanges();
            return true;
        }

        public Soal Insert_ReturnObject(Soal entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Soal entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Soal entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long entityId)
        {
            throw new NotImplementedException();
        }

        public Task<Soal> GetByIdAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertASync(Soal entity)
        {
            throw new NotImplementedException();
        }

        public Task<Soal> Insert_ReturnObjectASync(Soal entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateASync(Soal entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteASync(Soal entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteASync(long entityId)
        {
            throw new NotImplementedException();
        }
    }
}
#endregion