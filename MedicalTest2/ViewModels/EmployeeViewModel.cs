using MedicalTest2.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "برجاء ادخال الاسم")]
        [MaxLength(20)]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "برجاء ادخال العنوان")]

        public string EmployeeTitle { get; set; }
        public string EmployeeDescription { get; set; }
        [Required(ErrorMessage = "برجاء ادخال التصنيف")]

        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; }
        public IFormFile Url { get; set; }
        public string ImageUrl { get; set; }
    }
}
