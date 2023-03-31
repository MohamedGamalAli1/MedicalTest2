using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models
{
    public class EmployeeCategory
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "برجاء ادخال الاسم")]
        [Display(Name = "الاسم")]
        public string Name { get; set; }
    }
}
