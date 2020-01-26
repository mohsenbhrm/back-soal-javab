

namespace SoalJavab.DomainClasses
{
    public class SoalToUser
    {
        public long Id { get; set; }
        public bool IsVisited { get; set; }
        public bool Isdeleted { get; set; }
        public bool Isanswered { get; set; }
        public virtual ApplicationUser User { set; get; }
        public virtual Soal Soal { set; get; }
    }
}
