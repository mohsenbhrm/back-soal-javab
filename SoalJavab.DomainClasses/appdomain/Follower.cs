

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoalJavab.DomainClasses
{
    public class SoalFollower
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("SoalId")]
        public virtual Soal Soal { set; get; }
        public long SoalId { set; get; }
    }
}
