using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SoalJavab.DomainClasses
{
    public class UserRole 
    { 
        [Key]
        public long UserRoleId{get;set;}
       public long UserId { get; set; }
        public long RoleId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Role Role { get; set; } 

    }
}