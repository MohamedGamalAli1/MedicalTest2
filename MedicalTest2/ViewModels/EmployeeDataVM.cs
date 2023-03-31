using MedicalTest2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.ViewModels
{
    public class EmployeeDataVM
    {
        [Key]
        [Display(Name = "الرقم")]
        public int Id { get; set; }
        [Display(Name = "الاسم")]
        public string Name { get; set; }
        [Display(Name = "اسم المستخدم")]
        public string NationalId { get; set; }
        [Display(Name = "تاريخ التسجيل")]
        public string CreationDate { get; set; }
        [Display(Name = "الحالة")]
        public bool IsRetired { get; set; }
        [Display(Name = "رقم الحاسب")]
        public int ComputerNumber { get; set; }
        [Display(Name = "الجهة")]
        public string DestinationName { get; set; }
        [Display(Name = "القسم")]
        public string DepartmentName { get; set; }
        [Display(Name = "العمل الفعلى")]
        public string ActualWorkName { get; set; }
        [Display(Name = "فئة الوظيفة")]
        public string JobCategoryName { get; set; }
        [Display(Name = "نوع التعيبن")]
        public string WorkTypeName { get; set; }
        [Display(Name = "رقم الجوال")]
        public string PhoneNumber { get; set; }
        [Display(Name ="البريد الالكترونى")]
        public string Email { get; set; }  
        
    }
}
