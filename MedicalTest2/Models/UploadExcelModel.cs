using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models
{
    public class UploadExcelModel
    {
        [Required(ErrorMessage = "برجاء تحميل ملف الاكسيل")]
        [Display()]
        public IFormFile Url { get; set; }
        public string ImageUrl { get; set; }
    }
}
