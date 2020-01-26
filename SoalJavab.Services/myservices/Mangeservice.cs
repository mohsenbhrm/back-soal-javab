using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoalJavab.Services;
using SoalJavab.Services.Contracts;
using SoalJavab.DataLayer;
//using System.Web.Mvc;
using SoalJavab.DomainClasses;
using SoalJavab.Services.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using SoalJavab.Services.Models.old;

namespace SoalJavab.Services
{
    public class Mangeservice 
    {
        IUnitOfWork db;
        ISoalRepository _soalRepository;
        IUserRepository _Userrepository;
        IJavabRepository _javabrepository;
        ISoalToUser _soalToUserRepository;
        IZirReshteh _zirReshtehRepository;
        public Mangeservice(IUnitOfWork unitOfWork, ISoalRepository soalrepo, IUserRepository userRepo, IJavabRepository javbRepo, ISoalToUser soaltouserRepo, IZirReshteh zirReshteRepo)
        {
            db = unitOfWork;
            _soalRepository = soalrepo;
            _Userrepository = userRepo;
            _javabrepository = javbRepo;
            _soalToUserRepository = soaltouserRepo;
            _zirReshtehRepository = zirReshteRepo;
        }

        public void Check_visited_Soals_To_User(long[] IdSoals)
        {
            if (IdSoals == null)
            {
                throw new NullReferenceException();
            }
            foreach (var i in IdSoals)
            {
                if (!Check_visited_Soals_To_User(i)) throw new OperationCanceledException();
            }
            db.SaveAllChanges();
        }
        public long Count_All_Soals_of_User(long IdUser)
        {
            try
            {
                var q = _soalRepository.GetAllByUserID(IdUser).Count();
                return q;
            }
            catch { throw new OperationCanceledException(); }
        }
        public long Count_All_Soals_of_User_GetJavab(long IdUser)
        {
            try
            {
                return _soalRepository.GetAllByUserID(IdUser)
                    .Where(x => !x.IsDeleted && x.Javab != null).LongCount();
            }
            catch { throw new NotImplementedException(); }

        }
        public long Count_All_Soals_To_User(long IdUser)
        {
            try
            {
                return _soalToUserRepository.Get(x => !x.Isdeleted && x.User.Id == IdUser).LongCount();
                // return db.Set<SoalToUser>().Where(x => !x.Isdeleted && x.User.Id == IdUser).LongCount();
            }
            catch { throw new NotImplementedException(); }
        }
        public long Count_Soals_To_User_getJavab(long IdUser)
        {
            return _soalToUserRepository
                .Get(x => !x.Isdeleted && x.User.Id == IdUser)
                .Where(c => c.Soal.Javab != null).LongCount();
            throw new NotImplementedException();
        }
        public long Count_Soals_of_User_GetJavab_notVisited(long IdUser)
        {
            return _soalRepository.GetAllByUserID(IdUser)
                .Where(x => x
                    .Javab.Where(c => !c.Isvisited) != null)
                .LongCount();
            throw new NotImplementedException();
        }
        public long Count_Soals_To_User_notVisited(long IdUser)
        {
            return _soalToUserRepository.Get(x => !x.IsVisited && x.User.Id.Equals(IdUser)).LongCount();
            throw new NotImplementedException();
        }

        public bool Delete_soal_Of_user(long IdSoal)
        {
            try
            {
                var q = _soalRepository.GetById(IdSoal);
                q.IsDeleted = true;
                db.SaveAllChanges();
                return true;
            }
            catch { throw new NotImplementedException(); }
        }

        private bool Delete_soal_To_User(long IdSoal)
        {
            try
            {
                var q = _soalToUserRepository.GetById(IdSoal);
                q.Isdeleted = true;
                db.MarkAsChanged<SoalToUser>(q);
                return true;
            }
            catch { throw new NotImplementedException(); }
        }
        public bool Delete_soal_To_User(long[] IdSoal)
        {
            try
            {
                foreach (var i in IdSoal)
                {
                    if (!Delete_soal_Of_user(i)) throw new NotImplementedException();
                }
                db.SaveAllChanges();
                return true;
            }
            catch { throw new NotImplementedException(); }
        }

        public IList<Soal> List_Deleted_Soals_Of_user(long IdUser)
        {
            try
            {
                var q = _soalRepository.Get(x => x.ApplicationUserId == IdUser && x.IsDeleted).ToList();
                return q;
            }
            catch { throw new NotImplementedException(); }

        }

        public IList<Javab> List_Javab_Of_soal_To_User(long IdUser)
        {
            try
            {
                var q = _javabrepository.GetAllByUserID(IdUser).ToList();
                return q;
            }
            catch { throw new NotImplementedException(); }
        }

