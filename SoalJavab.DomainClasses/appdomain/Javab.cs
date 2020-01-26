using System;
using System.Collections.Generic;


namespace SoalJavab.DomainClasses
{
    public class Javab
    {
        public long Id { get; set; }
        public string Matn { get; set; }
        public DateTime RegDate { get; set; }
        public bool Isvisited { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Soal Soal { get; set; }
        public virtual IList<JavabLike> JavabLike { get; set; }
        public virtual IList<ReportJavab> ReportJavab { get; set; }
    }
}
