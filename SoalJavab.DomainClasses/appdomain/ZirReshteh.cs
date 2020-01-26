using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoalJavab.DomainClasses
{
    public class ZirReshteh
    {
        public long Id { get; set; }
        public string Onvan { get; set; }
        public DateTime? Regdat { get; set; }
        public bool IsVisited { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Reshteh Reshteh { get; set; }
        public virtual IList<ReshtehUser> ReshtehUser { set; get; }
        public virtual IList<Soal> Soal { get; set; }
        public virtual IList<Tag> Tag { get; set; }
    }
}
