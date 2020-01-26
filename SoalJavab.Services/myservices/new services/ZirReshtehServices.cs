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

    public class ZirReshtehServices : IZirReshtehServices
    {
        private IReshteh _reshteh;
        private IUnitOfWork _db;
        private DbSet<ZirReshteh> _zirreshteh;

        public ZirReshtehServices(IUnitOfWork unitOfWork, IReshteh reshteh)
        {
            _reshteh = reshteh;
            _db = unitOfWork;
            _zirreshteh = _db.Set<ZirReshteh>();
        }
        public IList<ZirReshtehVm> GetByReshteh(long id)
        {
            var q = _zirreshteh.Where(x => x.Reshteh.Id == id && !x.IsDeleted).ToList();

            if (q == null)
            {
                return null;
            }
            return q.Select(c => new ZirReshtehVm
            {
                Id = c.Id,
                Onvan = c.Onvan,
                Reshteh = c.Reshteh.Onvan
            }).ToList();
        }
        public IList<ZirReshtehVm> GetByUser(long id)
        {
            var q1 = _db.Set<ReshtehUser>().Where(x => x.User.Id == id && !x.IsDeleted).Select(i=>i.ZirReshtehId).ToList();

            var q = _zirreshteh.Where(x =>q1.Contains(x.Id) && !x.IsDeleted).Include(rs=>rs.Reshteh)
            .ToList();

            if (q == null)
            {
                return null;
            }
            return q.Select(c => new ZirReshtehVm
            {
                Id = c.Id,
                Onvan = c.Onvan,
                Reshteh = c.Reshteh.Onvan
            }).ToList();
        }

        public bool ValidateZirreshteh(long[] id)
        {
            foreach (var n in id)
            {
                if (ValidateZirreshteh(n) == false)
                {
                    return false;
                }
            }
            return true;
        }
        public bool ValidateZirreshteh(long id)
        {
            var q = _zirreshteh.Find(id);
            if (q == null) return false;
            return true;
        }
    }
}