        public IList<Soal> List_soals_Of_user(long IdUser)
        {
            try
            {
                var q = _soalRepository.GetAllByUserID(IdUser).Where(x => !x.IsDeleted).ToList();
                return q;
            }
            catch { throw new NotImplementedException(); }
        }
        public IList<Soal> List_soals_To_user(long IdUser)
        {
            try
            {
                var q = _soalToUserRepository.Get(x => x.User.Id == IdUser).Select(c => c.Soal).ToList();
                return q;
            }
            catch { throw new NotImplementedException(); }
        }
        private bool Check_visited_Soals_To_User(long IdSoal)
        {
            try
            {
                var q = _soalRepository.GetById(IdSoal);
                q.IsVisited = true;
                return true;
            }
            catch { return false; }
        }
        public ReshtehVmold Creat_ModelView_For_Manage_Reshteh()
        {
            var q = db.Set<Reshteh>().Where(x => !x.IsDeleted);
            var vm = new ReshtehVmold();
            vm.Reshteh = new List<SelectListItem>();
            foreach (var n in q)
            {
                vm.Reshteh.Add(new SelectListItem { Text = n.Onvan, Value = n.Id.ToString() });
            }
            return vm;
        }
        public List<ZirReshteVm> list_zirReshte_By_reshtehId(long ReshteId)
        {
            if (db.Set<Reshteh>().Find(ReshteId) == null)
            {
                return null;
            }
            var q = db.Set<ZirReshteh>()
                .Where(x => !x.IsDeleted && x.Reshteh.Id == ReshteId)
                .Select(c => new ZirReshteVm { Id = c.Id, Name = c.Onvan }).ToList();
            return q;
        }
        public bool Post_zirreshteh_for_user_old(long[] ZRId, long UserId)
        {
            var user = _Userrepository.GetById(UserId);
            if (ZRId != null)
            {
                var zir = db.Set<ReshtehUser>().Where(z => z.ApplicationUserId.Equals(UserId));
                var zr = (from c in zir.Where(x => !ZRId.Contains(x.ZirReshtehId)) select c).ToList();

                zr.ForEach(x => x.IsDeleted = true);
                ZRId.Except(zir.Where(x => !x.IsDeleted).Select(x => x.ZirReshtehId));
                //soalVM.Tags.Except(tags.Where(x => !x.Isdeleted).Select(x => x.Id));
                //یک فیلتر یا کوئری برای اضافه کردن رکورد جدید به جدول
                //تگ سوال برای تگهای جدید اضافه
                //شده توسط کاربر باید نوشته شد
                foreach (var n in ZRId)
                {
                    db.Addnew<ReshtehUser>(new ReshtehUser { IsDeleted = false, ZirReshtehId = n, ApplicationUserId = user.Id });
                }
            }
            else
            {
                var tg = db.Set<ReshtehUser>().Where(x => x.ApplicationUserId.Equals(user.Id)).ToList();
                tg.ForEach(x => x.IsDeleted = true);
            }
            db.SaveAllChanges();
            return true;
        }
        public IList<Models.JsonVm> getZirReshteh_JsonVM(string Id, long userId)
        {
            var idnew = Int64.Parse(Id);
            var q = db.Set<ReshtehUser>()
                .Where(x => !x.IsDeleted && x.ApplicationUserId.Equals(userId))
                .Select(c => c.ZirReshtehId);
            var s = new GenericRepository<ZirReshteh>(db)
                .Get(x => !x.IsDeleted &&
                x.Reshteh.Id.Equals(idnew) &&
                !q.Contains(x.Id))
                .Select(x => new Models.JsonVm { name = x.Onvan, Id = x.Id }).ToList();
            return s;
        }
        public IList<Models.JsonVm> getuserZirReshteh_JsonVM(string Id, long UserId)
        {
            var idnew = Int64.Parse(Id);
            var q = db.Set<ReshtehUser>()
                .Where(x => !x.IsDeleted && x.ApplicationUserId.Equals(UserId))
                .Select(c => c.ZirReshtehId);
            var s = db.Set<ZirReshteh>().Where(x => !x.IsDeleted &&
                 x.Reshteh.Id.Equals(idnew) &&
                 q.Contains(x.Id))
                .Select(x => new Models.JsonVm { name = x.Onvan, Id = x.Id }).ToList();
            return s;
        }
        
        public bool Post_zirreshteh_for_user(long[] ZRId, long UserId , long reshtehId)
        {
            var user = _Userrepository.GetById(UserId);
            //sl.Matn = soalVM.Matn;
            if (reshtehId <= 0)
                return false;
            if (ZRId != null)
            {
                var reshteid = _zirReshtehRepository.GetById(ZRId.FirstOrDefault()).Reshteh.Id;
                if (reshtehId != reshteid) return false;
                var zir = db.Set<ReshtehUser>()
                    .Where(z => z.ApplicationUserId.Equals(UserId) && z.ZirReshteh.Reshteh.Id.Equals(reshteid));

                var zr = (from c in zir.Where(x => !ZRId.Contains(x.ZirReshtehId)) select c).ToList();
                zr.ForEach(x => x.IsDeleted = true);
                var q =  ZRId.Except(zir.Where(x => !x.IsDeleted).Select(x => x.ZirReshtehId));
                //soalVM.Tags.Except(tags.Where(x => !x.Isdeleted).Select(x => x.Id));
                //یک فیلتر یا کوئری برای اضافه کردن رکورد جدید به جدول
                //تگ سوال برای تگهای جدید اضافه
                //شده توسط کاربر باید نوشته شد
                foreach (var n in q)
                {
                    db.Addnew<ReshtehUser>(new ReshtehUser { IsDeleted = false, ZirReshtehId = n, ApplicationUserId = user.Id });
                }
            }
            else
            {
                //var reshteid = _zirReshtehRepository.GetById(ZRId.FirstOrDefault()).Reshteh.Id;
                //if (reshtehId != reshteid) return false;
                var tg = db.Set<ReshtehUser>().Where(x => x.ApplicationUserId.Equals(user.Id) 
                && x.ZirReshteh.Reshteh.Id.Equals(reshtehId)).ToList();
                tg.ForEach(x => x.IsDeleted = true);
            }
            db.SaveAllChanges();
            return true;
        }


    }
}