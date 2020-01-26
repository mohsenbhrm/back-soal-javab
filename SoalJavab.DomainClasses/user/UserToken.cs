using System;

namespace SoalJavab.DomainClasses
{

public class UserToken
    {
        public int Id { get; set; }

        public string AccessTokenHash { get; set; }

        public DateTimeOffset AccessTokenExpiresDateTime { get; set; }

        public string RefreshTokenIdHash { get; set; }

        public string RefreshTokenIdHashSource { get; set; }

        public DateTimeOffset RefreshTokenExpiresDateTime { get; set; }

        public long UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}