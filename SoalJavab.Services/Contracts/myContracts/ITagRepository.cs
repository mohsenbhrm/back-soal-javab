using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoalJavab.DomainClasses;
using SoalJavab.Services.Models;

namespace SoalJavab.Services.Contracts
{
    public interface ITagRepository 
    {
       Task<bool> DeleteAsync(long entityId);
        Task<IList<Tag>> GetByReshtehAsync(long id);
        IList<Tag> Get ();

        Tag GetById(long Id);
        

        ///
        Task<IList<Tag>> GetAsync();
        Task<Tag> GetByIdAsync(long Id);
        Task<TagVM> CreatAsync(TagVM t);
        IList<Tag> GetByReshteh(long id);
        IList<TagVM> CreatRange(IList<TagVM> t);
        bool Delete(long entityId);
        
    }
}