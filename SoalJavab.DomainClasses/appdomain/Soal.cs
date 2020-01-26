using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoalJavab.DomainClasses

{
    public class Soal
    {
        public virtual IList<SoalToUser> SoalToUser { get; set; }
        public virtual IList<TagSoal> TagSoal { set; get; }
        public virtual IList<Javab> Javab { set; get; }
        public virtual IList<ReportSoal> ReportSoal { get; set; }
        public virtual IList<SoalFollower> SoalFollower { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }
        public string Matn { get; set; }
        public string Name { get; set; }
        public DateTime Regdat { get; set; }
        public bool IsVisited { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] RowVersion { set; get; }
        /// <summary>
        /// //////
        /// </summary>
        /// 
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser User { set; get; }
        public long ApplicationUserId { set; get; }
        [ForeignKey("ZirReshtehId")]
        public virtual ZirReshteh ZirReshteh { set; get; }
        public long ZirReshtehId { get; set; }

    }
}
