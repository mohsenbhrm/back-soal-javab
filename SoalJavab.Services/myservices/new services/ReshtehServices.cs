using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SoalJavab.DataLayer;
using SoalJavab.DomainClasses;
using SoalJavab.Services.Contracts;
using SoalJavab.Services.Models;
using System.Linq;

namespace SoalJavab.Services.myservices
{
   public  class ReshtehServices: IReshtehServices
    {
        IReshteh _reshteh;

        public ReshtehServices(IUnitOfWork unitOfWork, IReshteh reshteh)
        {
            _reshteh = reshteh;
        }
        public IList<ReshtehVm> Get()
        {
            var r = _reshteh.Get().Select(x => new ReshtehVm
            {
                Id = x.Id,
                Onvan = x.Onvan,
                Zirreshteh = x.ZirReshteh.Select(y => new ReshtehVm
                { Id = y.Id, Onvan = y.Onvan }).ToList()
            }).ToList();
            return r;
        }
    }
}
