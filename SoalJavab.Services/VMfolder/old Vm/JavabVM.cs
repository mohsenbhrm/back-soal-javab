using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoalJavab.Services.Models.old

{
    public class JavabVM
    {
        public long Id { get; set; }
        [Required]
        public string matn { get; set; }
        [Required]
        public long IdSoal { get; set; }
        public long IdUser { get; set; }
    }
    public class JavabCountVM
    {
        public  long Count { get; set; }
        public long Soal_Id { get; set; }
    }
}