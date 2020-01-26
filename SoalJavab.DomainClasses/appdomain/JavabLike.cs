using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoalJavab.DomainClasses
{
    public class JavabLike
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime regdate { set; get; }
        public virtual ApplicationUser User { set; get; }
        public virtual Javab Javab { set; get; }
    }
}
