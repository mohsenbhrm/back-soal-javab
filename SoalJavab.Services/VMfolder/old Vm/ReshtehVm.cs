using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SoalJavab.DomainClasses;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SoalJavab.Services.Models.old
{
    public class ReshtehVmold
    {
        [Display(Name = "دسته بندی موضوع")]
        [Required(ErrorMessage = "موضوع مرتبط با سوال را انتخاب کنید")]
        public List<SelectListItem> Reshteh { get; set; }
    }
    public class ZirReshteVm
    {
        public long Id { get; set; }
        public string  Name { get; set; }
    }
}
