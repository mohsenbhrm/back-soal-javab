using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoalJavab.DomainClasses
{
    public class Sath
    {
        public int Id { get; set; }
        public string Onvan { get; set; }
        public bool IsDeleted { set; get; }
        public virtual IList<SathUser> SathUser { get; set; }
    }
}
