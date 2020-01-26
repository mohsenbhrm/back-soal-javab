using System.Collections.Generic;
using SoalJavab.Services.Models;

namespace SoalJavab.Services.Contracts
{
    public interface IReshtehServices
    {
        IList<ReshtehVm> Get();
    }
}