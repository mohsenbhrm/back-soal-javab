using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace SoalJavab.DomainClasses
{
    public class ApplicationUser
    {
        public virtual IList<SathUser> SathUser { get; set; }
        public virtual IList<Soal> Soal { get; set; }
        public virtual IList<SoalFollower> SoalFollower { get; set; }
        public virtual IList<Javab> Javab { get; set; }
        public virtual IList<JavabLike> JavabLike { get; set; }
        public virtual IList<ReshtehUser> ReshtehUser { get; set; }
        public virtual IList<SoalToUser> SoalToUser { get; set; }
        public virtual IList<ReportUser> ReportUser { get; set; }
        public virtual IList<ReportSoal> ReportSoal { get; set; }
        public virtual IList<ReportJavab> ReportJavab { get; set; }
        public long Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Mobile { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Regdate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime visitedDate { get; set; }
        public DateTimeOffset? LastLoggedIn { get; set; }
        public DateTimeOffset? OldLoggedIn { get; set; }

        public bool IsActive { get; set; }
        /// <summary>
        /// every time the user changes his Password,
        /// or an admin changes his Roles or stat/IsActive,
        /// create a new `SerialNumber` GUID and store it in the DB.
        /// </summary>
        public string SerialNumber { get; set; }
        public string DisplayName { get; set; }

        public string Image { get; set; }
        public bool Ban { get; set; }
        public bool NewReg { get; set; }
        public string Passs { get; set; }
        public byte[] RowVersion { set; get; }
        public string FullName
        {
            get { return Name + " " + Family; }
        }
        // سایر خواص اضافی در اینجا

        public virtual Address Address { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<UserToken> UserTokens { get; set; }
    }
}
