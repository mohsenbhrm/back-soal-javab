using Microsoft.EntityFrameworkCore;
using SoalJavab.DataLayer;
using SoalJavab.DomainClasses;
using SoalJavab.Services.Contracts;
using SoalJavab.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoalJavab.Services.myservices
{
    public class SoalToUserservices : ISoalToUserServices

    {
        private IUnitOfWork db;
        private ISoalRepository _soalRepository;
        private ITagRepository _tagRepository;
        private IUserRepository _Userrepository;
        private IUsersService _users;
        private ISoalServices _soal;
        private DbSet<SoalToUser> _soaltouser;

        public SoalToUserservices(IUnitOfWork unitOfWork, ITagRepository tag,
            ISoalRepository soalrepo,
            ITagRepository tagRepository,IUserRepository userRepository,
            IUsersService usersService,ISoalServices soalSevices)
        {
            db = unitOfWork;
            _soalRepository = soalrepo;
            _tagRepository = tagRepository;
            _Userrepository = userRepository;
            _users = usersService;
            _soal = soalSevices;
            _soaltouser = db.Set<SoalToUser>();
        }
        public IList<SoalToUserVM> GetnewSoalTouserByIdUser(long Iduser)
        {
            try
            {
                var q = _Get_new_Soal_To_user_by_UserId(Iduser).ToList();
                return q;
            }
            catch { return null; }
        }
         public IList<SoalToUserVM> GetnewFeedSoalTouserByIdUser(long Iduser)
        {
            try
            {
                var q = _Get_new_feed_Soal_To_user_by_UserId(Iduser).ToList();
                return q;
            }
            catch { return null; }
        }
        public IList<SoalToUserVM> getSoalToUserByUserID(long LastIdsoal,long IdUser)
        {   
            try {
             return _LoadMoreSoalToUserByLastId(LastIdsoal,IdUser);
            }
            catch { return null; }
        }
        public long lastSoalToUserId(long Id)
        {
            try
            {
                var q = _soaltouser.Where(o => o.User.Id == Id).LastOrDefault();
                var w1 = q.Soal.Id;
                return w1;
            }
            catch
            {
                return 0;
            }
        }
        private IList<SoalToUserVM> _Get_new_Soal_To_user_by_UserId(long userId)
         {
             var User = _users.GetCurrentUserAsync().Result;

            var stu = new List<SoalToUserVM>();
            var u = _Userrepository.Get_ZirReshtehId_by_UserId(userId);
            //سوالهای جدید را بر اساس شناسه سوال که بزگتر از آخرین سوال پرسیده شده از کاربر باشه رو جدا میکند
            var sq = _soal.GetAllByzirreshteh(u.FirstOrDefault())
            .Where(x => x.ApplicationUserId != userId).ToList();

            foreach (var n in u.Skip(1))
            {
                var q = _soal.GetAllByzirreshteh(n)
                .Where(x => x.ApplicationUserId != userId).ToList();

                sq.Concat(q);
            }
            if (sq.Count() < 1)
            { return stu; }
             stu = sq.Select(x=> new SoalToUserVM {
                Username = x.User.Username,
                SoalId = x.Id,
                Matn = x.Matn,
                regDate = x.Regdat,
            }).OrderByDescending(x=>x.SoalId).ToList();
            return stu;
        }
         private IList<SoalToUserVM> _Get_new_feed_Soal_To_user_by_UserId(long userId)
         {
             var User = _users.GetCurrentUserAsync().Result;

            var stu = new List<SoalToUserVM>();
            var u = _Userrepository.Get_ZirReshtehId_by_UserId(userId);
            //سوالهای جدید را بر اساس شناسه سوال که بزگتر از آخرین سوال پرسیده شده از کاربر باشه رو جدا میکند
            var sq = _soal.GetAllByzirreshteh(u.FirstOrDefault())
            .Where(x => x.ApplicationUserId != userId && x.Regdat > User.LastLoggedIn).ToList();

            foreach (var n in u.Skip(1))
            {
                var q = _soal.GetAllByzirreshteh(n)
                .Where(x => x.ApplicationUserId != userId && x.Regdat > User.LastLoggedIn).ToList();

                sq.Concat(q);
            }
            if (sq.Count() < 1)
            { return stu; }
             stu = sq.Select(x=> new SoalToUserVM {
                Username = x.User.Username,
                SoalId = x.Id,
                Matn = x.Matn,
                regDate = x.Regdat,
            }).OrderByDescending(x=>x.SoalId).ToList();
            return stu;
        }
        private IList<SoalToUserVM> _LoadMoreSoalToUserByLastId(long LastSoalId, long userId)
        {
            var stu = new List<SoalToUserVM>();
            var u = _Userrepository.Get_ZirReshtehId_by_UserId(userId);
            //سوالهای جدید را بر اساس شناسه سوال که بزگتر از آخرین سوال پرسیده شده از کاربر باشه رو جدا میکند
            var sq = _soal.GetAllByzirreshteh(u.FirstOrDefault())
            .Where(x => x.ApplicationUserId != userId && x.Id < LastSoalId).ToList();

            foreach (var n in u.Skip(1))
            {
                var q = _soal.GetAllByzirreshteh(n)
                .Where(x => x.ApplicationUserId != userId && x.Id < LastSoalId).ToList();

                sq.Concat(q);
            }
            if (sq.Count() < 1)
            { return stu; }
            var User = _users.GetCurrentUserAsync().Result;
            stu = sq.Select(x => new SoalToUserVM
            {
                Username = x.User.Username,
                SoalId = x.Id,
                Matn = x.Matn,
                regDate = x.Regdat,
            }).TakeLast(10).ToList();
            return stu.OrderByDescending(x=>x.SoalId).ToList();
        }
       
       
        //  private IList<SoalToUser> Post_Soal_To_user_by_UserId(long userId)
        // {
        //     var stu = new List<SoalToUser>();
        //     var u = _Userrepository.Get_ZirReshtehId_by_UserId(userId);
        //     //سوالهای جدید را بر اساس شناسه سوال که بزگتر از آخرین سوال پرسیده شده از کاربر باشه رو جدا میکند
        //     var sq = _soal.GetAllByzirreshteh(u.FirstOrDefault())
        //     .Where(x => x.ApplicationUserId != userId && x.Id > lastSoalToUserId(userId)).ToList();

        //     foreach (var n in u.Skip(1))
        //     {
        //         var q = _soal.GetAllByzirreshteh(n)
        //         .Where(x => x.ApplicationUserId != userId && x.Id > lastSoalToUserId(userId)).ToList();

        //         sq.Concat(q);
        //     }
        //     if (sq.Count() < 1)
        //     { return stu; }
        //     var User = _users.GetCurrentUserAsync().Result;
        //      stu = sq.Select(x=> new SoalToUser {
        //         User = User,
        //         Soal = x,
        //         Isanswered =  false ,
        //         Isdeleted = false,
        //         IsVisited = false
        //     }).ToList();
        //     db.Set<SoalToUser>().AddRange(stu);
        //     db.SaveAllChanges();
        //     return stu.ToList();
        // }
        public IList<Soal> Post_Soal_To_user_by_UserId_10(long userId)
        {
            var u = _Userrepository.Get_ZirReshtehId_by_UserId(userId);
            //سوالهای جدید را بر اساس شناسه سوال که بزگتر از آخرین سوال پرسیده شده از کاربر باشه رو جدا میکند
            var sq = _soal.GetAllByzirreshteh(u.FirstOrDefault())
            .Where(x => x.ApplicationUserId != userId && x.Id > lastSoalToUserId(userId)).ToList();

            foreach (var n in u.Skip(1))
            {
                var q = _soal.GetAllByzirreshteh(n)
                .Where(x => x.ApplicationUserId != userId && x.Id > lastSoalToUserId(userId)).ToList();

                sq.Concat(q);
            }
            if (sq.Count() < 1)
            { return null; }
            var User = _users.GetCurrentUserAsync().Result;
            foreach (var n in sq.Take(10))
            {
                db.Addnew(
                    new SoalToUser
                    {
                        Isdeleted = false,
                        Soal = n,
                        User = User,
                        IsVisited = true
                    });
            }
            db.SaveAllChanges();
            return sq.Take(10).ToList();
        }
        // نمایش 10 سوال پرسیده شده از کاربر
        public IList<SoalToUserVM> getSoalToUserByUserId_10(long IdUser)
        {
            try
            {
                var q = Post_Soal_To_user_by_UserId_10(IdUser).Select(d => new SoalToUserVM
                {
                    SoalId = d.Id,
                    Matn = d.Matn,
                    Username = d.User.Username,
                }).ToList();

                return q;
            }
            catch { return null; }
        }
        private IEnumerable<SoalToUser> SoalToUserByUserId(long IdUser)
        {
            try
            {
                var t = db.Set<SoalToUser>()
                    .Where(x => !x.Isdeleted && x.User.Id == IdUser && x.IsVisited == false);
                return t;
            }
            catch { return null; }
        }
        public SoalToUserVM getSoalTouserByStoUId(long Id, long userId)
        {
            var t = db.Set<SoalToUser>().Where(x => x.Id == Id && x.User.Id.Equals(userId)).FirstOrDefault();
            if (t != null)
            {
                var s = new SoalToUserVM
                {
                    SoalId = t.Soal.Id,
                    Matn = t.Soal.Matn,
                    SoaltoUserId = t.Id,
                    Username = t.User.Username
                };
                return s;
            }
            return null;
        }
        public long CountofSoasToUser(long userId)
        {
            var q = _Userrepository.Get_ZirReshtehId_by_UserId(userId);
            long s = 0;
            foreach (var n in q)
            {
                s += _soalRepository.GetAllByzirreshteh(n)
                    .Where(x =>
                    !x.SoalToUser.Select(f => f.Soal.Id).Contains(x.Id)).Count();
            }
            return s;
        }
        public bool DeleteSoalToUserById(long StoUId)
        {
            try
            {
                var q = db.Set<SoalToUser>().Where(x => x.Id == StoUId).FirstOrDefault();
                if (q != null)
                {
                    q.Isdeleted = true;
                    db.SaveAllChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }
        public long Count_soal_To_User(long Iduser)
        {
            var user = _Userrepository.GetById(Iduser);
            var soals = _soalRepository.Get(
                x => !x.IsDeleted &&
                x.Regdat > user.visitedDate &&
                 user.ReshtehUser
                    .Select(c => c.ZirReshtehId)
                    .Contains(x.TagSoal.FirstOrDefault().Tag.ZirReshtehId))
                 .LongCount();
            return soals;
        }

        public IList<Soal> List_soal_To_User(long Iduser)
        {
            var user = _Userrepository.GetById(Iduser);
            var soals = _soalRepository.Get(
                x => !x.IsDeleted &&
                x.Regdat > user.visitedDate &&
                 user.ReshtehUser
                    .Select(c => c.ZirReshtehId)
                    .Contains(x.TagSoal.FirstOrDefault().Tag.ZirReshtehId))
                 .ToList();
            return soals;
        }

    }
}
