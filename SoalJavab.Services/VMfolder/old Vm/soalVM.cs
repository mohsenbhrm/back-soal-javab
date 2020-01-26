using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SoalJavab.DomainClasses;
//using System.Web.Mvc;
using SoalJavab.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SoalJavab.Services.Models.old
{
    public class soalVM
    {
        [HiddenInput]
        public long Id { get; set; }
        [Required(ErrorMessage = "متن سوال را وارد کنید")]
        public string Matn { get; set; }
        //public  List<Reshteh> Reshteh { get; set; }
        [Display(Name = "دسته بندی موضوع")]
        [Required(ErrorMessage = "موضوع مرتبط با سوال را انتخاب کنید")]
        public List<SelectListItem> Reshteh { get; set; }
        [Display(Name = "موضوع")]
        [Required(ErrorMessage = "موضوع مرتبط با سوال را انتخاب کنید")]
        public List<SelectListItem> zirReshteh { get; set; }
        [Required(ErrorMessage="موضوع مرتبط با سوال را انتخاب کنید")]
        public string ZirReshtehSelect { get; set; }
        [Required(ErrorMessage = "تگهای مربوط را انتخاب کنید ")]
        public long[] Tags { get; set; }
    }
    public class TagVM
    {
        public long Id { get; set; }
        public long ZirReshtehId { get; set; }
        [Display(Name = "عنوان تگ")]
        public string Onvan { get; set; }
    }
}