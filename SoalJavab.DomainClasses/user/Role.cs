using System.Collections.Generic;


namespace SoalJavab.DomainClasses
{
    public class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();

        }
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
