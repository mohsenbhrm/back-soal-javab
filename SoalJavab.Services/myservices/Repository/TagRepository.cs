using System;
using System.Collections.Generic;
using System.Linq;
using SoalJavab.DomainClasses;
using SoalJavab.DataLayer;
using SoalJavab.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SoalJavab.Services.Models;
using System.Linq.Expressions;

namespace SoalJavab.Services
{
   public  class TagRepository :ITagRepository
    {
        IUnitOfWork db;
        DbSet<Tag> _Tag;
        public TagRepository(IUnitOfWork Context)
        {
            this.db = Context;
            _Tag = db.Set<Tag>();
        }
        public bool Delete(long entityId)
        {
            var q = GetById(entityId);
            q.IsDeleted = true;
            db.SaveAllChanges();
            return true;
        }
        public async Task<bool> DeleteAsync(long entityId)
        {
            var q = await GetByIdAsync(entityId);
            q.IsDeleted = true;
            await db.SaveAllChangesAsync();
            return true;
        }
        public IList<Tag> Get()
        {
            return _Tag.Where(e => !e.IsDeleted).ToList();
        }

        public Tag GetById(int Id)
        {
            return _Tag.Where(e => e.Id == Id).SingleOrDefault();
        }

        public async Task<IList<Tag>> GetAsync()
        {
            return await _Tag.Where(e => !e.IsDeleted).ToListAsync();
        }

        public async Task<Tag> GetByIdAsync(long Id)
        {
            return await _Tag.Where(e => e.Id == Id).SingleOrDefaultAsync();
        }
        public IList<Tag> GetByReshteh(long id)
        {
            return _Tag.Where(e => !e.IsDeleted && e.ZirReshtehId.Equals(id)).ToList();
        }
        public async Task<IList<Tag>> GetByReshtehAsync(long id)
        {
            return await _Tag.Where(e => !e.IsDeleted && e.ZirReshtehId.Equals(id)).ToListAsync();
        }
         public IList<TagVM> CreatRange(IList<TagVM> t)
        {

            try
            {
                IList<Tag> tg = new List<Tag>();
                foreach(var n in t)
                {
                    tg.Add(new Tag{
                        ZirReshtehId = n.ZirReshtehId,
                Onvan = n.Onvan
                });
                }
                db.AddThisRange<Tag>(tg);
                db.SaveAllChanges();
                t = tg.Select(c=> new TagVM { Onvan = c.Onvan , Id=c.Id}).ToList();
                return t;
            }
            catch { return null; }
        }
        public async Task<TagVM> CreatAsync(TagVM t)
        {

            try
            {
                Tag tg = new Tag();
                tg.ZirReshtehId = t.ZirReshtehId;
                tg.Onvan = t.Onvan;
                db.Addnew<Tag>(tg);
                await db.SaveAllChangesAsync();
                t.Id = tg.Id;
                return t;
            }
            catch { return null; }
        }

        public IEnumerable<Tag> Get(Expression<Func<Tag, bool>> filter = null, Func<IQueryable<Tag>, IOrderedQueryable<Tag>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }


        public Tag GetById(long Id)
        {
            return _Tag.Where(e => e.Id == Id).SingleOrDefault();
        }

        public bool Insert(Tag entity)
        {
            throw new NotImplementedException();
        }

        public Tag Insert_ReturnObject(Tag entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Tag entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Tag entity)
        {
            throw new NotImplementedException();
        }
        
        public Task<bool> InsertASync(Tag entity)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> Insert_ReturnObjectASync(Tag entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateASync(Tag entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteASync(Tag entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteASync(long entityId)
        {
            throw new NotImplementedException();
        }
    }
}
