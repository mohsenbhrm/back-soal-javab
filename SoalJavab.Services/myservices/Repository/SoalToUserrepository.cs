using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data.Entity;
using SoalJavab.DomainClasses;
using SoalJavab.DataLayer;
using SoalJavab.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using SoalJavab.Services.Models;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SoalJavab.Services
{
    public class SoalToUserRepository :  ISoalToUser
    {
        private IUserRepository _Userrepo;
        private ISoalRepository _soalRepo;
        IUnitOfWork db;
        DbSet<SoalToUser> _SoalToUser;

        public SoalToUserRepository(IUnitOfWork Context,ISoalRepository soalRepository,IUserRepository userRepository)
        {
            _Userrepo = userRepository;
            _soalRepo = soalRepository;
            this.db = Context;
            _SoalToUser = db.Set<SoalToUser>();
        }

        private IList<SoalToUser> Post_Soal_To_user_by_UserId(long userId)
        {
            var u = _Userrepo.Get_ZirReshtehId_by_UserId(userId);
            //سوالهای مرتبط با زیر رشته کاری و بزرگتر از آخرین سوال پرسیده شده از کاربر باشد
            var s = _soalRepo.GetAllByzirreshteh(u)
                .Where(x => x.Id > _soalRepo.lastSoalToUserId(userId)).ToList();
            IList<SoalToUser> StoU = new List<SoalToUser>();
            var User = _Userrepo.GetById(userId);
            foreach (var n in s)
            {
                StoU.Add(new SoalToUser
                {
                    Isdeleted = false,
                    Soal = n,
                    User = User,
                    IsVisited = true
                });
            }
            db.AddThisRange(StoU);
            //db.Addnew(StoU);
            db.SaveAllChanges();
            return StoU.ToList();
        }
        private async Task<IList<SoalToUser>> Post_Soal_To_user_by_UserIdAsync(long userId)
        {
            var u = _Userrepo.Get_ZirReshtehId_by_UserId(userId);
            //سوالهای مرتبط با زیر رشته کاری و بزرگتر از آخرین سوال پرسیده شده از کاربر باشد
            var s = await _soalRepo.GetAllByzirreshtehAsync(u);

            IList<SoalToUser> StoU = new List<SoalToUser>();
            var User = _Userrepo.GetById(userId);
            foreach (var n in s.Where(x => x.Id > _soalRepo.lastSoalToUserId(userId)))
            {
                StoU.Add(new SoalToUser
                {
                    Isdeleted = false,
                    Soal = n,
                    User = User,
                    IsVisited = true
                });
            }
            db.AddThisRange(StoU);
            //db.Addnew(StoU);
             await db.SaveAllChangesAsync();
            return StoU.ToList();
        }


        public IList<SoalToUserVM> getSoalToUserByUserId(long IdUser)
        {
            try
            {
                var q = Post_Soal_To_user_by_UserId(IdUser).Select(d => new SoalToUserVM
                {
                    SoalId = d.Soal.Id,
                    Matn = d.Soal.Matn,
                    Username = d.User.Username,
                    SoaltoUserId = d.Id,
                }).ToList();
                return q;
            }

            catch { return null; }
        }
        public IList<SoalToUserVM> getVisitedSoalToUserByUserId(long IdUser)
        {
            try
            {
                var q = db.Set<SoalToUser>().Where(x => x.User.Id.Equals(IdUser) && x.IsVisited && !x.Isdeleted & !x.Isanswered).Select(d => new SoalToUserVM
                {
                    SoalId = d.Soal.Id,
                    Matn = d.Soal.Matn,
                    Username = d.User.Username,
                    SoaltoUserId = d.Id,
                }).ToList();
                return q;
            }
            catch { return null; }
        }
        public IList<SoalToUserVM> getAnsweredSoalToUserByUserId(long IdUser)
        {
            try
            {
                var q = db.Set<SoalToUser>().Where(x => x.User.Id.Equals(IdUser) && x.IsVisited && !x.Isdeleted & x.Isanswered).Select(d => new SoalToUserVM
                {
                    SoalId = d.Soal.Id,
                    Matn = d.Soal.Matn,
                    Username = d.User.Username,
                    SoaltoUserId = d.Id,
                }).ToList();
                return q;
            }
            catch { return null; }
        }

        public async Task<IList<SoalToUserVM>> GetSoalToUserByUserIdASync(long IdUser)
        {
            try
            {
                var q = await Post_Soal_To_user_by_UserIdAsync(IdUser);

                var a =  q.
                Select(d => new SoalToUserVM
                {
                    SoalId = d.Soal.Id,
                    Matn = d.Soal.Matn,
                    Username = d.User.Username,
                    SoaltoUserId = d.Id,
                }).ToList();
                return  a;
            }
            catch { return null; }
        }

        public async Task<IList<SoalToUserVM>> getVisitedSoalToUserByUserIdASync(long IdUser)
        {
            try
            {
                var q = await db.Set<SoalToUser>().Where(x => 
                x.User.Id.Equals(IdUser)
                && x.IsVisited && !x.Isdeleted & !x.Isanswered)
                    .Select(d => new SoalToUserVM
                {
                    SoalId = d.Soal.Id,
                    Matn = d.Soal.Matn,
                    Username = d.User.Username,
                    SoaltoUserId = d.Id,
                }).ToListAsync();
                return q;
            }
            catch { return null; }
        }
        public async Task<IList<SoalToUserVM>> getAnsweredSoalToUserByUserIdASyncAsync(long IdUser)
        {
            try
            {
                var q = await db.Set<SoalToUser>().Where(x => 
                x.User.Id.Equals(IdUser)
                && x.IsVisited && !x.Isdeleted & x.Isanswered)
                    .Select(d => new SoalToUserVM
                {
                    SoalId = d.Soal.Id,
                    Matn = d.Soal.Matn,
                    Username = d.User.Username,
                    SoaltoUserId = d.Id,
                }).ToListAsync();
                return q;
            }
            catch { return null; }
        }

        public IEnumerable<SoalToUser> Get(Expression<Func<SoalToUser, bool>> filter = null,
            Func<IQueryable<SoalToUser>, IOrderedQueryable<SoalToUser>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<SoalToUser> query = _SoalToUser;

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

        public IEnumerable<SoalToUser> Get()
        {
            throw new NotImplementedException();
        }

        public SoalToUser GetById(long Id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(SoalToUser entity)
        {
            throw new NotImplementedException();
        }

        public SoalToUser Insert_ReturnObject(SoalToUser entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(SoalToUser entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(SoalToUser entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long entityId)
        {
            throw new NotImplementedException();
        }

        public Task<SoalToUser> GetByIdAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertASync(SoalToUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<SoalToUser> Insert_ReturnObjectASync(SoalToUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateASync(SoalToUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteASync(SoalToUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteASync(long entityId)
        {
            throw new NotImplementedException();
        }
    }
}
