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
    public class JavabServices : IJavabServices
    {
        private IUnitOfWork db;
        private ISoalServices _soals;
        private IUsersService _users;
        private DbSet<Javab> _javabs;
       // private ISoalServices _soals;

        public JavabServices(IUnitOfWork unitOfWork,
            ISoalServices soalserv,
            ITagRepository tagRepository,
            IUsersService usersService
            )
        {
            db = unitOfWork;
            _soals = soalserv;
            _users = usersService;
            _javabs = db.Set<Javab>();
        }
        
        public Javab GetById(long Id) => _javabs.Find(Id);
        public JavabVM GetJavabById(long Id)
        {
            return _javabs.Where(x => x.Id.Equals(Id))
            .Select(i => new JavabVM
            {
                Id = i.Id,
                IdSoal = i.Soal.Id,
                IdUser = i.User.Id,
                Matn = i.Matn,
                Username = i.User.Username
            }).FirstOrDefault();
        }
        public IEnumerable<JavabVM> getJavabofsoalBysoalId(long IdSoal)
        {
            try
            {
                var t = db.Set<Javab>().Where(x => !x.IsDeleted && x.Soal.Id == IdSoal)
                    .Select(d => new JavabVM
                    {
                        Id = d.Id,
                        Matn = d.Matn,
                        IdSoal = d.Soal.Id,
                        Username = d.User.Username
                    }).ToList();
                return t.ToList();
            }
            catch { return null; }
        }
        public JavabCountVM getJavabCountOfSoal(long IdSoal)
        {
            try
            {

                var t = db.Set<Javab>().Where(x => !x.IsDeleted && x.Soal.Id == IdSoal).Count();
                var jcvm = new JavabCountVM
                {
                    Count = t,
                    Soal_Id = IdSoal
                };
                return jcvm;
            }
            catch { return null; }
        }
        public long getCountJavabCountOfSoal(long IdSoal)
        {
            try
            {

                var t = db.Set<Javab>().Where(x => !x.IsDeleted && x.Soal.Id == IdSoal).Count();

                return t;
            }
            catch { return 0; }
        }
        public JavabVM getjavabforCreat(string IdSoal = "")
        {
            JavabVM jv = new JavabVM();
            jv.IdSoal = long.Parse(IdSoal);
            return jv;
        }
        public bool Creatjavab(JavabVM jv)
        {
            try
            {
                var s = _users.GetCurrentUserAsync().Result;
                var sl = _soals.GetById(jv.IdSoal);
                if (sl == null || s == null)  return false; 
                db.Addnew<Javab>(new Javab
                {
                    Matn = jv.Matn,
                    IsDeleted = false,
                    Isvisited = false,
                    Soal = sl,
                    User = s,
                    RegDate = DateTime.Now
                });
                db.SaveAllChanges();
                return true;
            }
            catch { return false; }
        }
        
        // public long GetById(long id)
        // {

        //     var q = _javabs.Find(id).User.Id;
        //     return q;
        // }
        public bool isJavabOfuser(long javabId, long userId)
        {
           var q = _javabs.Find(javabId).User.Id;
            // var q1 = GetById(javabId).use //_javabs.Find(javabId).User;// .Where(x=>x.Id == javabId).FirstOrDefault().User;
            if (q>0)
            {
            var qw = GetById(javabId);
            if (q != 0 && q.Equals(userId))
                return true;
            else return false;
            }
            return false;

        }
        public bool EditeJavab(JavabVM javab)
        {
            if (javab != null)
            {
                var q = GetById(javab.Id);// _javabs.Where(x=>x.Id == javab.Id).FirstOrDefault();
                if (q == null)
                {
                    return false;
                    // else return false;
                }
                q.Matn = javab.Matn;
                db.MarkAsChanged(q);
                db.SaveAllChanges();
                return true;
            }
            return false;

        }

        public IList<JavabVM> GetAllJavabsByuserId(long userId)
        {
            var user = _users.FindUserAsync(userId).Result;
            var q = _javabs.Include(u=>u.User).Where(x => !x.IsDeleted && x.User == user).Include(s=>s.Soal)
            .Select(d => new JavabVM
            {
                Matn = d.Matn,
                Id = d.Id,
                IdSoal = d.Soal.Id,
                Username = d.User.Username
            }).ToList();
            if (q == null)
            {
                return null;
            }
            return q.ToList();
        }
        public bool Delete(long JavabId)
        {
            try
            {
                var q = GetById(JavabId);
                if (q == null) return false;
                q.IsDeleted = true;
                _javabs.Attach(q);
                db.MarkAsChanged<Javab>(q);
                db.SaveAllChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
