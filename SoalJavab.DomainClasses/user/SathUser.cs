using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SoalJavab.DomainClasses
{
    public class SathUser
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser User { get; set; }
        public long ApplicationUserId { get; set; }

        [ForeignKey("SathId")]
        public virtual Sath Sath { get; set; }
        public int SathId { get; set; }

    }
}
