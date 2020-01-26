using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoalJavab.Services.Contracts;
using SoalJavab.DomainClasses;
//using SoalJavab.Services.Models.old;
using Microsoft.AspNetCore.Mvc.Rendering;
using SoalJavab.DataLayer;
using SoalJavab.Services.Models;

namespace SoalJavab.Services
{

    public partial class Services
    {
        IUnitOfWork db;
        ISoalRepository _soalRepository;
        IUserRepository _Userrepository;
        ITagRepository _tagRepository;
        public Services(IUnitOfWork unitOfWork, ISoalRepository soalrepo, IUserRepository userRepo,ITagRepository tagRepository)
        {
            db = unitOfWork;
            _soalRepository = soalrepo;
            _Userrepository = userRepo;
            _tagRepository = tagRepository;
        }

        public Services()
        {
        }

        //UnitOfWork UnitOfWork = new UnitOfWork(new SoalClassLibrary.Soalcontext());
  


        private IEnumerable<ApplicationUser> GetUsersByzirreshteh(long ZirReshtehId)
        {
            try
            {
                //var zr = UnitOfWork.ZirReshtehGenericRepository.GetById(ZirReshtehId);
                var q = db.Set<ZirReshteh>().Find(ZirReshtehId);
                var us = _Userrepository.GetallbyReshteh(q);
                return us;
            }
            catch (Exception e)
            { throw e; }
        }
        public IEnumerable<UserListVM> getUsersList()
        {
            var s = _Userrepository.Get(x => !x.Ban).Select(x => new UserListVM { Id = x.Id, name = x.Name });
            return s;
        }

        

        public IList<JsonVm> getZirReshteh_JsonVM(string Id)
        {
            var idnew = Int64.Parse(Id);
            var s = db.Set<ZirReshteh>()
                .Where(x => !x.IsDeleted && x.Reshteh.Id.Equals(idnew))
                .Select(x => new JsonVm { name = x.Onvan, Id = x.Id }).ToList();
            return s;
        }




    }
}
