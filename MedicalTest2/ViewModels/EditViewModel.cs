using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.ViewModels
{
    public class EditViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "الاسم")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "البريد الالكترونى")]
        public string Email { get; set; }
    }
}
