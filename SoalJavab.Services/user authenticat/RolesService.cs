using System.Linq;
using System.Collections.Generic;
using SoalJavab.Common;
using SoalJavab.DataLayer;
using SoalJavab.DomainClasses;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using SoalJavab.Services.Models;

namespace SoalJavab.Services
{
    public interface IRolesService
    {
        Task<List<Role>> FindUserRolesAsync(long userId);
        Task<bool> IsUserInRoleAsync(long userId, string roleName);
        Task<List<ApplicationUser>> FindUsersInRoleAsync(string roleName);
        void CahngeUserRole(long userId, long[] roleIds);
        void addRole(string name);
        void addUserRole(long userId, long roleId);
        long getRoleId(string name);
    }

    public class RolesService : IRolesService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<Role> _roles;
        private readonly DbSet<ApplicationUser> _users;

        public RolesService(IUnitOfWork uow)
        {
            _uow = uow;
            _uow.CheckArgumentIsNull(nameof(_uow));

            _roles = _uow.Set<Role>();
            _users = _uow.Set<ApplicationUser>();
        }

        public Task<List<Role>> FindUserRolesAsync(long userId)
        {
            var r = from Role in _roles
                    from UserRole in Role.UserRoles
                    where UserRole.UserId == userId
                    select Role;
            ////var userRolesQuery = from role in _roles
            //                     from userRoles in role.UserRoles
            //                     where userRoles.UserId == userId
            //                     select role;

            return r.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<bool> IsUserInRoleAsync(long userId, string roleName)
        {
            var userRolesQuery = from role in _roles
                                 where role.Name == roleName
                                 from user in role.UserRoles
                                 where user.UserId == userId
                                 select role;
            var userRole = await userRolesQuery.FirstOrDefaultAsync();
            return userRole != null;
        }

        public Task<List<ApplicationUser>> FindUsersInRoleAsync(string roleName)
        {
            var roleUserIdsQuery = from role in _roles
                                   where role.Name == roleName
                                   from ApplicationUser in role.UserRoles
                                   select ApplicationUser.UserId;

            return _users.Where(user =>roleUserIdsQuery.Contains(user.Id))
                         .ToListAsync();
        }

        public void CahngeUserRole(long userId, long[] roleIds)
        {
            if(userId<1 || roleIds == null)
            {throw new Exception("کاربر و یا نقش کاربر مشخص نیست");}
            var q = _uow.Set<UserRole>();
            foreach (var n in q.Where(x => x.UserId == userId))
            {
                q.Remove(n);
            }
            foreach (var n in roleIds)
            {

                q.Add(entity: new UserRole { UserId = userId, RoleId = n });
            }
            _uow.SaveAllChanges();
        }
        
        public void addRole(string name)
        {
            _roles.Add(new Role { Name = name });
            _uow.SaveAllChanges();
        }
       public void addRole(string[] name)
        {
            foreach(var n in name)
            {
            _roles.Add(new Role { Name = n });
            }
            _uow.SaveAllChanges();
        }
        public void addUserRole(long userId,long roleId)
        {
            _uow.Set<UserRole>().Add( new UserRole 
            { UserId = userId,RoleId= roleId});
            _uow.SaveAllChanges();
        }
        
        public long getRoleId(string name)
        {
           return _roles.FirstOrDefault(x=>x.Name == name).Id;
        }
    }
}