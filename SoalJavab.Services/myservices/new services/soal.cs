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
    public class SoalSevices : ISoalServices
    {
        IUnitOfWork db;
        ISoalRepository _soalRepository;
        DbSet<Soal> _Soals;
        ITagRepository _tagRepository;
        public SoalSevices(IUnitOfWork unitOfWork, ITagRepository tag,
            ISoalRepository soalrepo,
            ITagRepository tagRepository
            )
        {

            db = unitOfWork;
            _Soals = db.Set<Soal>();
            _soalRepository = soalrepo;
            _tagRepository = tagRepository;

        }

        public SoalVM getSoalForEdit(long id)
        {
            try
            {
                var soal = _soalRepository.GetById(id);
                var zr = soal.TagSoal.Where(x => !x.Isdeleted).FirstOrDefault().Tag.ZirReshteh;
                var s = new SoalVM
                {
                    Id = soal.Id,
                    Matn = soal.Matn,
                    ZirReshtehId = zr.Id
                };
                return s;
            }
            catch { return null; }
        }
        public bool postforSoal(SoalVM soalVM, long UserId)
        {
            try
            {
                var q = new List<TagVM>();

                var e = new List<TagVM>();

                foreach (var n in soalVM.Tags.Where(x => x.Id == 0))
                {
                    q.Add(new TagVM
                    {
                        Onvan = n.name,
                        Id = n.Id,
                        ZirReshtehId = soalVM.ZirReshtehId
                    }
                    );
                }
                 q =  _tagRepository.CreatRange(q).ToList();

                foreach (var n in soalVM.Tags.Where(x => x.Id != 0))
                {
                    e.Add(new TagVM
                    {
                        Onvan = n.name,
                        Id = n.Id,
                        ZirReshtehId = soalVM.ZirReshtehId
                    }
                    );
                }
                
                var s = q.Union(e);
                var w = s.Select(c => c.Id).ToArray();

                // q.AddRange(soalVM.Tags.Where(x => x.Id == 0)
                // .Select(c => new TagVM { ZirReshtehId = c.Id }));
                _soalRepository.Insert(new Soal
                {
                    Matn = soalVM.Matn,
                    ApplicationUserId = UserId,
                    ZirReshtehId = soalVM.ZirReshtehId
                },
                w);
                db.SaveAllChanges();
                return true;
            }
            catch (Exception e) { throw e; }
        }
        public bool EditforSoal(SoalEditVM soalVM)
        {
            try
            {
                var sl = _soalRepository.GetById(soalVM.Id);
                sl.Matn = soalVM.Matn;
                if (soalVM.TagsId != null)
                {
                    var tags = db.Set<TagSoal>().Include(S=>S.Soal).ToList().Where(x => x.Soal.Id.Equals(soalVM.Id));
                    
                    var s = (from c in tags.Where(x => !soalVM.TagsId.Contains(x.Id)) select c).ToList();
                    s.ForEach(x => x.Isdeleted = true);
                    soalVM.TagsId.Except(tags.Where(x => !x.Isdeleted).Select(x => x.Id));
                    //یک فیلتر یا کوئری برای اضافه کردن رکورد جدید به جدول
                    //تگ سوال برای تگهای جدید اضافه
                    //شده توسط کاربر باید نوشته شد
                    foreach (var n in soalVM.TagsId)
                    {
                        db.Addnew<TagSoal>(new TagSoal { Isdeleted = false, TagId = n, Soal = sl });
                    }
                }
                else
                {
                    var tg = db.Set<TagSoal>().Where(x => x.Soal.Id.Equals(soalVM.Id)).ToList();
                    tg.ForEach(x => x.Isdeleted = true);
                }
                db.MarkAsChanged<Soal>(sl);
                
                db.SaveAllChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public SoalOfUserVM getSoalbyId(long IdSoal)
        {
            try
            {
                var x = _Soals.Where(s =>s.Id == IdSoal).Select(c=>new SoalOfUserVM
                {
                    RegDate = c.Regdat.ToString(),
                   JavabCount =c.Javab.Where(i=>!i.IsDeleted).LongCount(),
                    SoalId = c.Id,
                    Matn = c.Matn,
                    Reshteh = c.ZirReshteh.Reshteh.Onvan,
                    Zirreshteh = c.ZirReshteh.Onvan,
                    username = c.User.Username                    
                }).FirstOrDefault();
                return x;
            }
            catch { return null; }
        }
        

       public IList<SoalOfUserVM> getSoalbyIdUser(long IdUser)
        {
            try
            {
                var t = _Soals.Where(x => x.User.Id == IdUser && !x.IsDeleted && x.TagSoal.Any())
                .Include(b=>b.Javab)
                .Include(z=>z.ZirReshteh)
                .ThenInclude(y=>y.Reshteh)
                .Include(tg => tg.TagSoal).ThenInclude(th=>th.Tag).ToList();                
                  var q =   t.Select(x => new SoalOfUserVM
                     {


                      RegDate = x.Regdat.ToString(),
                      SoalId = x.Id,
                      Matn = x.Matn,
                      Reshteh = x.ZirReshteh.Reshteh.Onvan,
                      Zirreshteh = x.ZirReshteh.Onvan,
                      JavabCount = x.Javab.Where(j => !j.IsDeleted).LongCount(),
                      IdZirreshteh = x.ZirReshtehId,
                      Tags = x.TagSoal.Select(c => new TagVM
                      {
                          Id = c.TagId,
                          Onvan = c.Tag.Onvan
                      }).ToList()
                  }).ToList();
                return q;
            }
            catch { return null; }
        }
        public IList<SoalOfUserVM> getSoalbyIdAnswered(long IdUser)
        {
              try
            {
                var t = _soalRepository.GetAllDeletedByUserID(IdUser)
                    .Select(x => new SoalOfUserVM
                    { SoalId = x.Id, Matn = x.Matn }).ToList();
                return t;
            }
            catch { return null; }

        }
        public IList<SoalOfUserVM> getSoalbyIdDeleted(long IdUser)
        {
            try
            {
                var t = _soalRepository.GetAllDeletedByUserID(IdUser)
                    .Select(x => new SoalOfUserVM
                    { SoalId = x.Id, Matn = x.Matn }).ToList();
                return t;
            }
            catch { return null; }

        }

       
        public IList<SoalOfUserVM> getSoalbyIdUserAnswered(long IdIdUser)
        {
            throw new NotImplementedException();
        }

        public IList<SoalOfUserVM> getSoalbyIdUserDeleted(long IdIdUser)

        {
            throw new NotImplementedException();
        }
        public Soal GetById(long id)
        {

            var q = _Soals.Find(id);
            return q;
        }

        public bool isSoalOfuser(long IdSoal, long IdUser)
        {
            try {
                var S = GetById(IdSoal).User.Id;
            if (S == IdUser)
            {
                return true;
            }
            return false;
            }
            catch 
            {
                return false;
            }
        }

        public bool DeleteSoal(long Idsoal)
        {
            if (Idsoal>0)
            {
            var s = _Soals.Where(x => x.Id == Idsoal).FirstOrDefault();
                if (s != null)
                {
                    s.IsDeleted = true;
                    db.MarkAsChanged(s);
                    db.SaveAllChanges();
                    return true;
                }
                return false; 
            }
            return false;
        }
    
    public IEnumerable<Soal> GetAllByzirreshteh(long ZirReshtehID)
        {
            try
            {

                var w = db.Set<Tag>()
                    .Where(c => !c.IsDeleted && c.ZirReshtehId == ZirReshtehID)
                    .Select(c => c.Id).ToArray();
                var e = _Soals.Where(x =>
                                w.Contains(x.TagSoal.FirstOrDefault().TagId)
                                && !x.IsDeleted)
                                .Include(x=>x.SoalToUser)
                                .Include(u=>u.User).ToList();
                return e.ToList();
            }
            catch { return null; }
        }
    }
}
