using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoalJavab.DomainClasses;

namespace SoalJavab.Services.Contracts
{
    public interface ICustomRoleStore : IDisposable
    {
        Task<Role> FindByIdAsync(long roleId);
        Task<Role> FindByNameAsync(string roleName);
        Task CreateAsync(Role role);
        Task DeleteAsync(Role role);
        Task UpdateAsync(Role role);
        DbContext Context { get; }
        bool DisposeContext { get; set; }
        IQueryable<Role> Roles { get; }
    }
}