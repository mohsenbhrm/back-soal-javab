//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoalJavab.DomainClasses
{
    public class ReshtehUser
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ZirReshtehId")]
        public virtual ZirReshteh ZirReshteh { get; set; }
        public long ZirReshtehId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser User { get; set; }
        public long ApplicationUserId { get; set; }
    }
}
