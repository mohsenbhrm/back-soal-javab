using Microsoft.EntityFrameworkCore;
using SoalJavab.DataLayer;
using SoalJavab.DomainClasses;
using SoalJavab.Services.Contracts;
using SoalJavab.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoalJavab.Services.myservices
{
    public class TagServices : ITagServices
    {
        private IUnitOfWork db;
        private ISoalRepository _soalRepository;
        private ITagRepository _tagRepository;
        private readonly DbSet<Tag> _tags;

        public TagServices(IUnitOfWork unitOfWork, ITagRepository tag,
            ISoalRepository soalrepo,
            ITagRepository tagRepository)
        {
            db = unitOfWork;
            _soalRepository = soalrepo;
            _tagRepository = tagRepository;
            _tags = db.Set<Tag>();

        }
        public IList<TagVM> getTags()
        {
            return _tags.Where(e => !e.IsDeleted).Select(x =>
            new TagVM
            {
                Id = x.Id,
                Onvan = x.Onvan,
                ZirReshtehId = x.ZirReshtehId
            }).ToList(); ;
        }
        public async Task<List<JsonVm>> GetTAGsAsync() =>
        await _tags.Where(e => !e.IsDeleted).Select(x =>
        new JsonVm
        {
            Id = x.Id,
            name = x.Onvan,
        }).ToListAsync();

        public IList<TagVM> GetTagsByzirreshte(long Idzirreshteh)
        {
            try
            {
                var q = _tags.Where(e => !e.IsDeleted && e.ZirReshtehId.Equals(Idzirreshteh));
                return q.Select(x =>
              
               new TagVM
               {
                   Id = x.Id,
                   Onvan = x.Onvan,
                   ZirReshtehId = x.ZirReshtehId
               }).ToList();
            }
            catch
            {
                throw new Exception();
            }

        }
        public Task<List<JsonVm>> GetTagsByzirreshteAsync(long Idzirreshteh) =>
        _tags.Where(e => !e.IsDeleted && e.ZirReshtehId.Equals(Idzirreshteh))
           .Select(x =>
           new JsonVm
           {
               Id = x.Id,
               name = x.Onvan,
           }).ToListAsync();
        public void DeleteTag(long IdTag)
        {
            var q = _tagRepository.Delete(IdTag);
        }
        public TagVM getTagForCreat(string ZirReshte)
        {
            var t = new TagVM { ZirReshtehId = long.Parse(ZirReshte) };
            return t;
        }
        public TagVM CreatTag(TagVM tag)
        {
            try
            {
                if (tag == null)
                {
                    throw new NullReferenceException();
                }
                Tag tg = new Tag();
                tg.ZirReshtehId = tag.ZirReshtehId;
                tg.Onvan = tag.Onvan;
                db.Addnew<Tag>(tg);
                db.SaveAllChanges();
                tag.Id = tg.Id;
                return tag;
            }
            catch { return null; }
        }
        public async Task<TagVM> CreatTagAsync(TagVM tag)
        {

            try
            {
                if (tag == null)
                {
                    throw new NullReferenceException();
                }

                Tag tg = new Tag();
                tg.ZirReshtehId = tag.ZirReshtehId;
                tg.Onvan = tag.Onvan;
                db.Addnew<Tag>(tg);
                await db.SaveAllChangesAsync();
                tag.Id = tg.Id;

                if (tg != null)
                    return new TagVM { Id = tg.Id, Onvan = tg.Onvan, ZirReshtehId = tg.ZirReshtehId };
                throw new Exception();
            }
            catch { return null; }
        }
        public IList<JsonVm> GetTags(long IdZirreshte)
        {

            var q = _tagRepository.GetByReshteh(IdZirreshte)
                .Select(e => new JsonVm { Id = e.Id, name = e.Onvan }).ToList();
            return q;
        }
        public IList<JsonVm> GetTags(long IdZirreshte, string TagName)
        {
            try {

        
            var q = GetTagsByzirreshte(IdZirreshte).Where(x => x.Onvan.Contains(TagName))
                .Select(e => new JsonVm { Id = e.Id, name = e.Onvan }).ToList();
            return q;
            }
            catch{
                throw new Exception();
            }
        }
        public async Task<IList<JsonVm>> GetTAGsAsync(long IdZirreshte)
        {
            var q = await GetTagsByzirreshteAsync(IdZirreshte);
            return q;
        }
        public IList<JsonVm> GetTagsofSoal(long Idsoal)
        {
            var q = db.Set<TagSoal>().Where(e => !e.Isdeleted && e.Soal.Id.Equals(Idsoal))
                .Select(e => new JsonVm { Id = e.Tag.Id, name = e.Tag.Onvan }).ToList();
            return q;
        }
        // بازیابی دیگر تگهای مربوط به زیررشته برای ویرایش سوال
        public IList<JsonVm> GetOtherTagsforSoal(long Idsoal)
        {
            var q = db.Set<TagSoal>().Where(e => !e.Isdeleted && e.Soal.Id.Equals(Idsoal)).Select(e => e.Tag);

            var s = db.Set<Tag>()
                .Where(e => !e.IsDeleted && e.ZirReshteh.Id.Equals(q.FirstOrDefault().ZirReshtehId)).Except(q)
                .Select(e => new JsonVm { Id = e.Id, name = e.Onvan }).ToList();
            return s;
        }



    }
}
