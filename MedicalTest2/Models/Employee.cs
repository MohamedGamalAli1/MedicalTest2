using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "برجاء ادخال الاسم")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "برجاء ادخال التصنيف")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string ImageUrl { get; set; }
    }
}
