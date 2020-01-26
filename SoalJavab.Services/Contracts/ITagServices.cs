using System.Collections.Generic;
using System.Threading.Tasks;
using SoalJavab.Services.Models;

namespace SoalJavab.Services.Contracts
{
    public interface ITagServices
    {
        TagVM CreatTag(TagVM tag);
        Task<TagVM> CreatTagAsync(TagVM tag);
        IList<JsonVm> GetOtherTagsforSoal(long Idsoal);
        TagVM getTagForCreat(string ZirReshte);
        IList<JsonVm> GetTags(long IdZirreshte);
        IList<JsonVm> GetTags(long IdZirreshte, string TagName);
        IList<TagVM> getTags();
        Task<IList<JsonVm>> GetTAGsAsync(long IdZirreshte);
        Task<List<JsonVm>> GetTAGsAsync();
        Task<List<JsonVm>> GetTagsByzirreshteAsync(long Idzirreshteh);
        IList<JsonVm> GetTagsofSoal(long Idsoal);
    }
}