using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoalJavab.Services.Models
{
    public class SoalVM
    {
        [HiddenInput]
        public long Id { get; set; }
        [Required(ErrorMessage = "متن سوال را وارد کنید")]
        public string Matn { get; set; }
        public long ZirReshtehId { get; set; }
        public IList<JsonVm> Tags { get; set; }
    }
    public class SoalEditVM
    {
        [HiddenInput]
        public long Id { get; set; }
        [Required(ErrorMessage = "متن سوال را وارد کنید")]
        public string Matn { get; set; }
        public long[] TagsId { get; set; }
    }
    public class TagVM
    {
        public long Id { get; set; }
        public long ZirReshtehId { get; set; }
        [Display(Name = "عنوان تگ")]
        public string Onvan { get; set; }
    }
    public class ReshtehVm
    {
        public long Id { get; set; }
        [Display(Name = "عنوان تگ")]
        public string Onvan { get; set; }
        public IList<ReshtehVm> Zirreshteh { get; set; }
    }
    public class ZirReshtehVm
    {
        public long Id { get; set; }
        [Display(Name = "عنوان تگ")]
        public string Onvan { get; set; }
        public string Reshteh { get; set; }
    }

    public class UserListVM
    {
        public string name { get; set; }
        public long Id { get; set; }
    }
    public class SoalOfUserVM
    {
        public string RegDate { get; set; }
        public long SoalId { get; set; }
        public string username { get; set; }
        public string Matn { get; set; }
        public long JavabCount { get; set; }
        public string Zirreshteh { get; set; }
        public long IdZirreshteh { get; set; }
        public string Reshteh { get; set; }
        public IList<TagVM> Tags { get; set; }
    }
    public class SoalToUserVM
    {
        public string Username { get; set; }
        public long SoalId { get; set; }
        public long SoaltoUserId { get; set; }
        public string Matn { get; set; }
        public DateTime regDate { get; set; }
    }
    public class JavabVM
    {
        public long Id { get; set; }
        [Required]
        public string Matn { get; set; }
        [Required]
        public long IdSoal { get; set; }
        public long IdUser { get; set; }
        public string Username { get; set; }
    }
    public class JavabCountVM
    {
        public long Count { get; set; }
        public long Soal_Id { get; set; }
    }
    public class JsonVm
    {
        public string name { get; set; }
        public long Id { get; set; }
    }
    public class LoginVm
    {
        public string password { set; get; }
        public string username { set; get; }
    }
    public class SignUpVm
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Email { get; set; }
        public long[] zirReshteh { get; set; }
    }
    public class UserRoleVM
    {
        public long userId { get; set; }
        public long userRole { get; set; }
        public string Rolename { get; set; }
        public long RoleId { get; set; }

    }
}
