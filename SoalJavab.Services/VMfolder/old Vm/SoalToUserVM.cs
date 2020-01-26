using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;


namespace SoalJavab.Services.Models.old
{
    public class UserListVM
    {
        public string name { get; set; }
        public long Id { get; set; }
    }
    public class SoalOfUserVM
    {
        public long UserId { get; set; }
        public long SoalId { get; set; }
        public string Matn { get; set; }
    }
    public class SoalToUserVM
    {
        public long UserId { get; set; }
        public long SoalId { get; set; }
        public long SoaltoUserId { get; set; }
        public string Matn { get; set; }
    }
}
