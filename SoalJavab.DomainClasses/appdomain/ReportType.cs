using System;
using System.Collections.Generic;

namespace SoalJavab.DomainClasses
{
    public class ReportType
    {
        public int Id { get; set; }
        public string Onvan { get; set; }
        public bool IsDeleted { get; set; }
        public virtual IList<ReportSoal> ReportSoal { get; set; }
        public virtual IList<ReportJavab> ReportJavab { get; set; }
        public virtual  IList<ReportUser> ReportUser { get; set; }
    }
    public class ReportSoal
    {
        public int Id { get; set; }
        public string Matn { get; set; }
        public DateTime RegDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsVisited { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Soal Soal { get; set; }
        public virtual ReportType ReportType { get; set; }

    }
    public class ReportJavab
    {
        public int Id { get; set; }
        public string Matn { get; set; }
        public DateTime RegDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsVisited { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Soal Soal { get; set; }
        public virtual ReportType ReportType { get; set; }
    }

    public class ReportUser
    {
        public int Id { get; set; }
        public string Matn { get; set; }
        public DateTime RegDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsVisited { get; set; }
        public virtual ApplicationUser User { get; set; }
        //public virtual ApplicationUser User2 { get; set; }
        public virtual ReportType ReportType { get; set; }
    }


}
