using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoalJavab.DomainClasses
{
    public class Reshteh
    {
        public long Id { get; set; }
        public string Onvan { get; set; }
        public DateTime? Regdat { get; set; }
        public bool IsVisited { get; set; }
        public bool IsDeleted { get; set; }
        public virtual IList<ZirReshteh> ZirReshteh { set; get; }
    }
}
